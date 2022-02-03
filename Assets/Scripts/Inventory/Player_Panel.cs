using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player_Panel : Inventory {

	int durability;

	public Slots[] equipmentSlots;

	private static Player_Panel instance;
	public static Player_Panel Instance
	{
		get 
		{
			if (instance == null) 
			{
				instance = GameObject.FindObjectOfType<Player_Panel> ();
			}
			return Player_Panel.instance; 
		}
	}

	void Awake()
	{
		equipmentSlots = transform.GetComponentsInChildren<Slots> ();
	}

	void Update()
	{
		foreach (Slots slot in equipmentSlots) 
		{
			if (!slot.isEmpty) 
			{
				if (slot.CurrentItem.Item.ItemType == ItemType.Shield || slot.CurrentItem.Item.ItemType == ItemType.Armor) 
				{
					Equipment_Master e = (Equipment_Master)slot.CurrentItem.Item;

					if (e.Equip_Durability <= 0) 
					{
						e.Def = 1;
					} 
					else
					{
						e.Def = e.Def_Temp;
					}
				}
				else if (slot.CurrentItem.Item.ItemType == ItemType.Sword) 
				{
					Weapon w = (Weapon)slot.CurrentItem.Item;

					if(w.Weapon_Durability <= 0)
					{
						w.Str = 1;
					}
					else 
					{
						w.Str = w.Str_Temp;
					}
				}
			}
		}

		if (Player.upgrade_weapon) 
		{
			foreach (Slots slot in equipmentSlots) 
			{
				if (!slot.isEmpty) 
				{
					if (slot.CurrentItem.Item.ItemType == ItemType.Shield || slot.CurrentItem.Item.ItemType == ItemType.Armor) 
					{
						Equipment_Master e = (Equipment_Master)slot.CurrentItem.Item;
						e.Durability = 100;
					}
					else if (slot.CurrentItem.Item.ItemType == ItemType.Sword) 
					{
						Weapon w = (Weapon)slot.CurrentItem.Item;
						w.Durability = 100;
					}
				}
			}
			Player.upgrade_weapon = false;
		}
	}

	public override void CreateLayout ()
	{
		
	}

	public void EquipItem(Slots slot, Items_Script item)
	{
		CalStats ();
		Slots.SwapItems (slot, Array.Find (equipmentSlots, x => x.canContain == item.Item.ItemType));
	}
    
    public void CalStats()
	{
		int str = 0;
		int def = 0;
        
        foreach (Slots slot in equipmentSlots) 
		{
			if (!slot.isEmpty)
            { 
                if (slot.CurrentItem.Item.ItemType == ItemType.Shield || slot.CurrentItem.Item.ItemType == ItemType.Armor) 
				{
					Equipment_Master e = (Equipment_Master)slot.CurrentItem.Item;
					def = def + e.Def;
					durability = UnityEngine.Random.Range (1, 5) / 2;

					e.Durability = e.Durability - durability;

					if (e.Durability <= 0) 
					{
						e.Durability = 0;
					}
				}
				else if (slot.CurrentItem.Item.ItemType == ItemType.Sword) 
				{
                    Weapon w = (Weapon)slot.CurrentItem.Item;
                    
                    str = str + w.Str;
                    durability = UnityEngine.Random.Range(1, 3) / 2;

                    w.Durability = w.Durability - durability;

                    if (w.Durability <= 0) 
					{
						w.Durability = 0;
					}
				}
			}
		}

		Player.Instance.SetStats (str, def);
	}
}
