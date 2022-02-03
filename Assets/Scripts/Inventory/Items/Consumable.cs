using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Items
{
	public int Health { get; set; }

	public Consumable ()
	{
		
	}

	public Consumable(string itemName, string itemDesc, ItemType itemType, Quality quality, string spriteNeutral, string spriteHighlight, int maxSize , int health) 
		: base (itemName,itemDesc,itemType,quality,spriteNeutral,spriteHighlight,maxSize,1)
	{
		this.Health = health;
	}

	public override void Use (Slots slot, Items_Script item)
	{
		slot.Items.Pop ();
		Player.HP_Plus += 20;
	}

	public override string GetToolTip ()
	{
		string stats = string.Empty;

		if (Health > 0) 
		{
			stats += "\n Restores +" + Health.ToString() + " HP";
		}

		string itemTip = base.GetToolTip ();

		return string.Format ("{0}" + "<size=14> {1}</size>",itemTip,Id_Item);
	}
}
