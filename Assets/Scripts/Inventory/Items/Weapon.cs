using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : Items
{
	public int Str { get; set; }
	public int Weapon_Durability { get; set; }

	public int Str_Temp;
	public int Str_Broken = 1;

	public bool upgrade_Bool;

    public Weapon()
	{

	}

	public Weapon(string itemName, string itemDesc, ItemType itemType, Quality quality, string spriteNeutral, string spriteHighlight, int maxSize , int str , int weapon_Durability) 
		: base (itemName, itemDesc,itemType,quality,spriteNeutral,spriteHighlight,maxSize,weapon_Durability)
	{
        this.Id_Item = ItemType.ToString() + "_" + itemName;
        this.Str = str;
		this.Durability = Durability;
	}

	public override void Use (Slots slot, Items_Script item)
	{
        this.Durability--;
		Player_Panel.Instance.EquipItem (slot, item);
		if (this.Durability > 1) 
		{
			Str_Temp = this.Str;
			this.Str = Str_Temp;

			upgrade_Bool = false;
		}
		else 
		{
			if(!upgrade_Bool)
			{
				Str_Temp = this.Str;
				this.Str = Str_Broken;
				Durability = 0;
				upgrade_Bool = true;
			}
		}
	}

	public override string GetToolTip ()
	{
		string stats = string.Empty;

		if (Str > 0) 
		{
			stats += "\n +" + Str.ToString() + " Damage";
		}

		string itemTip = base.GetToolTip ();

		return string.Format ("{0}" + "<size=14> {1}</size>\n Durability : {2}",itemTip,stats,Durability);
	}
}
