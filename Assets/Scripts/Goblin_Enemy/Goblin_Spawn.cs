using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_Spawn : MonoBehaviour {

	public GameObject Objectnya_Goblin;
	public Inventory inventory;

	Transform Spawn_Mini_Orc, Spawn_Mini_Orc_1, Spawn_Mini_Orc_2, Spawn_Mini_Orc_3, Spawn_Mini_Orc_4, Spawn_Mini_Orc_5, Spawn_Mini_Orc_6;
	Transform Spawn_Mini_Orc_7, Spawn_Mini_Orc_8, Spawn_Mini_Orc_9, Spawn_Mini_Orc_10;
	private float Rand_Goblin;
	private float spawnTime = 1f;
	private float repeatTime = 5f;	
	public static float maxGoblin;

	public static bool Random_Drop = false;
	private Vector2 HP_Pos;

	private bool spawnGoblin;

	/*
	private float timer;
	private float waktu;
	private bool isTiming;
	*/

	void Awake()
	{
		spawnGoblin = GameMaster.enemySpawn;
		maxGoblin = 0;
	}

	// Use this for initialization
	void Start () {

		Spawn_Mini_Orc = transform.Find ("Mini_Orc");
		Spawn_Mini_Orc_1 = transform.Find ("Mini_Orc_1");
		Spawn_Mini_Orc_2 = transform.Find ("Mini_Orc_2");
		Spawn_Mini_Orc_3 = transform.Find ("Mini_Orc_3");
		Spawn_Mini_Orc_4 = transform.Find ("Mini_Orc_4");
		Spawn_Mini_Orc_5 = transform.Find ("Mini_Orc_5");
		Spawn_Mini_Orc_6 = transform.Find ("Mini_Orc_6");
		Spawn_Mini_Orc_7 = transform.Find ("Mini_Orc_7");
		Spawn_Mini_Orc_8 = transform.Find ("Mini_Orc_8");
		Spawn_Mini_Orc_9 = transform.Find ("Mini_Orc_9");
		Spawn_Mini_Orc_10 = transform.Find ("Mini_Orc_10");

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
		if (!spawnGoblin) {
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
				else if (Rand_Type > 11 && Rand_Type <= 15) 
				{
					tmp.AddComponent<Items_Script> ();
					Items_Script newWeapon = tmp.GetComponent<Items_Script> ();
					Random_Item = Random.Range (0, 3);
					newWeapon.Item = Inventory_Manager.Instance.ItemContainer.Weapons [Random_Item];
					inventory.addItem (newWeapon);
					Destroy (tmp);
                }
				else if (Rand_Type > 16 && Rand_Type <= 20) 
				{
					tmp.AddComponent<Items_Script> ();
					Items_Script newEquipment = tmp.GetComponent<Items_Script> ();
					Random_Item = Random.Range (0, 3);
					newEquipment.Item = Inventory_Manager.Instance.ItemContainer.Equipments [Random_Item];
					inventory.addItem (newEquipment);
					Destroy (tmp);
                }
				else if (Rand_Type > 21 && Rand_Type <= 30) 
				{
					tmp.AddComponent<Items_Script> ();
					Items_Script newMaterial = tmp.GetComponent<Items_Script> ();
					Random_Item = Random.Range (0, Inventory_Manager.Instance.ItemContainer.Materials.Count);
					newMaterial.Item = Inventory_Manager.Instance.ItemContainer.Materials [1];

					Rand_Drop = Random.Range (1, 3);

					for (int i = 0; i < Rand_Drop; i++) 
					{
						inventory.addItem (newMaterial);
					}

					Destroy (tmp);
                }
				else if (Rand_Type > 30 && Rand_Type <= 40) 
				{
					tmp.AddComponent<Items_Script> ();
					Items_Script newMaterial = tmp.GetComponent<Items_Script> ();
					Random_Item = Random.Range (0, Inventory_Manager.Instance.ItemContainer.Materials.Count);
					newMaterial.Item = Inventory_Manager.Instance.ItemContainer.Materials [5];

					Rand_Drop = Random.Range (1, 3);

					for (int i = 0; i < Rand_Drop; i++) 
					{
						inventory.addItem (newMaterial);
					}

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

			if (maxGoblin > 7) 
			{
				CancelInvoke ("Spawnnya_Goblin");
			} 
			else 
			{
				InvokeRepeating ("Spawnnya_Goblin", spawnTime, repeatTime);
			}
		}
	}

	void Spawnnya_Goblin()
	{
		maxGoblin++;
		Rand_Goblin = Random.Range (0, 12);
		if (Rand_Goblin == 0) {
			Instantiate (Objectnya_Goblin, Spawn_Mini_Orc.position, Quaternion.identity);
		} 
		else if (Rand_Goblin == 1) {
			Instantiate (Objectnya_Goblin, Spawn_Mini_Orc_1.position, Quaternion.identity);
		}
		else if (Rand_Goblin == 2) {
			Instantiate (Objectnya_Goblin, Spawn_Mini_Orc_2.position, Quaternion.identity);
		}
		else if (Rand_Goblin == 3) {
			Instantiate (Objectnya_Goblin, Spawn_Mini_Orc_3.position, Quaternion.identity);
		}
		else if (Rand_Goblin == 4) {
			Instantiate (Objectnya_Goblin, Spawn_Mini_Orc_4.position, Quaternion.identity);
		}
		else if (Rand_Goblin == 5) {
			Instantiate (Objectnya_Goblin, Spawn_Mini_Orc_5.position, Quaternion.identity);
		}
		else if (Rand_Goblin == 6) {
			Instantiate (Objectnya_Goblin, Spawn_Mini_Orc_6.position, Quaternion.identity);
		}
		else if (Rand_Goblin == 7) {
			Instantiate (Objectnya_Goblin, Spawn_Mini_Orc_7.position, Quaternion.identity);
		}
		else if (Rand_Goblin == 8) {
			Instantiate (Objectnya_Goblin, Spawn_Mini_Orc_8.position, Quaternion.identity);
		}
		else if (Rand_Goblin == 9) {
			Instantiate (Objectnya_Goblin, Spawn_Mini_Orc_9.position, Quaternion.identity);
		}
		else if (Rand_Goblin == 10) {
			Instantiate (Objectnya_Goblin, Spawn_Mini_Orc_10.position, Quaternion.identity);
		}
	}
}
