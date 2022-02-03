using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farming_4 : MonoBehaviour {

	public static bool changeDay_Farm4 = false;
	private bool dirt;

	GameObject tmp_inventory;
	public Inventory inventory;

    public static int dayCounter;

    public Sprite sprite_Dirt;
	public Sprite sprite_Null;
	private SpriteRenderer spriteRenderer;

	public Sprite sprite_Day1_Tomato;
	public Sprite sprite_Day2_Tomato;
	public Sprite sprite_Day3_Tomato;
	public Sprite sprite_Day4_Tomato;
	public Sprite sprite_Day5_Tomato;
	public Sprite sprite_Day6_Tomato;

	public Sprite sprite_Day1_Corn;
	public Sprite sprite_Day2_Corn;
	public Sprite sprite_Day3_Corn;
	public Sprite sprite_Day4_Corn;
	public Sprite sprite_Day5_Corn;
	public Sprite sprite_Day6_Corn;

	public static bool planted;
	public static string plant_Type;

	// Use this for initialization
	void Start () 
	{
        if (Player.ctr_Day_Farm4 >= 1)
        {
            dayCounter = Player.ctr_Day_Farm4;
        }
        else
        {
            dayCounter = 0;
        }

        if (inventory == null) 
		{
			inventory = FindObjectOfType<Inventory>();
		}

		spriteRenderer = GetComponent<SpriteRenderer> ();
		dirt = false;
	}

	// Update is called once per frame
	void Update () 
	{
		if (GameMaster.city) 
		{
			this.GetComponent<SpriteRenderer> ().enabled = true;
			this.GetComponent<BoxCollider2D> ().enabled = true;
		}
		else
		{
			this.GetComponent<SpriteRenderer> ().enabled = false;
			this.GetComponent<BoxCollider2D> ().enabled = false;
		}

		if (changeDay_Farm4 && plant_Type == "Tomato") 
		{
			if (dayCounter == -1) 
			{
				dayCounter = 0;
				changeDay_Farm4 = false;
			}
			else if (dayCounter == 0) 
			{
				spriteRenderer.sprite = sprite_Dirt;
				dayCounter = 1;
				changeDay_Farm4 = false;
			}
			else if(dayCounter == 1)
			{
				spriteRenderer.sprite = sprite_Day1_Tomato;
				dayCounter = 2;
				changeDay_Farm4 = false;
			}
			else if(dayCounter == 2)
			{
				spriteRenderer.sprite = sprite_Day2_Tomato;
				dayCounter = 3;
				changeDay_Farm4 = false;
			}
			else if(dayCounter == 3)
			{
				spriteRenderer.sprite = sprite_Day3_Tomato;
				dayCounter = 4;
				changeDay_Farm4 = false;
			}
			else if(dayCounter == 4)
			{
				spriteRenderer.sprite = sprite_Day4_Tomato;
				dayCounter = 5;
				changeDay_Farm4 = false;
			}
			else if(dayCounter == 5)
			{
				spriteRenderer.sprite = sprite_Day5_Tomato;
				dayCounter = 6;
				changeDay_Farm4 = false;
			}
			else if(dayCounter == 6)
			{
				spriteRenderer.sprite = sprite_Day6_Tomato;
				dayCounter = 7;
				changeDay_Farm4 = false;
			}
			else if(dayCounter == 7)
			{
				GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);
				tmp.AddComponent<Items_Script> ();
				Items_Script newMaterial = tmp.GetComponent<Items_Script> ();
				newMaterial.Item = Inventory_Manager.Instance.ItemContainer.Materials [13];
				inventory.addItem (newMaterial);
				Destroy (tmp);

				spriteRenderer.sprite = sprite_Null;

				dayCounter = 0;
				changeDay_Farm4 = false;
				plant_Type = "";
				planted = false;
			}
		}
		else if (changeDay_Farm4 && plant_Type == "Corn") 
		{
			if (dayCounter == -1) 
			{
				dayCounter = 0;
				changeDay_Farm4 = false;
			}
			else if (dayCounter == 0) 
			{
				spriteRenderer.sprite = sprite_Dirt;
				dayCounter = 1;
				changeDay_Farm4 = false;
			}
			else if(dayCounter == 1)
			{
				spriteRenderer.sprite = sprite_Day1_Corn;
				dayCounter = 2;
				changeDay_Farm4 = false;
			}
			else if(dayCounter == 2)
			{
				spriteRenderer.sprite = sprite_Day2_Corn;
				dayCounter = 3;
				changeDay_Farm4 = false;
			}
			else if(dayCounter == 3)
			{
				spriteRenderer.sprite = sprite_Day3_Corn;
				dayCounter = 4;
				changeDay_Farm4 = false;
			}
			else if(dayCounter == 4)
			{
				spriteRenderer.sprite = sprite_Day4_Corn;
				dayCounter = 5;
				changeDay_Farm4 = false;
			}
			else if(dayCounter == 5)
			{
				spriteRenderer.sprite = sprite_Day5_Corn;
				dayCounter = 6;
				changeDay_Farm4 = false;
			}
			else if(dayCounter == 6)
			{
				spriteRenderer.sprite = sprite_Day6_Corn;
				dayCounter = 7;
				changeDay_Farm4 = false;
			}
			else if(dayCounter == 7)
			{
				GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);
				tmp.AddComponent<Items_Script> ();
				Items_Script newMaterial = tmp.GetComponent<Items_Script> ();
				newMaterial.Item = Inventory_Manager.Instance.ItemContainer.Materials [14];
				inventory.addItem (newMaterial);
				Destroy (tmp);

				spriteRenderer.sprite = sprite_Null;

				dayCounter = 0;
				changeDay_Farm4 = false;
				plant_Type = "";
				planted = false;
			}
		}

        if (plant_Type == "Tomato")
        {
            if (dayCounter == 0)
            {
                spriteRenderer.sprite = sprite_Dirt;
            }
            else if (dayCounter == 1)
            {
                spriteRenderer.sprite = sprite_Day1_Tomato;
            }
            else if (dayCounter == 2)
            {
                spriteRenderer.sprite = sprite_Day2_Tomato;
            }
            else if (dayCounter == 3)
            {
                spriteRenderer.sprite = sprite_Day3_Tomato;
            }
            else if (dayCounter == 4)
            {
                spriteRenderer.sprite = sprite_Day4_Tomato;
            }
            else if (dayCounter == 5)
            {
                spriteRenderer.sprite = sprite_Day5_Tomato;
            }
            else if (dayCounter == 6)
            {
                spriteRenderer.sprite = sprite_Day6_Tomato;
            }
        }

        else if (plant_Type == "Corn")
        {
            if (dayCounter == 0)
            {
                spriteRenderer.sprite = sprite_Dirt;
            }
            else if (dayCounter == 1)
            {
                spriteRenderer.sprite = sprite_Day1_Corn;
            }
            else if (dayCounter == 2)
            {
                spriteRenderer.sprite = sprite_Day2_Corn;
            }
            else if (dayCounter == 3)
            {
                spriteRenderer.sprite = sprite_Day3_Corn;
            }
            else if (dayCounter == 4)
            {
                spriteRenderer.sprite = sprite_Day4_Corn;
            }
            else if (dayCounter == 5)
            {
                spriteRenderer.sprite = sprite_Day5_Corn;
            }
            else if (dayCounter == 6)
            {
                spriteRenderer.sprite = sprite_Day6_Corn;
            }
        }
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Player_Attack_Pos")) 
		{
			dirt = true;
			spriteRenderer.sprite = sprite_Dirt;
			dayCounter = 0;
		}
	}
}
