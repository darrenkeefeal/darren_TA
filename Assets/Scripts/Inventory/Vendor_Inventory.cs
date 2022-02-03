using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vendor_Inventory : Chest_Inventory {

	// Use this for initialization
	protected override void Start () 
	{
		EmptySlots = slots;
		base.Start ();
		GiveItem ("HP Potion");
		GiveItem ("Rare Sword");
		GiveItem ("Rare Shield");
	}

	protected void GiveItem(string itemName)
	{
		GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);
		tmp.AddComponent<Items_Script> ();
		Items_Script newItem = tmp.GetComponent<Items_Script> ();

		if (Inventory_Manager.Instance.ItemContainer.Consumables.Exists (x => x.ItemName == itemName)) 
		{
			newItem.Item = Inventory_Manager.Instance.ItemContainer.Consumables.Find (x => x.ItemName == itemName);
		}
		else if (Inventory_Manager.Instance.ItemContainer.Weapons.Exists (x => x.ItemName == itemName)) 
		{
			newItem.Item = Inventory_Manager.Instance.ItemContainer.Weapons.Find (x => x.ItemName == itemName);
		}
		else if (Inventory_Manager.Instance.ItemContainer.Equipments.Exists (x => x.ItemName == itemName)) 
		{
			newItem.Item = Inventory_Manager.Instance.ItemContainer.Equipments.Find (x => x.ItemName == itemName);
		}

		if (newItem != null) 
		{
			addItem(newItem);
		}

		Destroy (tmp);
	}

	public override void MoveItem(GameObject clicked)
	{
		
	}
}