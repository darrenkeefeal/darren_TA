using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest_Inventory : Inventory {

	private List<Stack<Items_Script>> chestItems;
	private int chestSlots;

	public override void CreateLayout()
	{
		allSlots = new List<GameObject> ();

		for (int i = 0; i < slots; i++) 
		{
			GameObject newSlot = Instantiate (Inventory_Manager.Instance.slotPrefab);
			newSlot.name = "Slot";
			newSlot.transform.SetParent (this.transform);
			allSlots.Add (newSlot);

			newSlot.GetComponent<Button> ().onClick.AddListener
			(
				delegate {MoveItem (newSlot);}
			);

			newSlot.SetActive (false);
		}
	}

    public void UpdateLayout(List<Stack<Items_Script>> items, int rows , int slots)
	{
		this.chestItems = items;
		this.chestSlots = slots;

		//Rumus Agar Mencetak UI Inventory Background
		inventoryWidth = (slots / rows) * (slotSize + slotPaddingLeft) + slotPaddingLeft;
		inventoryHeight = rows * (slotSize + slotPaddingTop) + slotPaddingTop;

		inventoryRect = GetComponent<RectTransform> ();

		//Untuk Mengubah Width dan Height Background
		inventoryRect.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, inventoryWidth + 10);
		inventoryRect.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, inventoryHeight + 10);

		int kolom = slots / rows;

		int index = 0;

		for (int i = 0; i < rows; i++) 
		{
			for (int j = 0; j < kolom; j++) 
			{
				//Game Object dipanggil untuk nantinya dapat diakses
				GameObject newSlot = allSlots[index];

				//Mengambil seluruh RectTransform childnya
				RectTransform rectSlot = newSlot.GetComponent<RectTransform> ();

				//Melakukan Set Parent ke Canvas
				newSlot.transform.SetParent (this.transform);

				//Rumus Untuk Mencetak Slot di dalamnya
				rectSlot.localPosition = new Vector3(slotPaddingLeft * (j + 1) + (slotSize * j), -slotPaddingTop * (i + 1) - (slotSize * i));
				rectSlot.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, slotSize * Inventory_Manager.Instance.canvas.scaleFactor);
				rectSlot.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, slotSize * Inventory_Manager.Instance.canvas.scaleFactor);

				newSlot.SetActive (true);

				index++;
			}
		}
	}

	public override void open()
	{
		base.open ();

		if (IsOpen) 
		{
			MoveItemFromChest ();
		}
	}

	public void MoveItemToChest()
	{
		chestItems.Clear ();
		for (int i = 0; i < chestItems.Count ; i++) 
		{
			Slots tmpSlot = allSlots [i].GetComponent<Slots> ();

			if (!tmpSlot.isEmpty) 
			{
				chestItems.Add (new Stack<Items_Script> (tmpSlot.Items));

				if (!IsOpen) 
				{
					tmpSlot.ClearSlot ();
				}
			} 
			else 
			{
				chestItems.Add (new Stack<Items_Script> ());
			}
			if (!IsOpen) 
			{
				allSlots [i].SetActive (false);
			}
		}
	}

	public void MoveItemFromChest()
	{
		for (int i = 0; i < chestItems.Count; i++) 
		{
			if (chestItems.Count != 0 && chestItems.Count >= i && chestItems[i] != null && chestItems[i].Count > 0)
			{
				GameObject newSlot = allSlots[i];
				newSlot.GetComponent<Slots> ().AddItems (chestItems [i]);
			}
		}

		for (int i = 0; i < chestSlots; i++) 
		{
			allSlots [i].SetActive (true);
		}
	}

	protected override IEnumerator FadeOut()
	{
		yield return StartCoroutine (base.FadeOut ());
		MoveItemToChest ();
	}
}
