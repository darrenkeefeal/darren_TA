using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Yes_No_Sell : MonoBehaviour {

    string url_Insert = "http://keefem.000webhostapp.com/Insert_Json.php";
    //string url_Insert = "http://fldstts.000webhostapp.com/Insert_Json.php";

    GameObject tmp_inventory;
	public CanvasGroup Yes_No_Button;

	public static bool yes_no_Sell;
	public static string sell_Item_Name;

	GameObject InputField_Object;
	InputField inputField;

	public static int price;
	public static string sell_Item_Type;

	// Use this for initialization
	void Start () 
	{
		InputField_Object = GameObject.Find("InputField");
		inputField = InputField_Object.GetComponent<InputField> ();

		tmp_inventory = GameObject.Find ("Inventory");
	}

	// Update is called once per frame
	void Update () 
	{
		if (yes_no_Sell) 
		{
			Yes_No_Button.alpha = 1;
		}
		else
		{
			Yes_No_Button.alpha = 0;
        }
	}

	public void GetPrice()
	{
        if (GameObject.Find("Cavnas_InputField").GetComponent<CanvasGroup>().alpha == 1)
        {
            price = int.Parse(inputField.text);

            WWWForm form = new WWWForm();
            //form.AddField("api", "darren_Insert");
            //form.AddField("username", GameMaster.player_Name);

            //Ganti JADI GAMEMASETER

            form.AddField("username", GameMaster.player_Name);
            form.AddField("item_Name", sell_Item_Name);
            form.AddField("item_Type", sell_Item_Type);
            form.AddField("item_Price", price);

            WWW url = new WWW(url_Insert, form);

            inputField.text = "Input Price...";
            sell_Item_Name = "";
            sell_Item_Type = "";

            GameObject.Find("Cavnas_InputField").GetComponent<CanvasGroup>().alpha = 0;
        }

        //Save.save_Anything_Player = true;
	}

	public void Yes_Sell()
	{
        if (!GameMaster.Online)
        {
            //Consumables
            if (sell_Item_Name == "HP Potion")
            {
                for (int i = 0; i < Inventory.ctrSell; i++)
                {
                    GameMaster.Player_Gold = GameMaster.Player_Gold + 25;
                }
                Button_Shop.updateGold = true;
            }

            //Weapons
            else if (sell_Item_Name == "Rare Sword")
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold + 500;
                Button_Shop.updateGold = true;
            }
            else if (sell_Item_Name == "Decent Sword")
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold + 1000;
                Button_Shop.updateGold = true;
            }
            else if (sell_Item_Name == "Bone Sword")
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold + 700;
                Button_Shop.updateGold = true;
            }
            else if (sell_Item_Name == "Wooden Sword")
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold + 150;
                Button_Shop.updateGold = true;
            }
            else if (sell_Item_Name == "Machette")
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold + 2500;
                Button_Shop.updateGold = true;
            }

            //Shields
            else if (sell_Item_Name == "Rare Shield")
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold + 700;
                Button_Shop.updateGold = true;
            }
            else if (sell_Item_Name == "Wooden Shield")
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold + 150;
                Button_Shop.updateGold = true;
            }
            else if (sell_Item_Name == "Normal Shield")
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold + 500;
                Button_Shop.updateGold = true;
            }
            else if (sell_Item_Name == "Steel Shield")
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold + 1000;
                Button_Shop.updateGold = true;
            }
            else if (sell_Item_Name == "Knight Shield")
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold + 2500;
                Button_Shop.updateGold = true;
            }

            //Armors
            else if (sell_Item_Name == "Rare Armor")
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold + 700;
                Button_Shop.updateGold = true;
            }
            else if (sell_Item_Name == "Wooden Armor")
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold + 150;
                Button_Shop.updateGold = true;
            }
            else if (sell_Item_Name == "Plate Wood Armor")
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold + 500;
                Button_Shop.updateGold = true;
            }
            else if (sell_Item_Name == "Steel Armor")
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold + 1000;
                Button_Shop.updateGold = true;
            }
            else if (sell_Item_Name == "Knight Armor")
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold + 2500;
                Button_Shop.updateGold = true;
            }

            //Materials
            else if (sell_Item_Name == "Bone")
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold + (20 * Inventory.ctrSell);
                Button_Shop.updateGold = true;
            }
            else if (sell_Item_Name == "Gold Bar")
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold + (5000 * Inventory.ctrSell);
                Button_Shop.updateGold = true;
            }
            else if (sell_Item_Name == "Golden Horn")
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold + (5000 * Inventory.ctrSell);
                Button_Shop.updateGold = true;
            }
            else if (sell_Item_Name == "Iron Bar")
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold + (50 * Inventory.ctrSell);
                Button_Shop.updateGold = true;
            }
            else if (sell_Item_Name == "Iron Pot")
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold + (40 * Inventory.ctrSell);
                Button_Shop.updateGold = true;
            }
            else if (sell_Item_Name == "Slime Gem")
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold + (10 * Inventory.ctrSell);
                Button_Shop.updateGold = true;
            }
            else if (sell_Item_Name == "Slime Liquid")
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold + (15 * Inventory.ctrSell);
                Button_Shop.updateGold = true;
            }
            else if (sell_Item_Name == "Wolf Fang")
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold + (30 * Inventory.ctrSell);
                Button_Shop.updateGold = true;
            }
            else if (sell_Item_Name == "Wolf Skin")
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold + (40 * Inventory.ctrSell);
                Button_Shop.updateGold = true;
            }
            else if (sell_Item_Name == "Wood")
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold + (10 * Inventory.ctrSell);
                Button_Shop.updateGold = true;
            }
            else if (sell_Item_Name == "Corn")
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold + (60 * Inventory.ctrSell);
                Button_Shop.updateGold = true;
            }
            else if (sell_Item_Name == "Tomato")
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold + (40 * Inventory.ctrSell);
                Button_Shop.updateGold = true;
            }
        }

        if (GameMaster.Online && Inventory.ctrSell >= 2)
        {
            Debug.Log("A");
            No_Sell();
            Inventory.ctrSell = 0;
        }
        else if (GameMaster.Online && GameMaster.Online_Max >= 6)
        {
            Debug.Log("B");
            No_Sell();
        }
        else if (GameMaster.Online && Inventory.ctrSell <= 1)
        {
            Yes_No_Button.alpha = 0;

            GameObject.Find("Cavnas_InputField").GetComponent<CanvasGroup>().alpha = 1;

            yes_no_Sell = false;
            Inventory.ctrSell = 0;
        }
        /*
		if (GameMaster.Online) 
		{
			Yes_No_Button.alpha = 0;
			GameObject.Find ("InputField").GetComponent<CanvasGroup> ().alpha = 0;
			yes_no_Sell = false;
			No_Sell ();
		}
        */
        else
        {
            Yes_No_Button.alpha = 0;
            Inventory.ctrSell = 0;
            sell_Item_Name = "";
            yes_no_Sell = false;

            if (GameMaster.Online)
            {
                GameObject.Find("Canvas_InputField").GetComponent<CanvasGroup>().alpha = 0;
            }
        }

        //Save.save_Anything_Player = true;
    }

	public void No_Sell()
	{
		//Consumables
		if (sell_Item_Name == "HP Potion") 
		{
			for (int i = 0; i < Inventory.ctrSell; i++)
			{
				GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);
				tmp.AddComponent<Items_Script> ();
				Items_Script newConsumable = tmp.GetComponent<Items_Script> ();
				newConsumable.Item = Inventory_Manager.Instance.ItemContainer.Consumables [0];
				tmp_inventory.GetComponent<Inventory> ().addItem (newConsumable);
				Destroy (tmp);
			}
		}

		//Swords
		else if (sell_Item_Name == "Rare Sword") 
		{
			GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);
			tmp.AddComponent<Items_Script> ();
			Items_Script newWeapon = tmp.GetComponent<Items_Script> ();
			newWeapon.Item = (Items)Inventory_Manager.Instance.ItemContainer.Weapons [0].Clone();
			tmp_inventory.GetComponent<Inventory> ().addItem (newWeapon);
			Destroy (tmp);
		}
		else if (sell_Item_Name == "Decent Sword") 
		{
			GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);
			tmp.AddComponent<Items_Script> ();
			Items_Script newWeapon = tmp.GetComponent<Items_Script> ();
			newWeapon.Item = (Items)Inventory_Manager.Instance.ItemContainer.Weapons[1].Clone();
			tmp_inventory.GetComponent<Inventory> ().addItem (newWeapon);
			Destroy (tmp);
		}
		else if (sell_Item_Name == "Bone Sword") 
		{
			GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);
			tmp.AddComponent<Items_Script> ();
			Items_Script newWeapon = tmp.GetComponent<Items_Script> ();
			newWeapon.Item = (Items)Inventory_Manager.Instance.ItemContainer.Weapons[2].Clone();
			tmp_inventory.GetComponent<Inventory> ().addItem (newWeapon);
			Destroy (tmp);
		}
		else if (sell_Item_Name == "Wooden Sword") 
		{
			GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);
			tmp.AddComponent<Items_Script> ();
			Items_Script newWeapon = tmp.GetComponent<Items_Script> ();
			newWeapon.Item = (Items)Inventory_Manager.Instance.ItemContainer.Weapons[3].Clone();
			tmp_inventory.GetComponent<Inventory> ().addItem (newWeapon);
			Destroy (tmp);
		}
		else if (sell_Item_Name == "Machette") 
		{
			GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);
			tmp.AddComponent<Items_Script> ();
			Items_Script newWeapon = tmp.GetComponent<Items_Script> ();
			newWeapon.Item = (Items)Inventory_Manager.Instance.ItemContainer.Weapons[4].Clone();
			tmp_inventory.GetComponent<Inventory> ().addItem (newWeapon);
			Destroy (tmp);
		}

		//Shields
		else if (sell_Item_Name == "Knight Shield") 
		{
			GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);
			tmp.AddComponent<Items_Script> ();
			Items_Script newShield = tmp.GetComponent<Items_Script> ();
			newShield.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments [1].Clone();
			tmp_inventory.GetComponent<Inventory> ().addItem (newShield);
			Destroy (tmp);
		}
		else if (sell_Item_Name == "Wooden Shield") 
		{
			GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);
			tmp.AddComponent<Items_Script> ();
			Items_Script newShield = tmp.GetComponent<Items_Script> ();
			newShield.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[6].Clone();
            tmp_inventory.GetComponent<Inventory> ().addItem (newShield);
			Destroy (tmp);
		}
		else if (sell_Item_Name == "Normal Shield") 
		{
			GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);
			tmp.AddComponent<Items_Script> ();
			Items_Script newShield = tmp.GetComponent<Items_Script> ();
			newShield.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[7].Clone();
            tmp_inventory.GetComponent<Inventory> ().addItem (newShield);
			Destroy (tmp);
		}
		else if (sell_Item_Name == "Steel Shield") 
		{
			GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);
			tmp.AddComponent<Items_Script> ();
			Items_Script newShield = tmp.GetComponent<Items_Script> ();
			newShield.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[8].Clone();
            tmp_inventory.GetComponent<Inventory> ().addItem (newShield);
			Destroy (tmp);
		}
		else if (sell_Item_Name == "Rare Shield") 
		{
			GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);
			tmp.AddComponent<Items_Script> ();
			Items_Script newShield = tmp.GetComponent<Items_Script> ();
			newShield.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[9].Clone();
            tmp_inventory.GetComponent<Inventory> ().addItem (newShield);
			Destroy (tmp);
		}

		//Armors
		else if (sell_Item_Name == "Knight Armor") 
		{
			GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);
			tmp.AddComponent<Items_Script> ();
			Items_Script newArmor = tmp.GetComponent<Items_Script> ();
			newArmor.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments [0].Clone();
			tmp_inventory.GetComponent<Inventory> ().addItem (newArmor);
			Destroy (tmp);
		}
		else if (sell_Item_Name == "Wooden Armor") 
		{
			GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);
			tmp.AddComponent<Items_Script> ();
			Items_Script newArmor = tmp.GetComponent<Items_Script> ();
			newArmor.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[2].Clone();
            tmp_inventory.GetComponent<Inventory> ().addItem (newArmor);
			Destroy (tmp);
		}
		else if (sell_Item_Name == "Plate Wood Armor") 
		{
			GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);
			tmp.AddComponent<Items_Script> ();
			Items_Script newArmor = tmp.GetComponent<Items_Script> ();
			newArmor.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[3].Clone();
            tmp_inventory.GetComponent<Inventory> ().addItem (newArmor);
			Destroy (tmp);
		}
		else if (sell_Item_Name == "Steel Armor") 
		{
			GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);
			tmp.AddComponent<Items_Script> ();
			Items_Script newArmor = tmp.GetComponent<Items_Script> ();
			newArmor.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[4].Clone();
            tmp_inventory.GetComponent<Inventory> ().addItem (newArmor);
			Destroy (tmp);
		}
		else if (sell_Item_Name == "Rare Armor") 
		{
			GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);
			tmp.AddComponent<Items_Script> ();
			Items_Script newArmor = tmp.GetComponent<Items_Script> ();
			newArmor.Item = Inventory_Manager.Instance.ItemContainer.Equipments [5];
			tmp_inventory.GetComponent<Inventory> ().addItem (newArmor);
			Destroy (tmp);
		}

		//Materials
		else if (sell_Item_Name == "Bone") 
		{
			for (int i = 0; i < Inventory.ctrSell; i++)
			{
				GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);
				tmp.AddComponent<Items_Script> ();
				Items_Script newMaterials = tmp.GetComponent<Items_Script> ();
				newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials [1];
				tmp_inventory.GetComponent<Inventory> ().addItem (newMaterials);
				Destroy (tmp);
			}
		}
		else if (sell_Item_Name == "Gold Bar") 
		{
			for (int i = 0; i < Inventory.ctrSell; i++)
			{
				GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);
				tmp.AddComponent<Items_Script> ();
				Items_Script newMaterials = tmp.GetComponent<Items_Script> ();
				newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials [2];
				tmp_inventory.GetComponent<Inventory> ().addItem (newMaterials);
				Destroy (tmp);
			}
		}
		else if (sell_Item_Name == "Golden Horn") 
		{
			for (int i = 0; i < Inventory.ctrSell; i++)
			{
				GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);
				tmp.AddComponent<Items_Script> ();
				Items_Script newMaterials = tmp.GetComponent<Items_Script> ();
				newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials [3];
				tmp_inventory.GetComponent<Inventory> ().addItem (newMaterials);
				Destroy (tmp);
			}
		}
		else if (sell_Item_Name == "Iron Bar") 
		{
			for (int i = 0; i < Inventory.ctrSell; i++)
			{
				GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);
				tmp.AddComponent<Items_Script> ();
				Items_Script newMaterials = tmp.GetComponent<Items_Script> ();
				newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials [4];
				tmp_inventory.GetComponent<Inventory> ().addItem (newMaterials);
				Destroy (tmp);
			}
		}
		else if (sell_Item_Name == "Iron Pot") 
		{
			for (int i = 0; i < Inventory.ctrSell; i++)
			{
				GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);
				tmp.AddComponent<Items_Script> ();
				Items_Script newMaterials = tmp.GetComponent<Items_Script> ();
				newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials [5];
				tmp_inventory.GetComponent<Inventory> ().addItem (newMaterials);
				Destroy (tmp);
			}
		}
		else if (sell_Item_Name == "Slime Gem") 
		{
			for (int i = 0; i < Inventory.ctrSell; i++)
			{
				GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);
				tmp.AddComponent<Items_Script> ();
				Items_Script newMaterials = tmp.GetComponent<Items_Script> ();
				newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials [6];
				tmp_inventory.GetComponent<Inventory> ().addItem (newMaterials);
				Destroy (tmp);
			}
		}
		else if (sell_Item_Name == "Slime Liquid") 
		{
			for (int i = 0; i < Inventory.ctrSell; i++)
			{
				GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);
				tmp.AddComponent<Items_Script> ();
				Items_Script newMaterials = tmp.GetComponent<Items_Script> ();
				newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials [7];
				tmp_inventory.GetComponent<Inventory> ().addItem (newMaterials);
				Destroy (tmp);
			}
		}
		else if (sell_Item_Name == "Wolf Fang") 
		{
			for (int i = 0; i < Inventory.ctrSell; i++)
			{
				GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);
				tmp.AddComponent<Items_Script> ();
				Items_Script newMaterials = tmp.GetComponent<Items_Script> ();
				newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials [8];
				tmp_inventory.GetComponent<Inventory> ().addItem (newMaterials);
				Destroy (tmp);
			}
		}
		else if (sell_Item_Name == "Wolf Skin") 
		{
			for (int i = 0; i < Inventory.ctrSell; i++)
			{
				GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);
				tmp.AddComponent<Items_Script> ();
				Items_Script newMaterials = tmp.GetComponent<Items_Script> ();
				newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials [9];
				tmp_inventory.GetComponent<Inventory> ().addItem (newMaterials);
				Destroy (tmp);
			}
		}
		else if (sell_Item_Name == "Wood") 
		{
			for (int i = 0; i < Inventory.ctrSell; i++)
			{
				GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);
				tmp.AddComponent<Items_Script> ();
				Items_Script newMaterials = tmp.GetComponent<Items_Script> ();
				newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials [10];
				tmp_inventory.GetComponent<Inventory> ().addItem (newMaterials);
				Destroy (tmp);
			}
		}
		else if (sell_Item_Name == "Corn") 
		{
			for (int i = 0; i < Inventory.ctrSell; i++)
			{
				GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);
				tmp.AddComponent<Items_Script> ();
				Items_Script newMaterials = tmp.GetComponent<Items_Script> ();
				newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials [14];
				tmp_inventory.GetComponent<Inventory> ().addItem (newMaterials);
				Destroy (tmp);
			}
		}
		else if (sell_Item_Name == "Tomato") 
		{
			for (int i = 0; i < Inventory.ctrSell; i++)
			{
				GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);
				tmp.AddComponent<Items_Script> ();
				Items_Script newMaterials = tmp.GetComponent<Items_Script> ();
				newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials [13];
				tmp_inventory.GetComponent<Inventory> ().addItem (newMaterials);
				Destroy (tmp);
			}
		}
			
		Yes_No_Button.alpha = 0;
		Inventory.ctrSell = 0;
		sell_Item_Name = "";
		yes_no_Sell = false;
	}
}
