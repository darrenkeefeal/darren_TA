using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crafting_Canvas : MonoBehaviour {

	string Canvas_List;

    private Quest_Manager quest_Manager;

    // Use this for initialization
    void Start () 
	{
		Canvas_List = "";

        quest_Manager = FindObjectOfType<Quest_Manager>();
    }

    // Update is called once per frame
    void Update () 
	{
        if (Quest_Object.Temp_Information_Quest == "None")
        {
            GameObject.Find("Txt_Quests").GetComponent<Text>().text = "Quest : \n " + "None ";
        }
        else
        {
            if (GameMaster.Questing_Global >= 0)
            {
                GameObject.Find("Txt_Quests").GetComponent<Text>().text = "Quest : \n " + Quest_Object.Temp_Information_Quest;
            }
        }

        if (Canvas_List == "Consumables")
		{
			GameObject.Find ("List_Consumables").GetComponent<CanvasGroup> ().alpha = 1;
			GameObject.Find ("List_Weapons").GetComponent<CanvasGroup> ().alpha = 0;
			GameObject.Find ("List_Shields").GetComponent<CanvasGroup> ().alpha = 0;
			GameObject.Find ("List_Armors").GetComponent<CanvasGroup> ().alpha = 0;
            GameObject.Find("List_Quests").GetComponent<CanvasGroup>().alpha = 0;
            Canvas_List = "";
		}
		else if(Canvas_List == "Weapons")
		{
			GameObject.Find ("List_Consumables").GetComponent<CanvasGroup> ().alpha = 0;
			GameObject.Find ("List_Weapons").GetComponent<CanvasGroup> ().alpha = 1;
			GameObject.Find ("List_Shields").GetComponent<CanvasGroup> ().alpha = 0;
			GameObject.Find ("List_Armors").GetComponent<CanvasGroup> ().alpha = 0;
            GameObject.Find("List_Quests").GetComponent<CanvasGroup>().alpha = 0;
            Canvas_List = "";
		}
		else if(Canvas_List == "Shields")
		{
			GameObject.Find ("List_Consumables").GetComponent<CanvasGroup> ().alpha = 0;
			GameObject.Find ("List_Weapons").GetComponent<CanvasGroup> ().alpha = 0;
			GameObject.Find ("List_Shields").GetComponent<CanvasGroup> ().alpha = 1;
			GameObject.Find ("List_Armors").GetComponent<CanvasGroup> ().alpha = 0;
            GameObject.Find("List_Quests").GetComponent<CanvasGroup>().alpha = 0;
            Canvas_List = "";
		}
		else if(Canvas_List == "Armors")
		{
			GameObject.Find ("List_Consumables").GetComponent<CanvasGroup> ().alpha = 0;
			GameObject.Find ("List_Weapons").GetComponent<CanvasGroup> ().alpha = 0;
			GameObject.Find ("List_Shields").GetComponent<CanvasGroup> ().alpha = 0;
			GameObject.Find ("List_Armors").GetComponent<CanvasGroup> ().alpha = 1;
            GameObject.Find("List_Quests").GetComponent<CanvasGroup>().alpha = 0;
            Canvas_List = "";
		}
        else if (Canvas_List == "Quests")
        {
            GameObject.Find("List_Consumables").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("List_Weapons").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("List_Shields").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("List_Armors").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("List_Quests").GetComponent<CanvasGroup>().alpha = 1;
            
            Canvas_List = "";
        }
    }

    public void Quests()
    {
        Canvas_List = "Quests";
    }

    public void Consumables()
	{
		Canvas_List = "Consumables";
	}

	public void Weapons()
	{
		Canvas_List = "Weapons";
	}

	public void Shields()
	{
		Canvas_List = "Shields";
	}

	public void Armors()
	{
		Canvas_List = "Armors";
	}
}
