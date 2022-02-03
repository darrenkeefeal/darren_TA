using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Object : MonoBehaviour {

	public int questNumber;

	public Quest_Manager quest_Manager;

    public static string Temp_Information_Quest;

	public string startText;
	public string endText;
	public string infoText;
	public int questReward;

	public bool isItemQuest;
	public string targetItem;

	public bool isEnemyQuest;
	public string enemyTarget;
	public int enemyToKill;
	public static int enemyCountKill;

	public string playerBuff;
	public static string playerBuff_Global;
    
    public static int Realationship_Guard = 1;
    public static int Realationship_Old_Woman = 1;

	// Use this for initialization
	void Start ()
    {
        if (GameMaster.Questing_Global >= 1)
        {
            if (GameMaster.Questing_Global >= 10)
            {
                GameMaster.Questing_Global = 1;
            }
            Temp_Information_Quest = quest_Manager.quests[GameMaster.Questing_Global].startText.ToString();
        }

        endText = endText + "\n Reward : " + questReward.ToString () + "G";
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isEnemyQuest) 
		{
			if (quest_Manager.enemyKilled == enemyTarget) 
			{
				quest_Manager.enemyKilled = null;
				enemyCountKill++;

				infoText = enemyCountKill + " / " + enemyToKill + " " + enemyTarget + " Killed";

				quest_Manager.ShowQuestText (infoText);
			}

			if (enemyCountKill >= enemyToKill) 
			{
				GameMaster.Player_Gold += questReward;
				Button_Shop.updateGold = true;
				EndQuest ();
			}
		}
		else if(isItemQuest)
		{
			if (quest_Manager.itemCollected == targetItem) 
			{
				quest_Manager.itemCollected = null;

				EndQuest ();
			}
		}
	}

	public void StartQuest()
	{
        //Save.save_Anything_Player = true;

        quest_Manager.ShowQuestText (startText);

        Temp_Information_Quest = startText;
        
        if (playerBuff == "Attack") 
		{
            playerBuff_Global = playerBuff;
            Player.Instance.SetStats ((3), 0);
		}
		else if (playerBuff == "Deff") 
		{
            playerBuff_Global = playerBuff;
            Player.Instance.SetStats (0, 3);
		}
	}

	public void EndQuest()
	{
        //Save.save_Anything_Player = true;

		quest_Manager.ShowQuestText (endText);
		quest_Manager.quest_Completed [questNumber] = true;
		quest_Manager.quest_Done [questNumber] = true;

        Temp_Information_Quest = "None";
        
        GameObject.Find ("Quest_Trigger_" + questNumber).SetActive (false);

        if (questNumber + 1 >= 9)
        {
            
        }
        else
        {
            GameObject.Find("Quest_Trigger_" + (questNumber + 1)).GetComponent<BoxCollider2D>().enabled = true;
        }

		GameMaster.Questing_Global++;

		playerBuff_Global = string.Empty;

		if (playerBuff == "Attack") 
		{
			Player.Instance.SetStats (-3, 0);
            Realationship_Guard = Realationship_Guard + 1;
        }
		else if (playerBuff == "Deff") 
		{
            Player.Instance.SetStats (0, -3);
            Realationship_Old_Woman = Realationship_Old_Woman + 1;
        }

		playerBuff = string.Empty;

		enemyCountKill = 0;
		gameObject.SetActive (false);

		Quest_Trigger.questing_Number++; 
	}
}
