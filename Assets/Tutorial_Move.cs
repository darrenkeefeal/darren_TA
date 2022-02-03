using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial_Move : Characters
{
    public static GameObject Attacking_Pos;

    public static bool Btn_Player_Attack;
    public bool OkAttack;

    public static bool Attacking = false;

    // Use this for initialization
    protected override void Start()
    {
        Attacking_Pos = GameObject.Find("Attack_Pos");

        Player.Player_Move = null;

        OkAttack = true;

        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        GetInput();

        base.Update();
    }

    public void GetInput()
    {
        direction = Vector2.zero;

        if (!isDead && !GameMaster.Shop_Open && !GameMaster.loadingScreen)
        {
            if (Input.GetKey(KeyCode.W) || Player.Player_Move == "Up")
            {
                direction += Vector2.up;
                Player_Attack_Pos(0, 0.6f);
            }
            if (Input.GetKey(KeyCode.A) || Player.Player_Move == "Left")
            {
                direction += Vector2.left;
                Player_Attack_Pos(-0.5f, 0);
            }
            if (Input.GetKey(KeyCode.S) || Player.Player_Move == "Down")
            {
                direction += Vector2.down;
                Player_Attack_Pos(0, -0.6f);
            }
            if (Input.GetKey(KeyCode.D) || Player.Player_Move == "Right")
            {
                direction += Vector2.right;
                Player_Attack_Pos(0.5f, 0);
            }
            if (Input.GetKeyDown(KeyCode.Space) && OkAttack || Btn_Player_Attack && OkAttack)
            {
                StartCoroutine(Attack());
                //audio_Sword = true;

                Btn_Player_Attack = false;
            }
        }
    }

    private void Player_Attack_Pos(float PosX, float PosY)
    {
        Vector3 thePosition = transform.localPosition;
        thePosition.x = PosX;
        thePosition.y = PosY;
        Attacking_Pos.transform.localPosition = thePosition;
    }

    private IEnumerator Attack()
    {
        if (!isAttack && !isMoving)
        {
            isAttack = true;
            Attacking = true;
            animator.SetBool("Attack", isAttack);
            //Untuk Waktu Menjalankan Sesuatu
            yield return new WaitForSeconds(0.5f);
            StopAttack();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Tutorial_2"))
        {
            SceneManager.LoadScene("Tutorial_2");
        }
    }
}