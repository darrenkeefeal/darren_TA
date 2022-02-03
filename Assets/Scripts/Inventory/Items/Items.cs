using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Items  : System.ICloneable
{
	public ItemType ItemType{ get; set;}
	public Quality Quality{ get; set;}

	public string SpriteNeutral{ get; set;}
	public string SpriteHighlight{ get; set;}

	public int MaxSize{ get; set;}
	public string ItemName{ get; set;}
	public string ItemDesc{ get; set;}

	public int Durability{ get; set;}

    public string Id_Item { get; set; }
    
	public Items()
	{
		
	}

	public Items (string itemName, string itemDesc, ItemType itemType, Quality quality, string spriteNeutral, string spriteHighlight, int maxSize , int durability)
	{
        this.Id_Item = ItemType.ToString() + "_" + itemName;
        this.ItemName = itemName;
		this.ItemType = itemType;
		this.ItemDesc = itemDesc;
		this.Quality = quality;
		this.SpriteNeutral = spriteNeutral;
		this.SpriteHighlight = spriteHighlight;
		this.MaxSize = maxSize;
		this.Durability = durability;
	}

	public abstract void Use (Slots slot, Items_Script item);
    
    public virtual void genId()
    {
        this.Id_Item = this.ItemType + "_" + this.ItemName;
    }

	public virtual string GetToolTip()
	{
		string color = string.Empty;
		string newLine = string.Empty;

		if (ItemDesc != string.Empty) 
		{
			newLine = "\n";
		}

        switch (Quality)
        {
            case Quality.Normal:
                color = "black";
                break;
            case Quality.Rare:
                color = "blue";
                break;
            case Quality.Epic:
                color = "red";
                break;
            case Quality.Legendary:
                color = "yellow";
                break;
        }
		return string.Format ("<color=" + color + "><size=16>{0}</size></color><size=14><i><color=black>" + newLine + "{1}</color></i></size>",ItemName,ItemDesc);
	}

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}
