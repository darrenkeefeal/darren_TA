using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest_Trigger : MonoBehaviour {

	public GameObject Sign;

	private Quest_Manager quest_Manager;

	public CanvasGroup Yes_No_Button;
	public Text txt_Quest;

	public static int quest_Number;

	public bool startQuest;
	public bool endQuest;

	Vector2 position;
	public static int ctrSign = 0;

	public static int questing_Number = 0;

	// Use this for initialization
	void Start () 
	{
		quest_Manager = FindObjectOfType<Quest_Manager> ();

		if (GameMaster.field) 
		{
			position = new Vector2 (GameObject.Find ("Quest_Trigger_0").transform.localPosition.x - 0.7f, GameObject.Find ("Quest_Trigger_0").transform.localPosition.y - 0.3f);
		}
		else if (GameMaster.city) 
		{
			position = new Vector2 (GameObject.Find ("Quest_Trigger_3").transform.localPosition.x + 8.6f, GameObject.Find ("Quest_Trigger_3").transform.localPosition.y + 9.6f);
		}

		GameObject.Find ("Btn_Yes").GetComponent<Button> ().onClick.AddListener (delegate {
			AcceptQuest ();
		});

		GameObject.Find ("Btn_No").GetComponent<Button> ().onClick.AddListener (delegate {
			DeclineQuest ();
		});
				
		if (GameMaster.Questing_Global <= 2 && GameMaster.field) 
		{
			GameObject.Find ("Quest_Trigger_" + GameMaster.Questing_Global).GetComponent<BoxCollider2D> ().enabled = true;
		}
		else if (GameMaster.Questing_Global > 2 && GameMaster.Questing_Global <= 5 && GameMaster.city) 
		{
			GameObject.Find ("Quest_Trigger_" + GameMaster.Questing_Global).GetComponent<BoxCollider2D> ().enabled = true;
		}
    }
	
	// Update is called once per frame
	void Update () 
	{
        /*
		if (!quest_Manager.quest_Completed [quest_Number]) 
		{
			if (startQuest && !quest_Manager.quests [quest_Number].gameObject.activeSelf) 
			{
				quest_Manager.quests [quest_Number].gameObject.SetActive (true);
				quest_Manager.quests [quest_Number].StartQuest ();
			}
			if (endQuest && quest_Manager.quests [quest_Number].gameObject.activeSelf) 
			{
				quest_Manager.quests [quest_Number].EndQuest ();
			}
		}
		*/
        
		if (Yes_No_Button == null) 
		{
			Yes_No_Button = GameObject.Find ("Yes_No_Button").GetComponent<CanvasGroup> ();
		}
		if (txt_Quest == null) 
		{
			txt_Quest = GameObject.Find ("Text_Yes_No").GetComponent<Text> ();
		}

		if (ctrSign == 0 && GameMaster.field) 
		{
			Instantiate (Sign, position, Quaternion.identity);
			ctrSign = -1;
		}
		if (ctrSign == 1 && GameMaster.city) 
		{
			Instantiate (Sign, position, Quaternion.identity);
			ctrSign = -1;
		}

		if (GameObject.FindGameObjectsWithTag ("Sign").Length > 1) 
		{
			Destroy (GameObject.FindGameObjectWithTag ("Sign"));
		}

		if (Yes_No_Button.alpha == 1) 
		{
			GameObject.Find ("Canvas_Button_Yes_No").GetComponent<Canvas> ().sortingOrder = 4;
		}
		else if (Yes_No_Button.alpha == 0) 
		{
			GameObject.Find ("Canvas_Button_Yes_No").GetComponent<Canvas> ().sortingOrder = -1;
		}
	}

	public void DeclineQuest()
	{
		Yes_No_Button.alpha = 0;
	}

	public void Quest_Mission()
	{
		if (!quest_Manager.quest_Completed [quest_Number] && Yes_No_Button.alpha == 1) 
		{
			if (startQuest && !quest_Manager.quests [quest_Number].gameObject.activeSelf) 
			{
				quest_Manager.quests [quest_Number].gameObject.SetActive (true);
				quest_Manager.quests [quest_Number].StartQuest ();
			}
			if (endQuest && quest_Manager.quests [quest_Number].gameObject.activeSelf) 
			{
				quest_Manager.quests [quest_Number].EndQuest ();
			}

			if (quest_Number + 1 < quest_Manager.quests.Length) 
			{
				quest_Number++;
			}

			GameObject.Find ("Quest_Trigger_" + (quest_Number)).GetComponent<BoxCollider2D> ().enabled = false;
		}

		Yes_No_Button.alpha = 0;
	}

	public void AcceptQuest()
	{
		if (quest_Number == 0) 
		{
			Quest_Mission ();
		}
		else if(quest_Number == 1 && quest_Manager.quest_Completed[quest_Number - 1])
		{
            Quest_Mission ();
		}
		else if (quest_Number == 2 && quest_Manager.quest_Completed[quest_Number - 1]) 
		{
			Quest_Mission ();
		}
		else if (quest_Number == 3 && quest_Manager.quest_Completed[quest_Number - 1]) 
		{
			Quest_Mission ();
		}	
		else if (quest_Number == 4 && quest_Manager.quest_Completed[quest_Number - 1]) 
		{
			Quest_Mission ();
		}
		else if (quest_Number == 5 && quest_Manager.quest_Completed[quest_Number - 1]) 
		{
			Quest_Mission ();
		}
        else if (quest_Number == 6 && quest_Manager.quest_Completed[quest_Number - 1])
        {
            Quest_Mission();
        }
        else if (quest_Number == 7 && quest_Manager.quest_Completed[quest_Number - 1])
        {
            Quest_Mission();
        }
        else if (quest_Number == 8 && quest_Manager.quest_Completed[quest_Number - 1])
        {
            Quest_Mission();
        }
        else if (quest_Number == 9 && quest_Manager.quest_Completed[quest_Number - 1])
        {
            Quest_Mission();
        }
    }

	void OnTriggerEnter2D(Collider2D other)
	{
        if (GameMaster.Questing_Global > 0)
        {
            quest_Number = GameMaster.Questing_Global;
        }

		if (other.gameObject.name == "Player" && GameMaster.field) 
		{
			if (quest_Number == 0) 
			{
				Yes_No_Button.alpha = 1;

				txt_Quest.text = "Quest : " + quest_Manager.quests[quest_Number].startText.ToString() + "\n Buff : " + quest_Manager.quests[quest_Number].playerBuff + "(" + Quest_Object.Realationship_Guard.ToString() + ")" + "\n" + "Reward : " + quest_Manager.quests[quest_Number].questReward.ToString() + "G"  + "\n Do You Want To Accept The Quest ?";
			}
			else if (quest_Number == 1 && quest_Manager.quest_Completed[quest_Number - 1]) 
			{
                if (quest_Manager.quest_Completed[quest_Number - 1] == true) 
				{
					Yes_No_Button.alpha = 1;

					txt_Quest.text = "Quest : " + quest_Manager.quests[quest_Number].startText.ToString() + "\n Buff : " + quest_Manager.quests[quest_Number].playerBuff +"(" + Quest_Object.Realationship_Guard.ToString() + ")" + "\n" + "Reward : " + quest_Manager.quests[quest_Number].questReward.ToString() + "G"  + "\n Do You Want To Accept The Quest ?";
				}
			}
			else if (quest_Number == 2 && quest_Manager.quest_Completed[quest_Number - 1]) 
			{
				if (quest_Manager.quest_Completed[quest_Number - 1] == true) 
				{
					Yes_No_Button.alpha = 1;

					txt_Quest.text = "Quest : " + quest_Manager.quests[quest_Number].startText.ToString() + "\n Buff : " + quest_Manager.quests[quest_Number].playerBuff + "(" + Quest_Object.Realationship_Guard.ToString() + ")" + "\n" + "Reward : " +quest_Manager.quests[quest_Number].questReward.ToString() + "G"  + "\n Do You Want To Accept The Quest ?";
				}
			}
				
			/*
			if (!quest_Manager.quest_Completed [quest_Number]) 
			{
				if (startQuest && !quest_Manager.quests [quest_Number].gameObject.activeSelf) 
				{
					quest_Manager.quests [quest_Number].gameObject.SetActive (true);
					quest_Manager.quests [quest_Number].StartQuest ();
				}
				if (endQuest && quest_Manager.quests [quest_Number].gameObject.activeSelf) 
				{
					quest_Manager.quests [quest_Number].EndQuest ();
				}
			}
			*/
			
		}
		else if (other.gameObject.name == "Player" && GameMaster.city) 
		{
			if (quest_Number == 3 && quest_Manager.quest_Completed[quest_Number - 1]) 
			{
				if (quest_Manager.quest_Completed[quest_Number - 1] == true) 
				{
					Yes_No_Button.alpha = 1;

                    txt_Quest.text = "Quest : " + quest_Manager.quests[quest_Number].startText.ToString() + "\n Buff : " + quest_Manager.quests[quest_Number].playerBuff + "(" + Quest_Object.Realationship_Old_Woman.ToString() + ")" + "\n" + "Reward : " + quest_Manager.quests[quest_Number].questReward.ToString() + "G"  + "\n Do You Want To Accept The Quest ?";
				}
			}
			if (quest_Number == 4 && quest_Manager.quest_Completed[quest_Number - 1]) 
			{
				if (quest_Manager.quest_Completed[quest_Number - 1] == true) 
				{
					Yes_No_Button.alpha = 1;

					txt_Quest.text = "Quest : " + quest_Manager.quests[quest_Number].startText.ToString() + "\n Buff : " + quest_Manager.quests[quest_Number].playerBuff + "(" + Quest_Object.Realationship_Old_Woman.ToString() + ")" + "\n" + "Reward : " + quest_Manager.quests[quest_Number].questReward.ToString() + "G"  + "\n Do You Want To Accept The Quest ?";
				}
			}
			if (quest_Number == 5 && quest_Manager.quest_Completed[quest_Number - 1]) 
			{
				if (quest_Manager.quest_Completed[quest_Number - 1] == true) 
				{
					Yes_No_Button.alpha = 1;

					txt_Quest.text = "Quest : " + quest_Manager.quests[quest_Number].startText.ToString() + "\n Buff : " + quest_Manager.quests[quest_Number].playerBuff + "(" + Quest_Object.Realationship_Old_Woman.ToString() + ")" + "\n" + "Reward : " + quest_Manager.quests[quest_Number].questReward.ToString() + "G"  + "\n Do You Want To Accept The Quest ?";
				}
			}
		}
        else if (other.gameObject.name == "Player" && GameMaster.city)
        {
            if (quest_Number == 6 && quest_Manager.quest_Completed[quest_Number - 1])
            {
                if (quest_Manager.quest_Completed[quest_Number - 1] == true)
                {
                    Yes_No_Button.alpha = 1;

                    txt_Quest.text = "Quest : " + quest_Manager.quests[quest_Number].startText.ToString() + "\n Buff : " + quest_Manager.quests[quest_Number].playerBuff + "(" + Quest_Object.Realationship_Old_Woman.ToString() + ")" + "\n" + "Reward : " + quest_Manager.quests[quest_Number].questReward.ToString() + "G" + "\n Do You Want To Accept The Quest ?";
                }
            }
            if (quest_Number == 7 && quest_Manager.quest_Completed[quest_Number - 1])
            {
                if (quest_Manager.quest_Completed[quest_Number - 1] == true)
                {
                    Yes_No_Button.alpha = 1;

                    txt_Quest.text = "Quest : " + quest_Manager.quests[quest_Number].startText.ToString() + "\n Buff : " + quest_Manager.quests[quest_Number].playerBuff + "(" + Quest_Object.Realationship_Old_Woman.ToString() + ")" + "\n" + "Reward : " + quest_Manager.quests[quest_Number].questReward.ToString() + "G" + "\n Do You Want To Accept The Quest ?";
                }
            }
            if (quest_Number == 8 && quest_Manager.quest_Completed[quest_Number - 1])
            {
                if (quest_Manager.quest_Completed[quest_Number - 1] == true)
                {
                    Yes_No_Button.alpha = 1;

                    txt_Quest.text = "Quest : " + quest_Manager.quests[quest_Number].startText.ToString() + "\n Buff : " + quest_Manager.quests[quest_Number].playerBuff + "(" + Quest_Object.Realationship_Old_Woman.ToString() + ")" + "\n" + "Reward : " + quest_Manager.quests[quest_Number].questReward.ToString() + "G" + "\n Do You Want To Accept The Quest ?";
                }
            }
            if (quest_Number == 9 && quest_Manager.quest_Completed[quest_Number - 1])
            {
                if (quest_Manager.quest_Completed[quest_Number - 1] == true)
                {
                    Yes_No_Button.alpha = 1;

                    txt_Quest.text = "Quest : " + quest_Manager.quests[quest_Number].startText.ToString() + "\n Buff : " + quest_Manager.quests[quest_Number].playerBuff + "(" + Quest_Object.Realationship_Old_Woman.ToString() + ")" + "\n" + "Reward : " + quest_Manager.quests[quest_Number].questReward.ToString() + "G" + "\n Do You Want To Accept The Quest ?";
                }
            }
        }
    }

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.name == "Player" && GameMaster.field) 
		{
			quest_Manager.DestroyQuestText ();
			Yes_No_Button.alpha = 0;
		}
		if (other.gameObject.name == "Player" && GameMaster.city) 
		{
			quest_Manager.DestroyQuestText ();
			Yes_No_Button.alpha = 0;
		}
	}
}
