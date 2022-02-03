using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crafting : Inventory {

	private static Crafting instance;
	public static Crafting Instance
	{
		get
		{ 
			if (instance == null) 
			{
				instance = FindObjectOfType<Crafting> ();
			}
			return instance;
		}
	}

	private Dictionary<string,Items> craftingItems = new Dictionary<string,Items> ();

	public GameObject btnPrefab;

	private GameObject previewSlot;

	public override void CreateLayout ()
	{
		base.CreateLayout ();

		GameObject btnCraft;

		inventoryRect.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, inventoryHeight + slotSize + slotPaddingTop * 3);
		btnCraft = Instantiate (btnPrefab);
		RectTransform btnRect = btnCraft.GetComponent<RectTransform> ();
		btnCraft.name = "CraftButton";
		btnCraft.transform.SetParent (this.transform.parent);
		btnRect.localPosition = inventoryRect.localPosition + new Vector3 (slotSize * 1.7f, -slotPaddingTop - (slotSize * 2.2f));
		btnRect.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, (slotSize + slotPaddingLeft) * Inventory_Manager.Instance.canvas.scaleFactor);
		btnRect.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, slotSize * Inventory_Manager.Instance.canvas.scaleFactor);
		btnCraft.transform.SetParent (transform);

		btnCraft.GetComponent<Button> ().onClick.AddListener (CraftItem);

		previewSlot = Instantiate (Inventory_Manager.Instance.slotPrefab);
		RectTransform slotRect = previewSlot.GetComponent<RectTransform> ();
		previewSlot.name = "Preview";
		previewSlot.transform.SetParent (this.transform.parent);
		slotRect.localPosition = inventoryRect.localPosition + new Vector3(slotPaddingLeft, -slotPaddingTop - (slotSize * 2.7f));
		slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,slotSize * Inventory_Manager.Instance.canvas.scaleFactor);
		slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,slotSize * Inventory_Manager.Instance.canvas.scaleFactor);
		previewSlot.transform.SetParent (this.transform);

		previewSlot.GetComponent<Slots> ().IsClickable = false;
	}

	public override void MoveItem(GameObject clicked)
	{
		base.MoveItem (clicked);
		UpdatePreview ();
	}

	public void CreateCrafting()
	{
		//Weapons
		craftingItems.Add ("Wolf Fang-Wolf Fang-Wolf Fang-Wolf Fang-Wolf Fang-Wolf Fang-", Inventory_Manager.Instance.ItemContainer.Weapons.Find (x => x.ItemName == "Decent Sword"));
		craftingItems.Add ("Bone-Bone-Bone-Bone-Bone-Bone-", Inventory_Manager.Instance.ItemContainer.Weapons.Find (x => x.ItemName == "Bone Sword"));
		craftingItems.Add ("Wood-Wood-Wood-Wood-Wood-Wood-", Inventory_Manager.Instance.ItemContainer.Weapons.Find (x => x.ItemName == "Wooden Sword"));
		craftingItems.Add ("Rare Sword-Rare Sword-Rare Sword-Golden Horn-Golden Horn-Gold Bar-", Inventory_Manager.Instance.ItemContainer.Weapons.Find (x => x.ItemName == "Machette"));
		craftingItems.Add ("Bone Sword-Iron Bar-Iron Bar-Iron Bar-Iron Bar-Iron Bar-", Inventory_Manager.Instance.ItemContainer.Weapons.Find (x => x.ItemName == "Rare Sword"));

		//Shields
		craftingItems.Add ("Wood-Wood-Wood-Slime Gem-Slime Gem-Slime Gem-", Inventory_Manager.Instance.ItemContainer.Equipments.Find (x => x.ItemName == "Wooden Shield"));
		craftingItems.Add ("Wolf Skin-Wolf Skin-Wolf Skin-Wolf Skin-Wolf Skin-Wolf Skin-", Inventory_Manager.Instance.ItemContainer.Equipments.Find (x => x.ItemName == "Normal Shield"));
		craftingItems.Add ("Iron Bar-Iron Bar-Iron Bar-Wolf Skin-Wolf Skin-Wolf Skin-", Inventory_Manager.Instance.ItemContainer.Equipments.Find (x => x.ItemName == "Steel Shield"));
		craftingItems.Add ("Steel Shield-Steel Shield-Steel Shield-Golden Horn-Golden Horn-Gold Bar-", Inventory_Manager.Instance.ItemContainer.Equipments.Find (x => x.ItemName == "Knight Shield"));
		craftingItems.Add ("Normal Shield-Iron Bar-Iron Bar-Wolf Skin-Wolf Skin-Wolf Skin-", Inventory_Manager.Instance.ItemContainer.Equipments.Find (x => x.ItemName == "Rare Shield"));

		//Armors
		craftingItems.Add ("Wood-Wood-Wood-Slime Liquid-Slime Gem-Slime Gem-", Inventory_Manager.Instance.ItemContainer.Equipments.Find (x => x.ItemName == "Wooden Armor"));
		craftingItems.Add ("Wood-Wood-Wood-Iron Pot-Iron Pot-Wooden Armor-", Inventory_Manager.Instance.ItemContainer.Equipments.Find (x => x.ItemName == "Plate Wood Armor"));
		craftingItems.Add ("Iron Bar-Iron Bar-Iron Bar-Iron Pot-Iron Pot-Iron Pot-", Inventory_Manager.Instance.ItemContainer.Equipments.Find (x => x.ItemName == "Steel Armor"));
		craftingItems.Add ("Steel Armor-Steel Armor-Steel Armor-Golden Horn-Golden Horn-Gold Bar-", Inventory_Manager.Instance.ItemContainer.Equipments.Find (x => x.ItemName == "Knight Armor"));
		craftingItems.Add ("Plate Wood Armor-Iron Bar-Iron Bar-Iron Pot-Iron Pot-Iron Pot-", Inventory_Manager.Instance.ItemContainer.Equipments.Find (x => x.ItemName == "Rare Armor"));

		//Consumables
		craftingItems.Add ("Slime Liquid-Slime Liquid-Slime Liquid-Slime Liquid-Slime Liquid-Slime Liquid-", Inventory_Manager.Instance.ItemContainer.Consumables.Find (x => x.ItemName == "HP Potion"));
    }

	public void CraftItem()
	{
		string output = string.Empty;

		foreach(GameObject slot in allSlots)
		{
			Slots tmpSlot = slot.GetComponent<Slots> ();

			if (tmpSlot.isEmpty) 
			{
				output += "Empty-";
			} 
			else 
			{
				output += tmpSlot.CurrentItem.Item.ItemName + "-";
			}
		}

		if (craftingItems.ContainsKey (output)) 
		{
			GameObject tmpObj = Instantiate (Inventory_Manager.Instance.itemObject);
			tmpObj.AddComponent<Items_Script> ();
			Items_Script craftedItem = tmpObj.AddComponent<Items_Script> ();

			Items tmpItem;

			craftingItems.TryGetValue (output, out tmpItem);
			if (tmpItem != null) 
			{
				craftedItem.Item = tmpItem;

				if (Player.Instance.inventory.addItem (craftedItem))
				{
					foreach (GameObject slot in allSlots) 
					{
						slot.GetComponent<Slots> ().RemoveItem ();
					}
				}
				Destroy (tmpObj);
			}
		}

		UpdatePreview ();
	}

	public void UpdatePreview()
	{
		string output = string.Empty;

		previewSlot.GetComponent<Slots> ().ClearSlot ();

		foreach(GameObject slot in allSlots)
		{
			Slots tmpSlot = slot.GetComponent<Slots> ();

			if (tmpSlot.isEmpty) 
			{
				output += "Empty-";
			} 
			else 
			{
				output += tmpSlot.CurrentItem.Item.ItemName + "-";
			}
		}

		if (craftingItems.ContainsKey (output)) 
		{
			GameObject tmpObj = Instantiate (Inventory_Manager.Instance.itemObject);
			tmpObj.AddComponent<Items_Script> ();
			Items_Script craftedItem = tmpObj.AddComponent<Items_Script> ();

			Items tmpItem;

			craftingItems.TryGetValue (output, out tmpItem);
			if (tmpItem != null) 
			{
				craftedItem.Item = tmpItem;

				previewSlot.GetComponent<Slots> ().AddItem (craftedItem);

				Destroy (tmpObj);
			}
		}
	}

	public override void open ()
	{
		base.open();

		foreach (GameObject slot in allSlots) 
		{
			Slots tmpSlot = slot.GetComponent<Slots> ();
			int count = tmpSlot.Items.Count;

			for (int i = 0; i < count; i++) 
			{
				Items_Script tmpItem = tmpSlot.RemoveItem ();

				if(!Player.Instance.inventory.addItem (tmpItem))
				{
					float anglex = Random.Range (-1, 2);
					float angley = Random.Range (-1, 2);

					Vector3 v = new Vector3 (anglex, angley, 0);

					GameObject tmpDrop = GameObject.Instantiate (Inventory_Manager.Instance.drop_Item, Player_Ref.transform.position + v, Quaternion.identity);
					tmpDrop.AddComponent<Items_Script> ();
					tmpDrop.GetComponent<Items_Script> ().Item = tmpItem.Item;
				}
			}
		}
	}
}
