using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob_Spawn : MonoBehaviour {

	public GameObject Objectnya_Blob;
	public Inventory inventory;

	Transform Spawn_Blob, Spawn_Blob_1, Spawn_Blob_2, Spawn_Blob_3, Spawn_Blob_4, Spawn_Blob_5, Spawn_Blob_6;
	private float Rand_Blob;
	private float spawnTime = 1f;
	private float repeatTime = 5f;	
	public static float maxBlob;

	public static bool Random_Drop = false;
	private Vector2 HP_Pos;

	private bool spawnBlob;

	/*
	private float timer;
	private float waktu;
	private bool isTiming;
	*/

	void Awake()
	{
		spawnBlob = GameMaster.enemySpawn;
		maxBlob = 0;
	}

	// Use this for initialization
	void Start () {

		Spawn_Blob = transform.Find ("Blob");
		Spawn_Blob_1 = transform.Find ("Blob_1");
		Spawn_Blob_2 = transform.Find ("Blob_2");
		Spawn_Blob_3 = transform.Find ("Blob_3");
		Spawn_Blob_4 = transform.Find ("Blob_4");
		Spawn_Blob_5 = transform.Find ("Blob_5");
		Spawn_Blob_6 = transform.Find ("Blob_6");

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
		if (!spawnBlob) {
			if (Random_Drop) {
				int Rand_Type = Random.Range (0, 100);
				int Rand_Drop;
				int Random_Item;
				GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);
                
				if (Rand_Type <= 5) 
				{
					tmp.AddComponent<Items_Script> ();
					Items_Script newConsumable = tmp.GetComponent<Items_Script> ();
					Random_Item = Random.Range (0, Inventory_Manager.Instance.ItemContainer.Consumables.Count);
					newConsumable.Item = Inventory_Manager.Instance.ItemContainer.Consumables [Random_Item];
					inventory.addItem (newConsumable);
					Destroy (tmp);
                }
				else if (Rand_Type > 6 && Rand_Type <= 10) 
				{
					tmp.AddComponent<Items_Script> ();
					Items_Script newMaterial = tmp.GetComponent<Items_Script> ();
					Random_Item = Random.Range (0, Inventory_Manager.Instance.ItemContainer.Materials.Count);
					newMaterial.Item = Inventory_Manager.Instance.ItemContainer.Materials [6];

					Rand_Drop = Random.Range (1, 3);

					for (int i = 0; i < Rand_Drop; i++) 
					{
						inventory.addItem (newMaterial);
					}
					Destroy (tmp);
                }
				else if (Rand_Type > 11 && Rand_Type <= 15) 
				{
					tmp.AddComponent<Items_Script> ();
					Items_Script newMaterial = tmp.GetComponent<Items_Script> ();
					Random_Item = Random.Range (0, Inventory_Manager.Instance.ItemContainer.Materials.Count);
					newMaterial.Item = Inventory_Manager.Instance.ItemContainer.Materials [7];

					Rand_Drop = Random.Range (1, 3);

					for (int i = 0; i < Rand_Drop; i++) 
					{
						inventory.addItem (newMaterial);
					}
					Destroy (tmp);
                }
				else if (Rand_Type > 16 && Rand_Type <= 18) 
				{
					tmp.AddComponent<Items_Script> ();
					Items_Script newMaterial = tmp.GetComponent<Items_Script> ();
					Random_Item = Random.Range (0, Inventory_Manager.Instance.ItemContainer.Materials.Count);
					newMaterial.Item = Inventory_Manager.Instance.ItemContainer.Materials [2];
					inventory.addItem (newMaterial);
					Destroy (tmp);
                }
                /*
                else
				{
					tmp.AddComponent<Items_Script> ();
					Items_Script newWeapon = tmp.GetComponent<Items_Script> ();

                    //Harus Di Clone Karena Dari XML Langsung Dijadikan Ke Object

					Random_Item = Random.Range (0, Inventory_Manager.Instance.ItemContainer.Weapons.Count);
					newWeapon.Item = (Items)Inventory_Manager.Instance.ItemContainer.Weapons [Random_Item].Clone();
                    
                    inventory.addItem (newWeapon);
					Destroy (tmp);

                    Save.save_Anything_Player = true;
                }
                */
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

			if (maxBlob > 4) 
			{
				CancelInvoke ("Spawnnya_Blob");
			} 
			else 
			{
				InvokeRepeating ("Spawnnya_Blob", spawnTime, repeatTime);
			}
		}
	}

	void Spawnnya_Blob()
	{
		maxBlob++;
		Rand_Blob = Random.Range (0, 7);
		if (Rand_Blob == 0) {
			Instantiate (Objectnya_Blob, Spawn_Blob.position, Quaternion.identity);
		} 
		else if (Rand_Blob == 1) {
			Instantiate (Objectnya_Blob, Spawn_Blob_1.position, Quaternion.identity);
		}
		else if (Rand_Blob == 2) {
			Instantiate (Objectnya_Blob, Spawn_Blob_2.position, Quaternion.identity);
		}
		else if (Rand_Blob == 3) {
			Instantiate (Objectnya_Blob, Spawn_Blob_3.position, Quaternion.identity);
		}
		else if (Rand_Blob == 4) {
			Instantiate (Objectnya_Blob, Spawn_Blob_4.position, Quaternion.identity);
		}
		else if (Rand_Blob == 5) {
			Instantiate (Objectnya_Blob, Spawn_Blob_5.position, Quaternion.identity);
		}
		else if (Rand_Blob == 6) {
			Instantiate (Objectnya_Blob, Spawn_Blob_6.position, Quaternion.identity);
		}
	}

	/*
	void beginTimer()
	{
		timer = 0;
		isTiming = true;
	}

	void endTimer()
	{
		isTiming = false;
	}
	*/
}
