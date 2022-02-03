using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Type Kita Sendiri
public enum ItemType{Consumables,Sword,Shield,Armor,Free,Material};
public enum Quality{Normal,Rare,Epic,Legendary};

public class Items_Script : MonoBehaviour {

	private Items item;
	public Items Item
	{
		get { return item; }
		set 
		{ 
			item = value;

			spriteNeutral = Resources.Load<Sprite> (value.SpriteNeutral);
			spriteHighlight = Resources.Load<Sprite> (value.SpriteHighlight);
		}
	}

    public string uid_item;

    //public int new_durability;
    /*
    public void newDurability()
    {
        this.new_durability = 100;
    }
    
    public int getNewDurability()
    {
        return this.new_durability;
    }
    */

    public void genId()
    {
        this.uid_item = System.Guid.NewGuid().ToString();
    }

    public string getUidItem()
    {
        return this.uid_item;
    }

	public Sprite itemSprite;

	public Sprite spriteNeutral;
	public Sprite spriteHighlight;

	public void use(Slots slot)
	{
		item.Use (slot,this);
	}

	public string GetToolTip()
	{
		return item.GetToolTip ();
	}
}
