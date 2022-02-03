using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour {

	public GameObject objectnya_Txt_Enemy;
	Transform txt_Damage_Enemy_Pos;

	public GameObject objectnya_Txt;
	Transform txt_Damage_Pos;

	private Rigidbody2D myRidigBody;

	[SerializeField]
	private Boss_Stat Boss_HP;
	private int Boss_HP_Start = 750;

	private bool Boss_Dead = false;

	public static float Boss_Dead_X, Boss_Dead_Y;

	[SerializeField]
	private float Chase_Target;

	private Animator Boss_Animator;
	private Vector2 Boss_Direction;

	private bool Boss_IsAttacking;
	//private bool Blob_IsTiming;
	//private float timer;

	private int walkDirection;

	private bool isWalking;
	private bool isChasing;

	private float MyAttackTime;
	private int Random_Attack_Damage;
	private float AttackCD = 1.2f;

	private int random_Gold;

	public string enemyQuestName;
	private Quest_Manager quest_Manager;

    public static bool orc_Attack_Sound;
    public static bool orc_Dead_Sound;

    public static float HP_Temp;

    // Use this for initialization
    public void Start () {
		quest_Manager = FindObjectOfType<Quest_Manager> ();

		myRidigBody = GetComponent<Rigidbody2D> ();

		Boss_HP.Initialize (Boss_HP_Start, Boss_HP_Start);
		Boss_Animator = GetComponent<Animator> ();

		txt_Damage_Pos = GameObject.Find ("Txt_Damage_Pos").GetComponent<Transform> ();
		txt_Damage_Enemy_Pos = GameObject.Find ("Txt_Damage_Enemy_Pos").GetComponent<Transform> ();

		ChooseDirection ();

		Boss_IsAttacking = false;

		//beginTimer ();
	}
    
	// Update is called once per frame
	public void Update () {
        HP_Temp = Boss_HP.MycurrentValue_Boss;

        if (!isWalking && !isChasing && !Boss_IsAttacking && !Boss_Dead) 
		{
			StartCoroutine (Walking ());
		}
		//Check Blob Mati Tidak
		else if (Boss_HP.MycurrentValue_Boss <= 0 && !Boss_Dead) 
		{
			Boss_Dead = true;
			Player.EXP_Plus += 500;

			random_Gold = Random.Range (1000, 1500);

            orc_Dead_Sound = true;

            GameMaster.Player_Gold += random_Gold;
			Button_Shop.updateGold = true;

			Boss_Spawn.Random_Drop = true;

			//Blob_Dead_X = transform.localPosition.x;
			//Blob_Dead_Y = transform.localPosition.y;

			myRidigBody.velocity = Vector2.zero;

			ActiveLayer ("Boss_Dead");

			quest_Manager.enemyKilled = enemyQuestName;

            if (GameMaster.EndGame != "yes")
            {
                WWWForm form_end = new WWWForm();
                form_end.AddField("username", GameMaster.player_Name);
                form_end.AddField("end", "yes");

                WWW url_Save_End = new WWW("http://keefem.000webhostapp.com/End_User.php", form_end);
            }

            //Destroy (gameObject, Boss_Animator.GetCurrentAnimatorClipInfo(2).Length);

            StartCoroutine(Story_End());
		}
		else 
		{
			//Blob Kejar Player Hingga Mendekati Player (Jarak Blob Hingga Mendekati Player)
			if (Vector3.Distance (transform.position, Player.Player_Pos) < 2f && !Boss_Dead && !Player.player_Dead) 
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
				if (!Boss_IsAttacking) 
				{
					MyAttackTime += Time.deltaTime;
					ActiveLayer ("Boss_Walk");
				} 
				if (MyAttackTime > AttackCD && !Boss_IsAttacking) 
				{
					MyAttackTime = 0;

					Random_Attack_Damage = Random.Range (40, 55);

                    orc_Attack_Sound = true;

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
			else if (Vector3.Distance (transform.position, Player.Player_Pos) < Chase_Target && !Boss_Dead && !Boss_IsAttacking && !Player.player_Dead) 
			{
				isChasing = true;
				Boss_Direction = Vector2.zero;
				transform.position = Vector2.MoveTowards (transform.position, Player.Player_Pos, Time.deltaTime);
				//BLOB ANIMATION X DAN Y SELALU 0
				//ActiveLayer ("Blob_Walk");
				//Blob_Animator.SetFloat ("x", transform.localPosition.x);
				//Blob_Animator.SetFloat ("y", transform.localPosition.y);
				if (transform.position.y >= Player.Player_Pos.y + 0.5f) 
				{
					Boss_Direction += Vector2.down;
				} 
				else if (transform.position.y + 0.5f <= Player.Player_Pos.y) 
				{
					Boss_Direction += Vector2.up;
				} 
				else if (transform.position.x >= Player.Player_Pos.x + 0.5f) 
				{
					Boss_Direction += Vector2.left;
				} 
				else if (transform.position.x <= Player.Player_Pos.x + 0.5f) 
				{
					Boss_Direction += Vector2.right;
				}
				Boss_Animator.SetFloat ("x", Boss_Direction.x);
				Boss_Animator.SetFloat ("y", Boss_Direction.y);
				ActiveLayer ("Boss_Walk");
			}
			else
			{
				isChasing = false;
			}
		}
    }

    IEnumerator Story_End()
    {
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("Story_2");
        //Video_2.ending_Story = true;
    }

    public void ChooseDirection()
	{
		Boss_Direction = Vector2.zero;
		walkDirection = Random.Range (0, 4);
		if (walkDirection == 0) 
		{
			Boss_Direction += Vector2.up;
			myRidigBody.velocity = new Vector2 (0, 0.3f);
		}
		else if (walkDirection == 1) 
		{
			Boss_Direction += Vector2.right;
			myRidigBody.velocity = new Vector2 (0.3f, 0);
		}
		else if (walkDirection == 2) 
		{
			Boss_Direction += Vector2.down;
			myRidigBody.velocity = new Vector2 (0, -0.3f);
		}
		else if (walkDirection == 3) 
		{
			Boss_Direction += Vector2.left;
			myRidigBody.velocity = new Vector2 (-0.3f, 0);
		}
		Boss_Animator.SetFloat ("x", Boss_Direction.x);
		Boss_Animator.SetFloat ("y", Boss_Direction.y);
		ActiveLayer ("Boss_Walk");
	}

	IEnumerator Attacking()
	{
		myRidigBody.velocity = Vector2.zero;

		Boss_IsAttacking = true;
		ActiveLayer ("Boss_Attack");

		yield return new WaitForSeconds (Boss_Animator.GetCurrentAnimatorClipInfo(1).Length);

		Boss_IsAttacking = false;
	}

	IEnumerator Walking()
	{
		isWalking = true;
		ChooseDirection ();
		yield return new WaitForSeconds (Boss_Animator.GetCurrentAnimatorClipInfo(0).Length);
		isWalking = false;
		myRidigBody.velocity = Vector2.zero;
	}

	/*
	private void beginTimer()
	{
		timer = 0;
		Boss_IsAttacking = false;
		Boss_IsTiming = true;
	}

	private void endTimer()
	{
		Boss_IsTiming = false;
	}
	*/

	public void ActiveLayer(string Blob_layerName)
	{
		for (int i = 0; i < Boss_Animator.layerCount; i++) 
		{
			Boss_Animator.SetLayerWeight (i, 0);
		}
		Boss_Animator.SetLayerWeight (Boss_Animator.GetLayerIndex (Blob_layerName), 1);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Player_Attack_Pos")) 
		{
			int Attack_Power = Random.Range(Player.Instance.Str - 5 , Player.Instance.Str);
			Boss_HP.MycurrentValue_Boss = Boss_HP.MycurrentValue_Boss - Attack_Power;
			GameMaster.Enemy_Damage = Attack_Power.ToString ();
			Instantiate (objectnya_Txt_Enemy, txt_Damage_Enemy_Pos.position, Quaternion.identity);
		}
	}
}
