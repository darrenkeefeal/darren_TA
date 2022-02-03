using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Manager : MonoBehaviour {

	public Quest_Object[] quests;
	public bool[] quest_Completed;
	public bool[] quest_Done;

	public Dialog_Manager dialog_Manager;

	public string itemCollected;

	public string enemyKilled;

	float x , y;

	// Use this for initialization
	void Start () {
		quest_Completed = new bool[quests.Length];
		quest_Done = new bool[quests.Length];

		if (GameMaster.NPC) 
		{
			x = this.transform.localPosition.x + 5;
			y = this.transform.localPosition.y + 5;
		}
		else
		{
			//x = Screen.width / 2;
			//y = Screen.height / 2;
		}
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

	public void ShowQuestText(string txt_Quest)
	{
		dialog_Manager.dialogLine = new string[1];
		dialog_Manager.dialogLine [0] = txt_Quest;

		dialog_Manager.currentLine = 0;

		dialog_Manager.ShowDialogFull (txt_Quest, x, y);

		StartCoroutine (DestroyText());
	}

	IEnumerator DestroyText()
	{
		yield return new WaitForSeconds (3);
		DestroyQuestText ();
	}

	public void DestroyQuestText()
	{
		dialog_Manager.DestroyDialog ();
	}
}
