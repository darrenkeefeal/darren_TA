using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Btn_Player : MonoBehaviour {

    Scene reset_Scene;

    private bool Delay_Attack;
    private float decay;

    float timer_Quit;
    bool start_Timer = false;

    // Use this for initialization
    void Start()
    {
        Delay_Attack = false;
        reset_Scene = SceneManager.GetActiveScene();
    }

    public static void ins()
    {
        /*
        Instantiate(Resources.Load("Txt_Confirm_Quit") as GameObject,Camera.main.transform.position,Quaternion.identity);
        if (GameObject.Find("Pos_Confirm") != null)
        {
            Vector3 pos_Temp = GameObject.Find("Pos_Confirm").gameObject.transform.position;
            
            //t.transform.SetParent(pos_Temp.x,pos_Temp.y,false);
            //t.transform.parent = GameObject.Find("Pos_Confirm").GetComponent<Transform>().transform;
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        //AppLogout();
        if (start_Timer)
        {
            timer_Quit += Time.deltaTime;
            if (timer_Quit >= 2f)
            {
                start_Timer = false;
                ctr_Quit = 0;
            }
        }

        Reset();
    }

    private void Reset()
    {
        if (Delay_Attack && decay > 0)
        {
            decay -= Time.deltaTime;
        }
        if (decay < 0)
        {
            decay = 0;
            Delay_Attack = false;
        }
    }

    static void Saveall()
    {
        Save.save_Anything_Farm = true;
        Save.save_Anything_Player = true;

        System.Threading.Thread.Sleep(100);
    }

    int ctr_Quit = 0;

    public void AppLogout()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ctr_Quit++;
            if (ctr_Quit >= 2)
            {
                this.gameObject.SetActive(false);

                Saveall();
                Application.Quit();
            }
            else
            {
                ins();
                start_Timer = true;
            }
        }
        */

        this.gameObject.SetActive(false);

        Saveall();
        Application.Quit();
    }

    void ResetAccount()
    {
        Array.Clear(Player.user, 0, Player.user.Length);
        Array.Clear(Player.items_user, 0, Player.items_user.Length);
        Array.Clear(Player.times, 0, Player.times.Length);
        Array.Clear(Player.durability, 0, Player.durability.Length);
        Array.Clear(Player.user_Data, 0, Player.user_Data.Length);
        Array.Clear(Player.items_user_Data, 0, Player.items_user_Data.Length);
        Array.Clear(Player.times_Data, 0, Player.times_Data.Length);
        Array.Clear(Player.durability_Data, 0, Player.durability_Data.Length);

        Array.Clear(Player.lvl_Data, 0, Player.lvl_Data.Length);
        Array.Clear(Player.exp_Data, 0, Player.exp_Data.Length);
        Array.Clear(Player.gold_Data, 0, Player.gold_Data.Length);

        Array.Clear(Player.item_Data, 0, Player.item_Data.Length);
        Array.Clear(Player.equipment_Durability_Data, 0, Player.equipment_Durability_Data.Length);

        Array.Clear(Player.farm1_Data, 0, Player.farm1_Data.Length);
        Array.Clear(Player.farm2_Data, 0, Player.farm2_Data.Length);
        Array.Clear(Player.farm3_Data, 0, Player.farm3_Data.Length);
        Array.Clear(Player.farm4_Data, 0, Player.farm4_Data.Length);
        Array.Clear(Player.farm5_Data, 0, Player.farm5_Data.Length);
        Array.Clear(Player.farm6_Data, 0, Player.farm6_Data.Length);
        Array.Clear(Player.farm7_Data, 0, Player.farm7_Data.Length);
        Array.Clear(Player.farm8_Data, 0, Player.farm8_Data.Length);
        Array.Clear(Player.farm9_Data, 0, Player.farm9_Data.Length);

        Array.Clear(Player.farm1_Type, 0, Player.farm1_Type.Length);
        Array.Clear(Player.farm2_Type, 0, Player.farm2_Type.Length);
        Array.Clear(Player.farm3_Type, 0, Player.farm3_Type.Length);
        Array.Clear(Player.farm4_Type, 0, Player.farm4_Type.Length);
        Array.Clear(Player.farm5_Type, 0, Player.farm5_Type.Length);
        Array.Clear(Player.farm6_Type, 0, Player.farm6_Type.Length);
        Array.Clear(Player.farm7_Type, 0, Player.farm7_Type.Length);
        Array.Clear(Player.farm8_Type, 0, Player.farm8_Type.Length);
        Array.Clear(Player.farm9_Type, 0, Player.farm9_Type.Length);

        Array.Clear(Player.quest_Data, 0, Player.quest_Data.Length);
        Array.Clear(Player.quest_Kill_Data, 0, Player.quest_Kill_Data.Length);

        GameMaster.player_Name = "";

        System.Threading.Thread.Sleep(100);

        SceneManager.LoadScene("Main_Menu");
    }

    public void Information()
    {
        Player.Btn_Player_Informations = true;
    }

    public void Cancel()
    {
        Player.Player_Move = null;
    }

    public void Up()
    {
        Player.Player_Move = "Up";
    }

    public void Down()
    {
        Player.Player_Move = "Down";
    }

    public void Left()
    {
        Player.Player_Move = "Left";
    }

    public void Right()
    {
        Player.Player_Move = "Right";
    }

    public void Profile()
    {
        Player.Btn_Player_Profile = true;
    }

    public void Sword()
    {
        if (!Delay_Attack)
        {
            decay = 1f;
            Delay_Attack = true;
            Player.Btn_Player_Attack = true;
            Tutorial_Move.Btn_Player_Attack = true;
        }
    }

    public void FinishTutorial()
    {
        GameMaster.newGame = true;
        GameMaster.tutorial = false;
        SceneManager.LoadScene("Loading_Screen");
    }

    public void Inventory()
    {
        Player.Btn_Player_Inventory = true;
    }
}
