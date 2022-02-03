using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Blob : MonoBehaviour {

    //public GameObject objectnya_BlobIntro;
    //Transform txt_BlobIntro;

	public GameObject objectnya_Txt_Enemy;
	Transform txt_Damage_Enemy_Pos;

	public GameObject objectnya_Txt;
	Transform txt_Damage_Pos;

	private Rigidbody2D myRidigBody;

	[SerializeField]
	private Blob_Stats Blob_HP;
	private int Blob_HP_Start = 50;

	private bool Blob_Dead = false;

	public static float Blob_Dead_X, Blob_Dead_Y;

	[SerializeField]
	private float Chase_Target;

	private Animator Blob_Animator;
	private Vector2 Blob_Direction;

	private bool Blob_IsAttacking;
	//private bool Blob_IsTiming;
	//private float timer;

	private int walkDirection;

	private bool isWalking;
	private bool isChasing;

	private float MyAttackTime;
	private int Random_Attack_Damage;
	private float AttackCD = 1.5f;

	private int random_Gold;

	public string enemyQuestName;
	private Quest_Manager quest_Manager;

	public GameObject Objectnya_Ruby;

    public static bool Blob_Attack_Sound;
    public static bool Blob_Dead_Sound;

    Scene scene_Name;

    // Use this for initialization
    public void Start () {
		quest_Manager = FindObjectOfType<Quest_Manager> ();

		myRidigBody = GetComponent<Rigidbody2D> ();

		Blob_HP.Initialize (Blob_HP_Start, Blob_HP_Start);
		Blob_Animator = GetComponent<Animator> ();

        //txt_BlobIntro = GameObject.Find("Blob_TxtPos").GetComponent<Transform> ();

        txt_Damage_Pos = GameObject.Find("Txt_Damage_Pos").GetComponent<Transform>();
        txt_Damage_Enemy_Pos = GameObject.Find ("Txt_Damage_Enemy_Pos").GetComponent<Transform> ();

		ChooseDirection ();

        Intro_Blob();

		Blob_IsAttacking = false;

		//beginTimer ();
	}

    public void Intro_Blob()
    {
        //Instantiate(objectnya_BlobIntro, txt_BlobIntro.position, Quaternion.identity);

        /*
        GameObject newBlob = new GameObject("myBlob");
        newBlob.transform.SetParent(this.transform);
        Text myBlobText = newBlob.AddComponent<Text>();
        myBlobText.transform.position = GameObject.Find("Blob_TxtPos").GetComponent<Transform>().position;
        myBlobText.text = "Blob Lvl 2";
        Debug.Log(myBlobText.transform.position);
        */
    }

	// Update is called once per frame
	public void Update () {

        scene_Name = SceneManager.GetActiveScene();

        if (!isWalking && !isChasing && !Blob_IsAttacking && !Blob_Dead) 
		{
			StartCoroutine (Walking ());
		}
		//Check Blob Mati Tidak
		else if (Blob_HP.MycurrentValue_Blob <= 0 && !Blob_Dead && scene_Name.name != "Tutorial_2") 
		{
			Blob_Dead = true;
			Blob_Spawn.maxBlob = Blob_Spawn.maxBlob - 1;
			Player.EXP_Plus += 25;

            Blob_Dead_Sound = true;

			if (!quest_Manager.quest_Completed [2] && quest_Manager.quests [2].gameObject.activeSelf) 
			{
				int Ran_Drop_Quest = Random.Range (0, 100);
				if (Ran_Drop_Quest <= 5) 
				{
					Instantiate (Objectnya_Ruby, this.transform.position, Quaternion.identity);
				}
			}

			random_Gold = Random.Range (15, 20);

			GameMaster.Player_Gold += random_Gold;
			Button_Shop.updateGold = true;

			Blob_Spawn.Random_Drop = true;

			//Blob_Dead_X = transform.localPosition.x;
			//Blob_Dead_Y = transform.localPosition.y;

			myRidigBody.velocity = Vector2.zero;

			ActiveLayer ("Blob_Dead");

			quest_Manager.enemyKilled = enemyQuestName;

			Destroy (gameObject, Blob_Animator.GetCurrentAnimatorClipInfo(2).Length);
		}
		else 
		{
			//Blob Kejar Player Hingga Mendekati Player (Jarak Blob Hingga Mendekati Player)
			if (Vector3.Distance (transform.position, Player.Player_Pos) < 0.8f && !Blob_Dead && !Player.player_Dead) 
			{
				/*
				if (Blob_IsTiming) 
				{
					timer += Time.deltaTime;
				}
				//Blob Menyerang Player Timernya Digunakan Untuk Delay Supaya Sprite Animator Selesai Dijalankan
				if (timer > 1.5f && !Blob_IsAttacking) 
				{
					endTimer ();
					ActiveLayer ("Blob_Attack");
					Player.HP_Minus = Player.Instance.Def / 2 - 10;
					//StartCoroutine (Attacking ());
					Blob_IsAttacking = true;
				} 
				else if (timer > 1.5f && Blob_IsAttacking) 
				{
					beginTimer ();
				}
				*/
				if (!Blob_IsAttacking) 
				{
					MyAttackTime += Time.deltaTime;
					ActiveLayer ("Blob_Walk");
				} 
				if (MyAttackTime > AttackCD && !Blob_IsAttacking && scene_Name.name != "Tutorial_2") 
				{
					MyAttackTime = 0;

					Random_Attack_Damage = Random.Range (10, 25);

                    Blob_Attack_Sound = true;

					StartCoroutine (Attacking ());

					Player.HP_Minus = Mathf.Ceil(Random_Attack_Damage - Player.Instance.Def);

					if (Player.HP_Minus <= 0) 
					{
						Player.HP_Minus = -10;
					}

					Player.Damage_In = Player.HP_Minus.ToString();

					Instantiate (objectnya_Txt, txt_Damage_Pos.position, Quaternion.identity);
				}

			} 
			else if (Vector3.Distance (transform.position, Player.Player_Pos) < Chase_Target && !Blob_Dead && !Blob_IsAttacking && !Player.player_Dead && scene_Name.name != "Tutorial_2") 
			{
				isChasing = true;
				Blob_Direction = Vector2.zero;
				transform.position = Vector2.MoveTowards (transform.position, Player.Player_Pos, Time.deltaTime);
				//BLOB ANIMATION X DAN Y SELALU 0
				//ActiveLayer ("Blob_Walk");
				//Blob_Animator.SetFloat ("x", transform.localPosition.x);
				//Blob_Animator.SetFloat ("y", transform.localPosition.y);
				if (transform.position.y >= Player.Player_Pos.y + 0.5f) 
				{
					Blob_Direction += Vector2.down;
				} 
				else if (transform.position.y + 0.5f <= Player.Player_Pos.y) 
				{
					Blob_Direction += Vector2.up;
				} 
				else if (transform.position.x >= Player.Player_Pos.x + 0.5f) 
				{
					Blob_Direction += Vector2.left;
				} 
				else if (transform.position.x <= Player.Player_Pos.x + 0.5f) 
				{
					Blob_Direction += Vector2.right;
				}
				Blob_Animator.SetFloat ("x", Blob_Direction.x);
				Blob_Animator.SetFloat ("y", Blob_Direction.y);
				ActiveLayer ("Blob_Walk");
			}
			else
			{
				isChasing = false;
			}
		}
	}

	public void ChooseDirection()
	{
		Blob_Direction = Vector2.zero;
		walkDirection = Random.Range (0, 4);
		if (walkDirection == 0) 
		{
			Blob_Direction += Vector2.up;
			myRidigBody.velocity = new Vector2 (0, 0.3f);
		}
		else if (walkDirection == 1) 
		{
			Blob_Direction += Vector2.right;
			myRidigBody.velocity = new Vector2 (0.3f, 0);
		}
		else if (walkDirection == 2) 
		{
			Blob_Direction += Vector2.down;
			myRidigBody.velocity = new Vector2 (0, -0.3f);
		}
		else if (walkDirection == 3) 
		{
			Blob_Direction += Vector2.left;
			myRidigBody.velocity = new Vector2 (-0.3f, 0);
		}
		Blob_Animator.SetFloat ("x", Blob_Direction.x);
		Blob_Animator.SetFloat ("y", Blob_Direction.y);
		ActiveLayer ("Blob_Walk");
	}

	IEnumerator Attacking()
	{
		myRidigBody.velocity = Vector2.zero;

		Blob_IsAttacking = true;
		ActiveLayer ("Blob_Attack");

		yield return new WaitForSeconds (Blob_Animator.GetCurrentAnimatorClipInfo(1).Length);

		Blob_IsAttacking = false;
	}

	IEnumerator Walking()
	{
		isWalking = true;
		ChooseDirection ();
		yield return new WaitForSeconds (Blob_Animator.GetCurrentAnimatorClipInfo(0).Length);
		isWalking = false;
		myRidigBody.velocity = Vector2.zero;
	}

    /*
	private void beginTimer()
	{
		timer = 0;
		Blob_IsAttacking = false;
		Blob_IsTiming = true;
	}

	private void endTimer()
	{
		Blob_IsTiming = false;
	}
	*/

	public void ActiveLayer(string Blob_layerName)
	{
		for (int i = 0; i < Blob_Animator.layerCount; i++) 
		{
			Blob_Animator.SetLayerWeight (i, 0);
		}
		Blob_Animator.SetLayerWeight (Blob_Animator.GetLayerIndex (Blob_layerName), 1);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Player_Attack_Pos")) 
		{
            if (scene_Name.name == "Tutorial_2")
            {
                ActiveLayer("Blob_Dead");
            }
            else
            {
                int Attack_Power = Random.Range(Player.Instance.Str - 5, Player.Instance.Str);
                Blob_HP.MycurrentValue_Blob = Blob_HP.MycurrentValue_Blob - Attack_Power;
                GameMaster.Enemy_Damage = Attack_Power.ToString();
                Instantiate(objectnya_Txt_Enemy, txt_Damage_Enemy_Pos.position, Quaternion.identity);
            }
		}
	}
}
