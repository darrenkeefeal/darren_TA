using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Item : MonoBehaviour {

	public int questNumber;

	private Quest_Manager quest_Manager;

	public string itemName;

	// Use this for initialization
	void Start () {
		quest_Manager = FindObjectOfType<Quest_Manager> ();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "Player") 
		{
			if (!quest_Manager.quest_Completed [questNumber] && quest_Manager.quests [questNumber].gameObject.activeSelf) 
			{
				quest_Manager.itemCollected = itemName;
				gameObject.SetActive (false);
			}
		}	
	}

}
