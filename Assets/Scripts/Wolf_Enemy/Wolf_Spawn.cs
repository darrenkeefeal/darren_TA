using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf_Spawn : MonoBehaviour {

	public GameObject Objectnya_Wolf;
	public Inventory inventory;

	Transform Spawn_Wolf, Spawn_Wolf_1, Spawn_Wolf_2, Spawn_Wolf_3, Spawn_Wolf_4, Spawn_Wolf_5, Spawn_Wolf_6;
	Transform Spawn_Wolf_7, Spawn_Wolf_8, Spawn_Wolf_9, Spawn_Wolf_10, Spawn_Wolf_11, Spawn_Wolf_12, Spawn_Wolf_13;
	private float Rand_Wolf;
	private float spawnTime = 1f;
	private float repeatTime = 5f;	
	public static float maxWolf;

	public static bool Random_Drop = false;
	private Vector2 HP_Pos;

	private bool spawnWolf;

	/*
	private float timer;
	private float waktu;
	private bool isTiming;
	*/

	void Awake()
	{
		spawnWolf = GameMaster.enemySpawn;
		maxWolf = 0;
	}

	// Use this for initialization
	void Start () {

		Spawn_Wolf = transform.Find ("Wolf");
		Spawn_Wolf_1 = transform.Find ("Wolf_1");
		Spawn_Wolf_2 = transform.Find ("Wolf_2");
		Spawn_Wolf_3 = transform.Find ("Wolf_3");
		Spawn_Wolf_4 = transform.Find ("Wolf_4");
		Spawn_Wolf_5 = transform.Find ("Wolf_5");
		Spawn_Wolf_6 = transform.Find ("Wolf_6");
		Spawn_Wolf_7 = transform.Find ("Wolf_7");
		Spawn_Wolf_8 = transform.Find ("Wolf_8");
		Spawn_Wolf_9 = transform.Find ("Wolf_9");
		Spawn_Wolf_10 = transform.Find ("Wolf_10");
		Spawn_Wolf_11 = transform.Find ("Wolf_11");
		Spawn_Wolf_12 = transform.Find ("Wolf_12");
		Spawn_Wolf_13 = transform.Find ("Wolf_13");

		//isTiming = false;
		//beginTimer ();
	}

	// Update is called once per frame
	// ERROR MASUK 5X KARENA TRANSFORM ADA 5
	void Update () {
		/*
		if (isTiming) {
			timer += Time.deltaTime;
		}
		if (timer >= waktu) {
			waktu = 2;
			endTimer ();
			Spawnnya_Blob ();
			beginTimer ();
		}
		*/
		if (!spawnWolf) {
			if (Random_Drop) {
				int Rand_Type = Random.Range (0, 100);
				int Rand_Drop;
				int Random_Item;
				GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);

				if (Rand_Type <= 10) 
				{
					tmp.AddComponent<Items_Script> ();
					Items_Script newConsumable = tmp.GetComponent<Items_Script> ();
					Random_Item = Random.Range (0, Inventory_Manager.Instance.ItemContainer.Consumables.Count);
					newConsumable.Item = Inventory_Manager.Instance.ItemContainer.Consumables [Random_Item];
					inventory.addItem (newConsumable);
					Destroy (tmp);
                }
				else if (Rand_Type > 11 && Rand_Type <= 20) 
				{
					tmp.AddComponent<Items_Script> ();
					Items_Script newMaterial = tmp.GetComponent<Items_Script> ();
					Random_Item = Random.Range (0, Inventory_Manager.Instance.ItemContainer.Materials.Count);
					newMaterial.Item = Inventory_Manager.Instance.ItemContainer.Materials [8];

					Rand_Drop = Random.Range (1, 3);

					for (int i = 0; i < Rand_Drop; i++) 
					{
						inventory.addItem (newMaterial);
					}
					Destroy (tmp);
                }
				else if (Rand_Type > 21 && Rand_Type <= 30) 
				{
					tmp.AddComponent<Items_Script> ();
					Items_Script newMaterial = tmp.GetComponent<Items_Script> ();
					Random_Item = Random.Range (0, Inventory_Manager.Instance.ItemContainer.Materials.Count);
					newMaterial.Item = Inventory_Manager.Instance.ItemContainer.Materials [9];

					Rand_Drop = Random.Range (1, 3);

					for (int i = 0; i < Rand_Drop; i++) 
					{
						inventory.addItem (newMaterial);
					}
					Destroy (tmp);
                }
				else if (Rand_Type > 31 && Rand_Type <= 35) 
				{
					tmp.AddComponent<Items_Script> ();
					Items_Script newMaterial = tmp.GetComponent<Items_Script> ();
					Random_Item = Random.Range (0, Inventory_Manager.Instance.ItemContainer.Materials.Count);
					newMaterial.Item = Inventory_Manager.Instance.ItemContainer.Materials [2];
					inventory.addItem (newMaterial);
					Destroy (tmp);
                }
				else
				{
					Destroy (tmp);
				}

                /*
				switch (Rand_Type) {
				case 0:
					tmp.AddComponent<Items_Script> ();
					Items_Script newConsumable = tmp.GetComponent<Items_Script> ();
					Random_Item = Random.Range (0, Inventory_Manager.Instance.ItemContainer.Consumables.Count);
					newConsumable.Item = Inventory_Manager.Instance.ItemContainer.Consumables [Random_Item];
					inventory.addItem (newConsumable);
					Destroy (tmp);
					break;
				case 1:
					tmp.AddComponent<Items_Script> ();
					Items_Script newEquipment = tmp.GetComponent<Items_Script> ();
					Random_Item = Random.Range (0, Inventory_Manager.Instance.ItemContainer.Equipments.Count);
					newEquipment.Item = Inventory_Manager.Instance.ItemContainer.Equipments [Random_Item];
					inventory.addItem (newEquipment);
					Destroy (tmp);
					break;
				case 2:
					tmp.AddComponent<Items_Script> ();
					Items_Script newWeapon = tmp.GetComponent<Items_Script> ();
					Random_Item = Random.Range (0, Inventory_Manager.Instance.ItemContainer.Weapons.Count);
					newWeapon.Item = Inventory_Manager.Instance.ItemContainer.Weapons [Random_Item];
					inventory.addItem (newWeapon);
					Destroy (tmp);
					break;
				case 3:
					tmp.AddComponent<Items_Script> ();
					Items_Script newMaterial = tmp.GetComponent<Items_Script> ();
					Random_Item = Random.Range (0, Inventory_Manager.Instance.ItemContainer.Materials.Count);
					newMaterial.Item = Inventory_Manager.Instance.ItemContainer.Materials [Random_Item];
					for (int i = 0; i < 2; i++) 
					{
						inventory.addItem (newMaterial);
					}
					Destroy (tmp);
					break;
				}
				*/
                //Save.save_Anything_Player = true;

                Random_Drop = false;
            }

			if (maxWolf > 8) 
			{
				CancelInvoke ("Spawnnya_Wolf");
			} 
			else 
			{
				InvokeRepeating ("Spawnnya_Wolf", spawnTime, repeatTime);
			}
		}
	}

	void Spawnnya_Wolf()
	{
		maxWolf++;
		Rand_Wolf = Random.Range (0, 14);
		if (Rand_Wolf == 0) {
			Instantiate (Objectnya_Wolf, Spawn_Wolf.position, Quaternion.identity);
		} 
		else if (Rand_Wolf == 1) {
			Instantiate (Objectnya_Wolf, Spawn_Wolf_1.position, Quaternion.identity);
		}
		else if (Rand_Wolf == 2) {
			Instantiate (Objectnya_Wolf, Spawn_Wolf_2.position, Quaternion.identity);
		}
		else if (Rand_Wolf == 3) {
			Instantiate (Objectnya_Wolf, Spawn_Wolf_3.position, Quaternion.identity);
		}
		else if (Rand_Wolf == 4) {
			Instantiate (Objectnya_Wolf, Spawn_Wolf_4.position, Quaternion.identity);
		}
		else if (Rand_Wolf == 5) {
			Instantiate (Objectnya_Wolf, Spawn_Wolf_5.position, Quaternion.identity);
		}
		else if (Rand_Wolf == 6) {
			Instantiate (Objectnya_Wolf, Spawn_Wolf_6.position, Quaternion.identity);
		}
		else if (Rand_Wolf == 7) {
			Instantiate (Objectnya_Wolf, Spawn_Wolf_7.position, Quaternion.identity);
		}
		else if (Rand_Wolf == 8) {
			Instantiate (Objectnya_Wolf, Spawn_Wolf_8.position, Quaternion.identity);
		}
		else if (Rand_Wolf == 9) {
			Instantiate (Objectnya_Wolf, Spawn_Wolf_9.position, Quaternion.identity);
		}
		else if (Rand_Wolf == 10) {
			Instantiate (Objectnya_Wolf, Spawn_Wolf_10.position, Quaternion.identity);
		}
		else if (Rand_Wolf == 11) {
			Instantiate (Objectnya_Wolf, Spawn_Wolf_11.position, Quaternion.identity);
		}
		else if (Rand_Wolf == 12) {
			Instantiate (Objectnya_Wolf, Spawn_Wolf_12.position, Quaternion.identity);
		}
		else if (Rand_Wolf == 13) {
			Instantiate (Objectnya_Wolf, Spawn_Wolf_13.position, Quaternion.identity);
		}
	}
}
