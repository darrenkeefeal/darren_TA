using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment_Master : Items
{
	public int Def { get; set; }
	public int Equip_Durability { get; set; }

	public int Def_Temp;
	public int Def_Broken = 1;

	public bool upgrade_Bool;

	public  Equipment_Master()
	{
		
	}

	public Equipment_Master(string itemName, string itemDesc, ItemType itemType, Quality quality, string spriteNeutral, string spriteHighlight, int maxSize , int def , int equip_Durability) 
		: base (itemName,itemDesc,itemType,quality,spriteNeutral,spriteHighlight,maxSize,equip_Durability)
	{
		this.Def = def;
		this.Durability = Durability;
	}

	public override void Use (Slots slot, Items_Script item)
	{
        this.Durability--;
        Player_Panel.Instance.EquipItem (slot, item);
		if (this.Durability >= 1) 
		{
			Def_Temp = this.Def;
			this.Def = Def_Temp;

			upgrade_Bool = false;
		}
		else
		{
			if(!upgrade_Bool)
			{
				Def_Temp = this.Def;
				this.Def = Def_Broken;
                Durability = 0;
				upgrade_Bool = true;
			}
		}

	}

	public override string GetToolTip ()
	{
		string stats = string.Empty;

		if (Def > 0) 
		{
			stats += "\n +" + Def.ToString() + " Def";
		}

		string itemTip = base.GetToolTip ();

		return string.Format ("{0}" + "<size=14> {1}</size>\n Durability : {2}",itemTip,stats,this.Durability);
	}
}
