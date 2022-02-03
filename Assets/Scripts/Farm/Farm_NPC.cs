using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Farm_NPC : MonoBehaviour {

	public Slots[] equipmentSlots;

    [SerializeField]
    Text txt_Tomato_Seed;
    public static int tomato_Seed;

    [SerializeField]
    Text txt_Corn_Seed;
    public static int corn_Seed;

    public static bool Check_Seed_Txt;

    bool plant_Once = true;

	// Use this for initialization
	void Start () 
	{
		equipmentSlots = GameObject.Find("Inventory").GetComponentsInChildren<Slots> ();

        tomato_Seed = 0;
        corn_Seed = 0;
	}

    private void Update()
    {
        if(Check_Seed_Txt)
        {
            if (GameObject.Find("Canvas_Farm").GetComponent<CanvasGroup>().alpha == 1)
            {
                foreach (Slots slot in equipmentSlots)
                {
                    if (!slot.isEmpty)
                    {
                        if (slot.CurrentItem.Item.ItemName == "Tomato Seed")
                        {
                            tomato_Seed = tomato_Seed + slot.isCount;
                        }
                        else if (slot.CurrentItem.Item.ItemName == "Corn Seed")
                        {
                            corn_Seed = corn_Seed + slot.isCount;
                        }
                    }
                }

                txt_Tomato_Seed.text = "Tomato Seed " + tomato_Seed + "X";
                txt_Corn_Seed.text = "Corn Seed " + corn_Seed + "X";

                Check_Seed_Txt = false;
            }
        }
    }

    public void Tomato()
	{
        tomato_Seed = 0;
        corn_Seed = 0;
        plant_Once = true;

		foreach (Slots slot in equipmentSlots) 
		{
			if (!slot.isEmpty) 
			{
				if (slot.CurrentItem.Item.ItemName == "Tomato Seed" && plant_Once) 
				{
					if (!Farming.planted) 
					{
						slot.RemoveItem ();
						Farming.plant_Type = "Tomato";
						Farming.planted = true;
					}
					else if (!Farming_2.planted) 
					{
						slot.RemoveItem ();
						Farming_2.plant_Type = "Tomato";
						Farming_2.planted = true;
					}
					else if (!Farming_3.planted) 
					{
						slot.RemoveItem ();
						Farming_3.plant_Type = "Tomato";
						Farming_3.planted = true;
					}
					else if (!Farming_4.planted) 
					{
						slot.RemoveItem ();
						Farming_4.plant_Type = "Tomato";
						Farming_4.planted = true;
					}
					else if (!Farming_5.planted) 
					{
						slot.RemoveItem ();
						Farming_5.plant_Type = "Tomato";
						Farming_5.planted = true;
					}
					else if (!Farming_6.planted) 
					{
						slot.RemoveItem ();
						Farming_6.plant_Type = "Tomato";
						Farming_6.planted = true;
					}
					else if (!Farming_7.planted) 
					{
						slot.RemoveItem ();
						Farming_7.plant_Type = "Tomato";
						Farming_7.planted = true;
					}
					else if (!Farming_8.planted) 
					{
						slot.RemoveItem ();
						Farming_8.plant_Type = "Tomato";
						Farming_8.planted = true;
					}
					else if (!Farming_9.planted) 
					{
						slot.RemoveItem ();
						Farming_9.plant_Type = "Tomato";
						Farming_9.planted = true;
					}

                    plant_Once = false;
				}
			}
		}

        Check_Seed_Txt = true;
        //Save.save_Anything_Farm = true;
    }

	public void Corn()
	{
        tomato_Seed = 0;
        corn_Seed = 0;
        plant_Once = true;

		foreach (Slots slot in equipmentSlots) 
		{
			if (!slot.isEmpty) 
			{
				if (slot.CurrentItem.Item.ItemName == "Corn Seed" && plant_Once) 
				{
					if (!Farming.planted) 
					{
						slot.RemoveItem ();
						Farming.plant_Type = "Corn";
						Farming.planted = true;
					}
					else if (!Farming_2.planted) 
					{
						slot.RemoveItem ();
						Farming_2.plant_Type = "Corn";
						Farming_2.planted = true;
					}
					else if (!Farming_3.planted) 
					{
						slot.RemoveItem ();
						Farming_3.plant_Type = "Corn";
						Farming_3.planted = true;
					}
					else if (!Farming_4.planted) 
					{
						slot.RemoveItem ();
						Farming_4.plant_Type = "Corn";
						Farming_4.planted = true;
					}
					else if (!Farming_5.planted) 
					{
						slot.RemoveItem ();
						Farming_5.plant_Type = "Corn";
						Farming_5.planted = true;
					}
					else if (!Farming_6.planted) 
					{
						slot.RemoveItem ();
						Farming_6.plant_Type = "Corn";
						Farming_6.planted = true;
					}
					else if (!Farming_7.planted) 
					{
						slot.RemoveItem ();
						Farming_7.plant_Type = "Corn";
						Farming_7.planted = true;
					}
					else if (!Farming_8.planted) 
					{
						slot.RemoveItem ();
						Farming_8.plant_Type = "Corn";
						Farming_8.planted = true;
					}
					else if (!Farming_9.planted) 
					{
						slot.RemoveItem ();
						Farming_9.plant_Type = "Corn";
						Farming_9.planted = true;
					}

                    plant_Once = false;
                }
			}
		}

        Check_Seed_Txt = true;
        //Save.save_Anything_Farm = true;
    }
}
