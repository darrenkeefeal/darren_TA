using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Container
{
	private List<Items> consumables = new List<Items>();
	public List<Items> Consumables
	{
		get { return consumables; }
		set { consumables = value; }
	}

	private List<Items> equipments = new List<Items>();
	public List<Items> Equipments
	{
		get { return equipments; }
		set { equipments = value; }
	}

	private List<Items> weapons = new List<Items>();
	public List<Items> Weapons
	{
		get { return weapons; }
		set { weapons = value; }
	}

	private List<Items> materials = new List<Items>();
	public List<Items> Materials
	{
		get { return materials; }
		set { materials = value; }
	}

	public void itemContainer()
	{
		
	}
}
