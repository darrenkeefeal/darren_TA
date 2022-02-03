using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goblin : MonoBehaviour {
    
	public GameObject objectnya_Txt_Enemy;
	Transform txt_Damage_Enemy_Pos;

	public GameObject objectnya_Txt;
	Transform txt_Damage_Pos;

	private Rigidbody2D myRidigBody;

	[SerializeField]
	private Goblin_Stats Goblin_HP;
	private int Goblin_HP_Start = 220;

	private bool Goblin_Dead = false;

	public static float Goblin_Dead_X, Goblin_Dead_Y;

	[SerializeField]
	private float Chase_Target;

	private Animator Goblin_Animator;
	private Vector2 Goblin_Direction;

	private bool Goblin_IsAttacking;
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

    public GameObject objectnya_Relic;

    public static bool goblin_Attack_Sound;
    public static bool goblin_Dead_Sound;

    // Use this for initialization
    public void Start () {
		quest_Manager = FindObjectOfType<Quest_Manager> ();

		myRidigBody = GetComponent<Rigidbody2D> ();

		Goblin_HP.Initialize (Goblin_HP_Start, Goblin_HP_Start);
		Goblin_Animator = GetComponent<Animator> ();

		txt_Damage_Pos = GameObject.Find ("Txt_Damage_Pos").GetComponent<Transform> ();
		txt_Damage_Enemy_Pos = GameObject.Find ("Txt_Damage_Enemy_Pos").GetComponent<Transform> ();

		ChooseDirection ();

		Goblin_IsAttacking = false;

		//beginTimer ();
	}

	// Update is called once per frame
	public void Update () {

		if (!isWalking && !isChasing && !Goblin_IsAttacking && !Goblin_Dead) 
		{
			StartCoroutine (Walking ());
		}
		//Check Blob Mati Tidak
		else if (Goblin_HP.MycurrentValue_Goblin <= 0 && !Goblin_Dead) 
		{
			Goblin_Dead = true;
			Goblin_Spawn.maxGoblin = Goblin_Spawn.maxGoblin - 1;
			Player.EXP_Plus += 35;

            if (!quest_Manager.quest_Completed[8] && quest_Manager.quests[8].gameObject.activeSelf)
            {
                int Ran_Drop_Quest = Random.Range(0, 100);
                if (Ran_Drop_Quest <= 5)
                {
                    Instantiate(objectnya_Relic, this.transform.position, Quaternion.identity);
                }
            }

            goblin_Dead_Sound = true;

            random_Gold = Random.Range (150, 200);

			GameMaster.Player_Gold += random_Gold;
			Button_Shop.updateGold = true;

			Goblin_Spawn.Random_Drop = true;

			//Blob_Dead_X = transform.localPosition.x;
			//Blob_Dead_Y = transform.localPosition.y;

			myRidigBody.velocity = Vector2.zero;

			ActiveLayer ("Goblin_Dead");

			quest_Manager.enemyKilled = enemyQuestName;

			Destroy (gameObject, Goblin_Animator.GetCurrentAnimatorClipInfo(2).Length);
		}
		else 
		{
			//Blob Kejar Player Hingga Mendekati Player (Jarak Blob Hingga Mendekati Player)
			if (Vector3.Distance (transform.position, Player.Player_Pos) < 1f && !Goblin_Dead && !Player.player_Dead) 
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
				if (!Goblin_IsAttacking) 
				{
					MyAttackTime += Time.deltaTime;
					ActiveLayer ("Goblin_Walk");
				} 
				if (MyAttackTime > AttackCD && !Goblin_IsAttacking) 
				{
					MyAttackTime = 0;

					Random_Attack_Damage = Random.Range (30, 45);

                    goblin_Attack_Sound = true;

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
			else if (Vector3.Distance (transform.position, Player.Player_Pos) < Chase_Target && !Goblin_Dead && !Goblin_IsAttacking && !Player.player_Dead) 
			{
				isChasing = true;
				Goblin_Direction = Vector2.zero;
				transform.position = Vector2.MoveTowards (transform.position, Player.Player_Pos, Time.deltaTime);
				//BLOB ANIMATION X DAN Y SELALU 0
				//ActiveLayer ("Blob_Walk");
				//Blob_Animator.SetFloat ("x", transform.localPosition.x);
				//Blob_Animator.SetFloat ("y", transform.localPosition.y);
				if (transform.position.y >= Player.Player_Pos.y + 0.5f) 
				{
					Goblin_Direction += Vector2.down;
				} 
				else if (transform.position.y + 0.5f <= Player.Player_Pos.y) 
				{
					Goblin_Direction += Vector2.up;
				} 
				else if (transform.position.x >= Player.Player_Pos.x + 0.5f) 
				{
					Goblin_Direction += Vector2.left;
				} 
				else if (transform.position.x <= Player.Player_Pos.x + 0.5f) 
				{
					Goblin_Direction += Vector2.right;
				}
				Goblin_Animator.SetFloat ("x", Goblin_Direction.x);
				Goblin_Animator.SetFloat ("y", Goblin_Direction.y);
				ActiveLayer ("Goblin_Walk");
			}
			else
			{
				isChasing = false;
			}
		}
	}

	public void ChooseDirection()
	{
		Goblin_Direction = Vector2.zero;
		walkDirection = Random.Range (0, 4);
		if (walkDirection == 0) 
		{
			Goblin_Direction += Vector2.up;
			myRidigBody.velocity = new Vector2 (0, 0.3f);
		}
		else if (walkDirection == 1) 
		{
			Goblin_Direction += Vector2.right;
			myRidigBody.velocity = new Vector2 (0.3f, 0);
		}
		else if (walkDirection == 2) 
		{
			Goblin_Direction += Vector2.down;
			myRidigBody.velocity = new Vector2 (0, -0.3f);
		}
		else if (walkDirection == 3) 
		{
			Goblin_Direction += Vector2.left;
			myRidigBody.velocity = new Vector2 (-0.3f, 0);
		}
		Goblin_Animator.SetFloat ("x", Goblin_Direction.x);
		Goblin_Animator.SetFloat ("y", Goblin_Direction.y);
		ActiveLayer ("Goblin_Walk");
	}

	IEnumerator Attacking()
	{
		myRidigBody.velocity = Vector2.zero;

		Goblin_IsAttacking = true;
		ActiveLayer ("Goblin_Attack");

		yield return new WaitForSeconds (Goblin_Animator.GetCurrentAnimatorClipInfo(1).Length);

		Goblin_IsAttacking = false;
	}

	IEnumerator Walking()
	{
		isWalking = true;
		ChooseDirection ();
		yield return new WaitForSeconds (Goblin_Animator.GetCurrentAnimatorClipInfo(0).Length);
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
		for (int i = 0; i < Goblin_Animator.layerCount; i++) 
		{
			Goblin_Animator.SetLayerWeight (i, 0);
		}
		Goblin_Animator.SetLayerWeight (Goblin_Animator.GetLayerIndex (Wolf_layerName), 1);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Player_Attack_Pos")) 
		{
			int Attack_Power = Random.Range(Player.Instance.Str - 5 , Player.Instance.Str);
			Goblin_HP.MycurrentValue_Goblin = Goblin_HP.MycurrentValue_Goblin - Attack_Power;
			GameMaster.Enemy_Damage = Attack_Power.ToString ();

			Instantiate (objectnya_Txt_Enemy, txt_Damage_Enemy_Pos.position, Quaternion.identity);
		}
	}
}
