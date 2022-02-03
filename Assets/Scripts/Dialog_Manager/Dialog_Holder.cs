using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_Holder : MonoBehaviour {

    public Sprite talk_Action;
    public Sprite sword_Action;

    private Quest_Manager quest_Manager;

    private Dialog_Manager dialog_Manager;

	public string dialog;

	float x , y;

	// Use this for initialization
	void Start () {
        quest_Manager = FindObjectOfType<Quest_Manager>();

        dialog_Manager = FindObjectOfType<Dialog_Manager> ();

		x = this.transform.localPosition.x + 5;
		y = this.transform.localPosition.y + 5;
	}
	
	// Update is called once per frame
	void Update () {
        if (GameMaster.boss)
        {
            Destroy(this);
        }
	}

    private void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.name == "Player") 
		{
            if (this.tag == "Portal_Boss")
            {
                if (quest_Manager.quests[9].isActiveAndEnabled || GameMaster.EndGame == "yes")
                {

                }
                else
                {
                    dialog_Manager.ShowDialogFull(dialog, x, y);
                }
            }
            else
            {
                GameObject.Find("Img_Actions").GetComponent<Image>().sprite = talk_Action;

                dialog_Manager.ShowDialogFull(dialog, x, y);
                GameMaster.NPC = true;
            }
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.name == "Player") 
		{
            if (this.tag == "Portal_Boss")
            {
                dialog_Manager.DestroyDialog();
            }
            else
            {
                GameObject.Find("Img_Actions").GetComponent<Image>().sprite = sword_Action;

                dialog_Manager.DestroyDialog();
                GameMaster.NPC = true;
            }
		}
	}
}
