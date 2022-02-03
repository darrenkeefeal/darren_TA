using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour {

	public GameObject objectnya_Txt_Enemy;
	Transform txt_Damage_Enemy_Pos;

	public GameObject objectnya_Txt;
	Transform txt_Damage_Pos;

	private Rigidbody2D myRidigBody;

	[SerializeField]
	private Wolf_Stats Wolf_HP;
	private int Wolf_HP_Start = 145;

	private bool Wolf_Dead = false;

	public static float Wolf_Dead_X, Wolf_Dead_Y;

	[SerializeField]
	private float Chase_Target;

	private Animator Wolf_Animator;
	private Vector2 Wolf_Direction;

	private bool Wolf_IsAttacking;
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

    public GameObject objectnya_Crystal;

    public static bool wolf_Attack_Sound;
    public static bool wolf_Dead_Sound;

    // Use this for initialization
    public void Start () {
		quest_Manager = FindObjectOfType<Quest_Manager> ();

		myRidigBody = GetComponent<Rigidbody2D> ();

		Wolf_HP.Initialize (Wolf_HP_Start, Wolf_HP_Start);
		Wolf_Animator = GetComponent<Animator> ();

		txt_Damage_Pos = GameObject.Find ("Txt_Damage_Pos").GetComponent<Transform> ();
		txt_Damage_Enemy_Pos = GameObject.Find ("Txt_Damage_Enemy_Pos").GetComponent<Transform> ();

		ChooseDirection ();

		Wolf_IsAttacking = false;

		//beginTimer ();
	}

	// Update is called once per frame
	public void Update () {

		if (!isWalking && !isChasing && !Wolf_IsAttacking && !Wolf_Dead) 
		{
			StartCoroutine (Walking ());
		}
		//Check Blob Mati Tidak
		else if (Wolf_HP.MycurrentValue_Wolf <= 0 && !Wolf_Dead) 
		{
			Wolf_Dead = true;
			Wolf_Spawn.maxWolf = Wolf_Spawn.maxWolf - 1;
			Player.EXP_Plus += 20;

            if (!quest_Manager.quest_Completed[5] && quest_Manager.quests[5].gameObject.activeSelf)
            {
                int Ran_Drop_Quest = Random.Range(0, 100);
                if (Ran_Drop_Quest <= 5)
                {
                    Instantiate(objectnya_Crystal, this.transform.position, Quaternion.identity);
                }
            }

            wolf_Dead_Sound = true;

            random_Gold = Random.Range (50, 150);

			GameMaster.Player_Gold += random_Gold;
			Button_Shop.updateGold = true;

			Wolf_Spawn.Random_Drop = true;

			//Blob_Dead_X = transform.localPosition.x;
			//Blob_Dead_Y = transform.localPosition.y;

			myRidigBody.velocity = Vector2.zero;

			ActiveLayer ("Wolf_Dead");

			quest_Manager.enemyKilled = enemyQuestName;

			Destroy (gameObject, Wolf_Animator.GetCurrentAnimatorClipInfo(2).Length);
		}
		else 
		{
			//Blob Kejar Player Hingga Mendekati Player (Jarak Blob Hingga Mendekati Player)
			if (Vector3.Distance (transform.position, Player.Player_Pos) < 0.8f && !Wolf_Dead && !Player.player_Dead) 
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
				if (!Wolf_IsAttacking) 
				{
					MyAttackTime += Time.deltaTime;
					ActiveLayer ("Wolf_Walk");
				} 
				if (MyAttackTime > AttackCD && !Wolf_IsAttacking) 
				{
					MyAttackTime = 0;

					Random_Attack_Damage = Random.Range (30, 45);

                    wolf_Attack_Sound = true;

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
			else if (Vector3.Distance (transform.position, Player.Player_Pos) < Chase_Target && !Wolf_Dead && !Wolf_IsAttacking && !Player.player_Dead) 
			{
				isChasing = true;
				Wolf_Direction = Vector2.zero;
				transform.position = Vector2.MoveTowards (transform.position, Player.Player_Pos, Time.deltaTime);
				//BLOB ANIMATION X DAN Y SELALU 0
				//ActiveLayer ("Blob_Walk");
				//Blob_Animator.SetFloat ("x", transform.localPosition.x);
				//Blob_Animator.SetFloat ("y", transform.localPosition.y);
				if (transform.position.y >= Player.Player_Pos.y + 0.5f) 
				{
					Wolf_Direction += Vector2.down;
				} 
				else if (transform.position.y + 0.5f <= Player.Player_Pos.y) 
				{
					Wolf_Direction += Vector2.up;
				} 
				else if (transform.position.x >= Player.Player_Pos.x + 0.5f) 
				{
					Wolf_Direction += Vector2.left;
				} 
				else if (transform.position.x <= Player.Player_Pos.x + 0.5f) 
				{
					Wolf_Direction += Vector2.right;
				}
				Wolf_Animator.SetFloat ("x", Wolf_Direction.x);
				Wolf_Animator.SetFloat ("y", Wolf_Direction.y);
				ActiveLayer ("Wolf_Walk");
			}
			else
			{
				isChasing = false;
			}
		}
	}

	public void ChooseDirection()
	{
		Wolf_Direction = Vector2.zero;
		walkDirection = Random.Range (0, 4);
		if (walkDirection == 0) 
		{
			Wolf_Direction += Vector2.up;
			myRidigBody.velocity = new Vector2 (0, 0.3f);
		}
		else if (walkDirection == 1) 
		{
			Wolf_Direction += Vector2.right;
			myRidigBody.velocity = new Vector2 (0.3f, 0);
		}
		else if (walkDirection == 2) 
		{
			Wolf_Direction += Vector2.down;
			myRidigBody.velocity = new Vector2 (0, -0.3f);
		}
		else if (walkDirection == 3) 
		{
			Wolf_Direction += Vector2.left;
			myRidigBody.velocity = new Vector2 (-0.3f, 0);
		}
		Wolf_Animator.SetFloat ("x", Wolf_Direction.x);
		Wolf_Animator.SetFloat ("y", Wolf_Direction.y);
		ActiveLayer ("Wolf_Walk");
	}

	IEnumerator Attacking()
	{
		myRidigBody.velocity = Vector2.zero;

		Wolf_IsAttacking = true;
		ActiveLayer ("Wolf_Attack");

		yield return new WaitForSeconds (Wolf_Animator.GetCurrentAnimatorClipInfo(1).Length);

		Wolf_IsAttacking = false;
	}

	IEnumerator Walking()
	{
		isWalking = true;
		ChooseDirection ();
		yield return new WaitForSeconds (Wolf_Animator.GetCurrentAnimatorClipInfo(0).Length);
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

	public void ActiveLayer(string Wolf_layerName)
	{
		for (int i = 0; i < Wolf_Animator.layerCount; i++) 
		{
			Wolf_Animator.SetLayerWeight (i, 0);
		}
		Wolf_Animator.SetLayerWeight (Wolf_Animator.GetLayerIndex (Wolf_layerName), 1);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Player_Attack_Pos")) 
		{
			int Attack_Power = Random.Range(Player.Instance.Str - 5 , Player.Instance.Str);
			Wolf_HP.MycurrentValue_Wolf = Wolf_HP.MycurrentValue_Wolf - Attack_Power;
			GameMaster.Enemy_Damage = Attack_Power.ToString ();
			Instantiate (objectnya_Txt_Enemy, txt_Damage_Enemy_Pos.position, Quaternion.identity);
		}
	}
}
