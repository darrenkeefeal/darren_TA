using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checking_Ok : MonoBehaviour {

    private Quest_Manager quest_Manager;

    // Use this for initialization
    void Start ()
    {
        quest_Manager = FindObjectOfType<Quest_Manager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (quest_Manager.quests[9].isActiveAndEnabled || GameMaster.EndGame == "yes")
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if (quest_Manager.quests[9].isActiveAndEnabled || GameMaster.EndGame == "yes")
            {
                
            }
            else
            {
                other.GetComponent<Transform>().position = new Vector2(other.GetComponent<Transform>().position.x, other.GetComponent<Transform>().position.y - 2);
            }
        }
    }
}
