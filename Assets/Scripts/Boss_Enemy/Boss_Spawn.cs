using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Spawn : MonoBehaviour {

	public GameObject Objectnya_Boss;
    public GameObject Objectnya_Minion;
    public GameObject Objectnya_Skill;
    private Inventory inventory;

	Transform Boss_Pos;
    Transform Minion_Pos, Minion_Pos_1;

    Transform skill_pos1, skill_pos2, skill_pos3;

	private bool okBoss_Spawn;

	public static bool Random_Drop = false;

	private Vector2 HP_Pos;
    
    int ctr_Minion = 0;
    bool spawn_Minion = true;
    float time_Minion = 10;

    bool specialOk = true;
    float specialCD = 7;

	/*
	private float timer;
	private float waktu;
	private bool isTiming;
	*/

	void Awake()
	{
		
	}

	// Use this for initialization
	void Start () {

		if (inventory == null)
		{
			inventory = FindObjectOfType<Inventory> ();
		}

		Boss_Pos = transform.Find ("Boss_Pos");

        Minion_Pos = transform.Find("Minion_Pos");
        Minion_Pos_1 = transform.Find("Minion_Pos_1");

        skill_pos1 = transform.Find("Skill_Pos_1");
        skill_pos2 = transform.Find("Skill_Pos_2");
        skill_pos3 = transform.Find("Skill_Pos_3");

        okBoss_Spawn = true;

		//isTiming = false;
		//beginTimer ();
	}

    public IEnumerator Special_Boss()
    {
        yield return new WaitForSeconds(1f);
        specialCD--;
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
		if (okBoss_Spawn) 
		{
			Instantiate (Objectnya_Boss, Boss_Pos.position, Quaternion.identity);
			okBoss_Spawn = false;
		}

        if (Boss.HP_Temp <= 125 && Boss.HP_Temp >= 1)
        {
            if (specialOk)
            {
                int random_SS = Random.Range(0, 3);
                if (random_SS == 1)
                {
                    Instantiate(Objectnya_Skill, skill_pos1.position, Quaternion.identity);
                }
                else if (random_SS == 2)
                {
                    Instantiate(Objectnya_Skill, skill_pos2.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(Objectnya_Skill, skill_pos3.position, Quaternion.identity);
                }
                specialOk = false;
            }
        }

        if (Boss.HP_Temp <= 125)
        {
            specialCD -= Time.deltaTime;
            if (specialCD < 0)
            {
                specialCD = 7;
                specialOk = true;
            }
        }

        if (Boss.HP_Temp <= 300 && ctr_Minion <= 4 && spawn_Minion && Boss.HP_Temp >= 1)
        {
            Instantiate(Objectnya_Minion, Minion_Pos.position, Quaternion.identity);
            Instantiate(Objectnya_Minion, Minion_Pos_1.position, Quaternion.identity);
            ctr_Minion = ctr_Minion + 2;
            spawn_Minion = false;
        }

        if (Boss.HP_Temp <= 300)
        {
            time_Minion -= Time.deltaTime;
            if (time_Minion < 0)
            {
                time_Minion = 10;
                spawn_Minion = true;
            }
        }
        if (Random_Drop) 
		{
			int Rand_Type = Random.Range (0, 100);
			int Rand_Drop;
			int Random_Item;
			GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);

			if (Rand_Type <= 15) 
			{
				tmp.AddComponent<Items_Script> ();
				Items_Script newConsumable = tmp.GetComponent<Items_Script> ();
				Random_Item = Random.Range (0, Inventory_Manager.Instance.ItemContainer.Consumables.Count);
				newConsumable.Item = Inventory_Manager.Instance.ItemContainer.Consumables [Random_Item];

				Rand_Drop = Random.Range (1, 5);

				for (int i = 0; i < Rand_Drop; i++) 
				{
					inventory.addItem (newConsumable);
				}
				Destroy (tmp);
            }
			else if (Rand_Type > 15 && Rand_Type <= 25) 
			{
				tmp.AddComponent<Items_Script> ();
				Items_Script newMaterial = tmp.GetComponent<Items_Script> ();
				Random_Item = Random.Range (0, Inventory_Manager.Instance.ItemContainer.Materials.Count);
				newMaterial.Item = Inventory_Manager.Instance.ItemContainer.Materials [2];

				Rand_Drop = Random.Range (1, 5);

				for (int i = 0; i < Rand_Drop; i++) 
				{
					inventory.addItem (newMaterial);
				}
				Destroy (tmp);
            }
			else if (Rand_Type > 25 && Rand_Type <= 30) 
			{
				tmp.AddComponent<Items_Script> ();
				Items_Script newMaterial = tmp.GetComponent<Items_Script> ();
				Random_Item = Random.Range (0, Inventory_Manager.Instance.ItemContainer.Materials.Count);
				newMaterial.Item = Inventory_Manager.Instance.ItemContainer.Materials [3];
				inventory.addItem (newMaterial);
				Destroy (tmp);
            }
			else if (Rand_Type > 30 && Rand_Type <= 50) 
			{
				tmp.AddComponent<Items_Script> ();
				Items_Script newMaterial = tmp.GetComponent<Items_Script> ();
				Random_Item = Random.Range (0, Inventory_Manager.Instance.ItemContainer.Materials.Count);
				newMaterial.Item = Inventory_Manager.Instance.ItemContainer.Materials [4];
				inventory.addItem (newMaterial);
				Destroy (tmp);
            }
			else if (Rand_Type > 50 && Rand_Type <= 55) 
			{
				tmp.AddComponent<Items_Script> ();
				Items_Script newWeapon = tmp.GetComponent<Items_Script> ();
				Random_Item = Random.Range (0, Inventory_Manager.Instance.ItemContainer.Weapons.Count);
				newWeapon.Item = Inventory_Manager.Instance.ItemContainer.Weapons [Random_Item];
				inventory.addItem (newWeapon);
				Destroy (tmp);
            }
			else if (Rand_Type > 55 && Rand_Type <= 60) 
			{
				tmp.AddComponent<Items_Script> ();
				Items_Script newEquipment = tmp.GetComponent<Items_Script> ();
				Random_Item = Random.Range (0, Inventory_Manager.Instance.ItemContainer.Equipments.Count);
				newEquipment.Item = Inventory_Manager.Instance.ItemContainer.Equipments [Random_Item];
				inventory.addItem (newEquipment);
				Destroy (tmp);
            }
			else
			{
				Destroy (tmp);
			}

            //Save.save_Anything_Player = true;

            Random_Drop = false;
        }
	}
}
