using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class InventoryLink : MonoBehaviour {

	public Chest_Inventory chestInventory;
	BoxCollider2D[] myCollider;

	public int row, slots;

	private List<Stack<Items_Script>> allSlots;

	Scene current_Scene;
	string scene_Name;

	void Update()
	{
		if (GameMaster.house) 
		{
            chestInventory.UpdateLayout(allSlots, row, slots);

            this.GetComponent<SpriteRenderer> ().enabled = true;
			//this.GetComponent<BoxCollider2D> ().enabled = true;

			foreach (BoxCollider2D collider in myCollider) 
			{
				collider.enabled = true;
			}
		}
		else
		{
			this.GetComponent<SpriteRenderer> ().enabled = false;
			//this.GetComponent<BoxCollider2D> ().enabled = false;

			foreach (BoxCollider2D collider in myCollider) 
			{
				collider.enabled = false;
			}
		}
	}

	void Start()
	{
		if (chestInventory == null) 
		{
			chestInventory = GameObject.Find ("Chest_Inventory").GetComponent<Chest_Inventory> ();
		}

		myCollider = gameObject.GetComponents<BoxCollider2D> ();

		allSlots = new List<Stack<Items_Script>> ();

		current_Scene = SceneManager.GetActiveScene();
		scene_Name = current_Scene.name;
    }

    private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
			chestInventory.UpdateLayout (allSlots, row, slots);
			chestInventory.MoveItemToChest ();
        }
	}
}
