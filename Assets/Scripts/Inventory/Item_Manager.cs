using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Xml.Serialization;

public enum Category{Equipment, Weapon, Consumable , Material};

public class Item_Manager : MonoBehaviour 
{
	public ItemType itemType;
	public Quality quality;
	public Category category;
	public string spriteNeutral;
	public string spriteHighlight;
	public string itemName;
	public string itemDesc;
	public int maxSize;
	public int str;
	public int def;
	public int health;
	public int weapon_durability;
	public int armor_durability;

    public void CreateItem()
	{
		Item_Container itemContainer = new Item_Container ();

		Type[] itemTypes = { typeof(Equipment_Master), typeof(Weapon), typeof(Consumable), typeof(Materials) };

		FileStream fs = new FileStream (Path.Combine (Application.streamingAssetsPath, "Items.xml"), FileMode.Open);

		XmlSerializer serializer = new XmlSerializer (typeof(Item_Container), itemTypes);

		itemContainer = (Item_Container)serializer.Deserialize (fs);

		serializer.Serialize (fs, itemContainer);

		fs.Close ();
        
        switch (category) {
			case Category.Equipment:
				itemContainer.Equipments.Add (new Equipment_Master (itemName, itemDesc, itemType, quality, spriteNeutral, spriteHighlight, maxSize, def , armor_durability));
				break;
			case Category.Weapon:
				itemContainer.Weapons.Add (new Weapon (itemName, itemDesc, itemType, quality, spriteNeutral, spriteHighlight, maxSize, str , weapon_durability));
                break;
			case Category.Consumable:
				itemContainer.Consumables.Add (new Consumable (itemName, itemDesc, itemType, quality, spriteNeutral, spriteHighlight, maxSize, health));
				break;
			case Category.Material:
                itemContainer.Materials.Add (new Materials (itemName, itemDesc, itemType, quality, spriteNeutral, spriteHighlight, maxSize));
				break;
		}

		fs = new FileStream(Path.Combine(Application.streamingAssetsPath,"Items.xml"),FileMode.Create);
		serializer.Serialize (fs, itemContainer);
		fs.Close ();
	}
}
