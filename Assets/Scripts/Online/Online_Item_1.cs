using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Online_Item_1 : MonoBehaviour {

    public string[] id;
    public string[] id_Data;

	public string[] items;
	public string[] items_Data;
	public string[] type;
	public string[] type_Data;
	public string[] price;
	public string[] price_Data;
    
	public bool[] items_Place;

    public string[] id_online;
    public string[] id_Data_online;

    public string[] items_online;
    public string[] items_Data_online;
    public string[] type_online;
    public string[] type_Data_online;
    public string[] price_online;
    public string[] price_Data_online;

    public bool[] items_Place_online;

    int ctr_Data;
    float ctr_Data_Online;

    string url_select = "http://keefem.000webhostapp.com/Select_Json.php";
    string url_select_online = "http://keefem.000webhostapp.com/Select_Json_Online.php";

    //string url_select = "http://fldstts.000webhostapp.com/Select_Json.php";
    //string url_select_online = "http://fldstts.000webhostapp.com/Select_Json_Online.php";

    GameObject tmp_inventory;

    public Button Btn_Get_Item_0;

    bool Destroy_Data;
    bool Destroy_Data_Online;
    bool check_Index_Buy;

    // Use this for initialization
    void Start () 
	{
        tmp_inventory = GameObject.Find("Inventory");
     
        /*
        WWW itemsData = new WWW ("http://keefem.000webhostapp.com/Select_Json.php");

        //WWWForm form = new WWWForm();
        //form.AddField("api", "darren_select");
        //WWW url = new WWW(url_select, form);

        yield return itemsData;

        string itemsDataString = itemsData.text;

        id = itemsDataString.Split(';');
		items = itemsDataString.Split (';');
		type = itemsDataString.Split (';');
		price = itemsDataString.Split (';');

		items_Place = new bool[items.Length];

        id_Data = new string[items.Length];
        items_Data = new string[items.Length];
		type_Data = new string[items.Length];
		price_Data = new string[items.Length];
        
		ctr_Data = 0;

		for (int i = 0; i < items.Length - 1 ; i++) 
		{
            items_Place[i] = true;

            id_Data[i] = GetDataValue(items[i], "Id:");
            items_Data[i] = GetDataValue(items[i], "Name:");
            type_Data[i] = GetDataValue(items[i], "Type:");
            price_Data[i] = GetDataValue(items[i], "Price:");

            ctr_Data++;
            /*
            items_Place [i] = true;
			items_Data [i] = GetDataValue (items [i], "Name:");
			type_Data [i] = GetDataValue (type [i], "Type:");
			price_Data [i] = GetDataValue (price [i], "Price:");
			ctr_Data++;
            
		}
        */
	}

	string GetDataValue(string data, string index)
	{
		string value = data.Substring (data.IndexOf (index) + index.Length);
		if (value.Contains ("|")) 
		{
			value = value.Remove (value.IndexOf ("|"));
		}
		return value;
	}
    
    public void GetItemOnline_Buy(int index_buy)
    {
        check_Index_Buy = false;

        string url_Delete = "http://keefem.000webhostapp.com/Delete_Json.php";
        //string url_Delete = "http://fldstts.000webhostapp.com/Delete_Json.php";

        string url_Gold = "http://keefem.000webhostapp.com/Save_Online_Gold.php";
        //string url_Gold = "http://fldstts.000webhostapp.com/Save_Online_Gold.php";

        if (!check_Index_Buy)
        {
            if (BottomPage <= 1)
            {
                BottomPage = 1;
                page = 1;
            }

            if (page >= totalPage)
            {
                page = totalPage;
            }

            index_buy = index_buy + ((page - 1) * 6);
            check_Index_Buy = true;
        }

        if (GameObject.Find("Img_Buy_" + (index_buy - ((page - 1) * 6) + 1)).GetComponent<CanvasGroup>().alpha == 1)
        {
            //Consumables
            if (items_Data_online[index_buy] == "HP Potion" && GameMaster.Player_Gold >= int.Parse(price_Data_online[index_buy]))
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold - int.Parse(price_Data_online[index_buy]);

                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newConsumable = tmp.GetComponent<Items_Script>();
                newConsumable.Item = Inventory_Manager.Instance.ItemContainer.Consumables[0];
                tmp_inventory.GetComponent<Inventory>().addItem(newConsumable);
                Destroy(tmp);

                Destroy_Data_Online = true;
            }

            //Swords
            else if (items_Data_online[index_buy] == "Rare Sword" && GameMaster.Player_Gold >= int.Parse(price_Data_online[index_buy]))
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold - int.Parse(price_Data_online[index_buy]);

                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newWeapon = tmp.GetComponent<Items_Script>();
                newWeapon.Item = (Items)Inventory_Manager.Instance.ItemContainer.Weapons[0].Clone();
                tmp_inventory.GetComponent<Inventory>().addItem(newWeapon);
                Destroy(tmp);

                Destroy_Data_Online = true;
            }
            else if (items_Data_online[index_buy] == "Decent Sword" && GameMaster.Player_Gold >= int.Parse(price_Data_online[index_buy]))
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold - int.Parse(price_Data_online[index_buy]);

                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newWeapon = tmp.GetComponent<Items_Script>();
                newWeapon.Item = (Items)Inventory_Manager.Instance.ItemContainer.Weapons[1].Clone();
                tmp_inventory.GetComponent<Inventory>().addItem(newWeapon);
                Destroy(tmp);

                Destroy_Data_Online = true;
            }
            else if (items_Data_online[index_buy] == "Bone Sword" && GameMaster.Player_Gold >= int.Parse(price_Data_online[index_buy]))
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold - int.Parse(price_Data_online[index_buy]);

                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newWeapon = tmp.GetComponent<Items_Script>();
                newWeapon.Item = (Items)Inventory_Manager.Instance.ItemContainer.Weapons[2].Clone();
                tmp_inventory.GetComponent<Inventory>().addItem(newWeapon);
                Destroy(tmp);

                Destroy_Data_Online = true;
            }
            else if (items_Data_online[index_buy] == "Wooden Sword" && GameMaster.Player_Gold >= int.Parse(price_Data_online[index_buy]))
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold - int.Parse(price_Data_online[index_buy]);

                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newWeapon = tmp.GetComponent<Items_Script>();
                newWeapon.Item = (Items)Inventory_Manager.Instance.ItemContainer.Weapons[3].Clone();
                tmp_inventory.GetComponent<Inventory>().addItem(newWeapon);
                Destroy(tmp);

                Destroy_Data_Online = true;
            }
            else if (items_Data_online[index_buy] == "Machette" && GameMaster.Player_Gold >= int.Parse(price_Data_online[index_buy]))
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold - int.Parse(price_Data_online[index_buy]);

                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newWeapon = tmp.GetComponent<Items_Script>();
                newWeapon.Item = (Items)Inventory_Manager.Instance.ItemContainer.Weapons[4].Clone();
                tmp_inventory.GetComponent<Inventory>().addItem(newWeapon);
                Destroy(tmp);

                Destroy_Data_Online = true;
            }

            //Shields
            else if (items_Data_online[index_buy] == "Knight Shield" && GameMaster.Player_Gold >= int.Parse(price_Data_online[index_buy]))
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold - int.Parse(price_Data_online[index_buy]);

                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newShield = tmp.GetComponent<Items_Script>();
                newShield.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[1].Clone();
                tmp_inventory.GetComponent<Inventory>().addItem(newShield);
                Destroy(tmp);

                Destroy_Data_Online = true;
            }
            else if (items_Data_online[index_buy] == "Wooden Shield" && GameMaster.Player_Gold >= int.Parse(price_Data_online[index_buy]))
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold - int.Parse(price_Data_online[index_buy]);

                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newShield = tmp.GetComponent<Items_Script>();
                newShield.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[6].Clone();
                tmp_inventory.GetComponent<Inventory>().addItem(newShield);
                Destroy(tmp);

                Destroy_Data_Online = true;
            }
            else if (items_Data_online[index_buy] == "Normal Shield" && GameMaster.Player_Gold >= int.Parse(price_Data_online[index_buy]))
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold - int.Parse(price_Data_online[index_buy]);

                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newShield = tmp.GetComponent<Items_Script>();
                newShield.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[7].Clone();
                tmp_inventory.GetComponent<Inventory>().addItem(newShield);
                Destroy(tmp);

                Destroy_Data_Online = true;
            }
            else if (items_Data_online[index_buy] == "Steel Shield" && GameMaster.Player_Gold >= int.Parse(price_Data_online[index_buy]))
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold - int.Parse(price_Data_online[index_buy]);

                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newShield = tmp.GetComponent<Items_Script>();
                newShield.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[8].Clone();
                tmp_inventory.GetComponent<Inventory>().addItem(newShield);
                Destroy(tmp);

                Destroy_Data_Online = true;
            }
            else if (items_Data_online[index_buy] == "Rare Shield" && GameMaster.Player_Gold >= int.Parse(price_Data_online[index_buy]))
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold - int.Parse(price_Data_online[index_buy]);

                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newShield = tmp.GetComponent<Items_Script>();
                newShield.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[9].Clone();
                tmp_inventory.GetComponent<Inventory>().addItem(newShield);
                Destroy(tmp);

                Destroy_Data_Online = true;
            }

            //Armors
            else if (items_Data_online[index_buy] == "Knight Armor" && GameMaster.Player_Gold >= int.Parse(price_Data_online[index_buy]))
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold - int.Parse(price_Data_online[index_buy]);

                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newArmor = tmp.GetComponent<Items_Script>();
                newArmor.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[0].Clone();
                tmp_inventory.GetComponent<Inventory>().addItem(newArmor);
                Destroy(tmp);

                Destroy_Data_Online = true;
            }
            else if (items_Data_online[index_buy] == "Wooden Armor" && GameMaster.Player_Gold >= int.Parse(price_Data_online[index_buy]))
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold - int.Parse(price_Data_online[index_buy]);

                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newArmor = tmp.GetComponent<Items_Script>();
                newArmor.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[2].Clone();
                tmp_inventory.GetComponent<Inventory>().addItem(newArmor);
                Destroy(tmp);

                Destroy_Data_Online = true;
            }
            else if (items_Data_online[index_buy] == "Plate Wood Armor" && GameMaster.Player_Gold >= int.Parse(price_Data_online[index_buy]))
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold - int.Parse(price_Data_online[index_buy]);

                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newArmor = tmp.GetComponent<Items_Script>();
                newArmor.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[3].Clone();
                tmp_inventory.GetComponent<Inventory>().addItem(newArmor);
                Destroy(tmp);

                Destroy_Data_Online = true;
            }
            else if (items_Data_online[index_buy] == "Steel Armor" && GameMaster.Player_Gold >= int.Parse(price_Data_online[index_buy]))
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold - int.Parse(price_Data_online[index_buy]);

                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newArmor = tmp.GetComponent<Items_Script>();
                newArmor.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[4].Clone();
                tmp_inventory.GetComponent<Inventory>().addItem(newArmor);
                Destroy(tmp);

                Destroy_Data_Online = true;
            }
            else if (items_Data_online[index_buy] == "Rare Armor" && GameMaster.Player_Gold >= int.Parse(price_Data_online[index_buy]))
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold - int.Parse(price_Data_online[index_buy]);

                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newArmor = tmp.GetComponent<Items_Script>();
                newArmor.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[5].Clone();
                tmp_inventory.GetComponent<Inventory>().addItem(newArmor);
                Destroy(tmp);

                Destroy_Data_Online = true;
            }

            //Materials
            else if (items_Data_online[index_buy] == "Bone" && GameMaster.Player_Gold >= int.Parse(price_Data_online[index_buy]))
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold - int.Parse(price_Data_online[index_buy]);

                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newMaterials = tmp.GetComponent<Items_Script>();
                newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials[1];
                tmp_inventory.GetComponent<Inventory>().addItem(newMaterials);
                Destroy(tmp);

                Destroy_Data_Online = true;
            }
            else if (items_Data_online[index_buy] == "Gold Bar" && GameMaster.Player_Gold >= int.Parse(price_Data_online[index_buy]))
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold - int.Parse(price_Data_online[index_buy]);

                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newMaterials = tmp.GetComponent<Items_Script>();
                newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials[2];
                tmp_inventory.GetComponent<Inventory>().addItem(newMaterials);
                Destroy(tmp);

                Destroy_Data_Online = true;
            }
            else if (items_Data_online[index_buy] == "Golden Horn" && GameMaster.Player_Gold >= int.Parse(price_Data_online[index_buy]))
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold - int.Parse(price_Data_online[index_buy]);

                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newMaterials = tmp.GetComponent<Items_Script>();
                newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials[3];
                tmp_inventory.GetComponent<Inventory>().addItem(newMaterials);
                Destroy(tmp);

                Destroy_Data_Online = true;
            }
            else if (items_Data_online[index_buy] == "Iron Bar" && GameMaster.Player_Gold >= int.Parse(price_Data_online[index_buy]))
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold - int.Parse(price_Data_online[index_buy]);

                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newMaterials = tmp.GetComponent<Items_Script>();
                newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials[4];
                tmp_inventory.GetComponent<Inventory>().addItem(newMaterials);
                Destroy(tmp);

                Destroy_Data_Online = true;
            }
            else if (items_Data_online[index_buy] == "Iron Pot" && GameMaster.Player_Gold >= int.Parse(price_Data_online[index_buy]))
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold - int.Parse(price_Data_online[index_buy]);

                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newMaterials = tmp.GetComponent<Items_Script>();
                newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials[5];
                tmp_inventory.GetComponent<Inventory>().addItem(newMaterials);
                Destroy(tmp);

                Destroy_Data_Online = true;
            }
            else if (items_Data_online[index_buy] == "Slime Gem" && GameMaster.Player_Gold >= int.Parse(price_Data_online[index_buy]))
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold - int.Parse(price_Data_online[index_buy]);

                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newMaterials = tmp.GetComponent<Items_Script>();
                newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials[6];
                tmp_inventory.GetComponent<Inventory>().addItem(newMaterials);
                Destroy(tmp);

                Destroy_Data_Online = true;
            }
            else if (items_Data_online[index_buy] == "Slime Liquid" && GameMaster.Player_Gold >= int.Parse(price_Data_online[index_buy]))
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold - int.Parse(price_Data_online[index_buy]);

                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newMaterials = tmp.GetComponent<Items_Script>();
                newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials[7];
                tmp_inventory.GetComponent<Inventory>().addItem(newMaterials);
                Destroy(tmp);

                Destroy_Data_Online = true;
            }
            else if (items_Data_online[index_buy] == "Wolf Fang" && GameMaster.Player_Gold >= int.Parse(price_Data_online[index_buy]))
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold - int.Parse(price_Data_online[index_buy]);

                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newMaterials = tmp.GetComponent<Items_Script>();
                newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials[8];
                tmp_inventory.GetComponent<Inventory>().addItem(newMaterials);
                Destroy(tmp);

                Destroy_Data_Online = true;
            }
            else if (items_Data_online[index_buy] == "Wolf Skin" && GameMaster.Player_Gold >= int.Parse(price_Data_online[index_buy]))
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold - int.Parse(price_Data_online[index_buy]);

                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newMaterials = tmp.GetComponent<Items_Script>();
                newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials[9];
                tmp_inventory.GetComponent<Inventory>().addItem(newMaterials);
                Destroy(tmp);

                Destroy_Data_Online = true;
            }
            else if (items_Data_online[index_buy] == "Wood" && GameMaster.Player_Gold >= int.Parse(price_Data_online[index_buy]))
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold - int.Parse(price_Data_online[index_buy]);

                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newMaterials = tmp.GetComponent<Items_Script>();
                newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials[10];
                tmp_inventory.GetComponent<Inventory>().addItem(newMaterials);
                Destroy(tmp);

                Destroy_Data_Online = true;
            }
            else if (items_Data_online[index_buy] == "Corn" && GameMaster.Player_Gold >= int.Parse(price_Data_online[index_buy]))
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold - int.Parse(price_Data_online[index_buy]);

                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newMaterials = tmp.GetComponent<Items_Script>();
                newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials[14];
                tmp_inventory.GetComponent<Inventory>().addItem(newMaterials);
                Destroy(tmp);

                Destroy_Data_Online = true;
            }
            else if (items_Data_online[index_buy] == "Tomato" && GameMaster.Player_Gold >= int.Parse(price_Data_online[index_buy]))
            {
                GameMaster.Player_Gold = GameMaster.Player_Gold - int.Parse(price_Data_online[index_buy]);

                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newMaterials = tmp.GetComponent<Items_Script>();
                newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials[13];
                tmp_inventory.GetComponent<Inventory>().addItem(newMaterials);
                Destroy(tmp);

                Destroy_Data_Online = true;
            }
        }
        
        if (Destroy_Data_Online)
        {
            WWWForm form_Id_Items = new WWWForm();
            form_Id_Items.AddField("item_Id", id_Data_online[index_buy]);
            WWW url_Delete_Items = new WWW(url_Delete, form_Id_Items);

            System.Threading.Thread.Sleep(50);

            Debug.Log("Total : " + id_Data_online.Length);
            Debug.Log("ID : " + id_Data_online[index_buy]);
            
            WWWForm form_Save_Gold = new WWWForm();
            form_Save_Gold.AddField("username", username_Shop_Data[index_buy]);
            form_Save_Gold.AddField("gold", price_Data_online[index_buy]);

            WWW url_Save_Level_WWW = new WWW(url_Gold, form_Save_Gold);
            System.Threading.Thread.Sleep(50);

            if (ctr_Data_Online == 1)
            {
                Destroy_Data_Online = true;
            }
            else
            {
                Destroy_Data_Online = false;
            }

            //Save.save_Anything_Player = true;
        }

    }

    public void GetItemOnline(int index)
    {
        string url_Delete = "http://keefem.000webhostapp.com/Delete_Json.php";
        //string url_Delete = "http://fldstts.000webhostapp.com/Delete_Json.php";

        if (GameObject.Find("Img_" + (index + 1)).GetComponent<CanvasGroup>().alpha == 1)
        {
            //Consumables
            if (items_Data[index] == "HP Potion")
            {
                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newConsumable = tmp.GetComponent<Items_Script>();
                newConsumable.Item = Inventory_Manager.Instance.ItemContainer.Consumables[0];
                tmp_inventory.GetComponent<Inventory>().addItem(newConsumable);
                Destroy(tmp);

                Destroy_Data = true;
            }

            //Swords
            else if (items_Data[index] == "Rare Sword")
            {
                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newWeapon = tmp.GetComponent<Items_Script>();
                newWeapon.Item = (Items)Inventory_Manager.Instance.ItemContainer.Weapons[0].Clone();
                tmp_inventory.GetComponent<Inventory>().addItem(newWeapon);
                Destroy(tmp);

                Destroy_Data = true;
            }
            else if (items_Data[index] == "Decent Sword")
            {
                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newWeapon = tmp.GetComponent<Items_Script>();
                newWeapon.Item = (Items)Inventory_Manager.Instance.ItemContainer.Weapons[1].Clone();
                tmp_inventory.GetComponent<Inventory>().addItem(newWeapon);
                Destroy(tmp);

                Destroy_Data = true;
            }
            else if (items_Data[index] == "Bone Sword")
            {
                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newWeapon = tmp.GetComponent<Items_Script>();
                newWeapon.Item = (Items)Inventory_Manager.Instance.ItemContainer.Weapons[2].Clone();
                tmp_inventory.GetComponent<Inventory>().addItem(newWeapon);
                Destroy(tmp);

                Destroy_Data = true;
            }
            else if (items_Data[index] == "Wooden Sword")
            {
                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newWeapon = tmp.GetComponent<Items_Script>();
                newWeapon.Item = (Items)Inventory_Manager.Instance.ItemContainer.Weapons[3].Clone();
                tmp_inventory.GetComponent<Inventory>().addItem(newWeapon);
                Destroy(tmp);

                Destroy_Data = true;
            }
            else if (items_Data[index] == "Machette")
            {
                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newWeapon = tmp.GetComponent<Items_Script>();
                newWeapon.Item = (Items)Inventory_Manager.Instance.ItemContainer.Weapons[4].Clone();
                tmp_inventory.GetComponent<Inventory>().addItem(newWeapon);
                Destroy(tmp);

                Destroy_Data = true;
            }

            //Shields
            else if (items_Data[index] == "Knight Shield")
            {
                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newShield = tmp.GetComponent<Items_Script>();
                newShield.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[1].Clone();
                tmp_inventory.GetComponent<Inventory>().addItem(newShield);
                Destroy(tmp);

                Destroy_Data = true;
            }
            else if (items_Data[index] == "Wooden Shield")
            {
                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newShield = tmp.GetComponent<Items_Script>();
                newShield.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[6].Clone();
                tmp_inventory.GetComponent<Inventory>().addItem(newShield);
                Destroy(tmp);

                Destroy_Data = true;
            }
            else if (items_Data[index] == "Normal Shield")
            {
                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newShield = tmp.GetComponent<Items_Script>();
                newShield.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[7].Clone();
                tmp_inventory.GetComponent<Inventory>().addItem(newShield);
                Destroy(tmp);

                Destroy_Data = true;
            }
            else if (items_Data[index] == "Steel Shield")
            {
                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newShield = tmp.GetComponent<Items_Script>();
                newShield.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[8].Clone();
                tmp_inventory.GetComponent<Inventory>().addItem(newShield);
                Destroy(tmp);

                Destroy_Data = true;
            }
            else if (items_Data[index] == "Rare Shield")
            {
                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newShield = tmp.GetComponent<Items_Script>();
                newShield.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[9].Clone();
                tmp_inventory.GetComponent<Inventory>().addItem(newShield);
                Destroy(tmp);

                Destroy_Data = true;
            }

            //Armors
            else if (items_Data[index] == "Knight Armor")
            {
                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newArmor = tmp.GetComponent<Items_Script>();
                newArmor.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[0].Clone();
                tmp_inventory.GetComponent<Inventory>().addItem(newArmor);
                Destroy(tmp);

                Destroy_Data = true;
            }
            else if (items_Data[index] == "Wooden Armor")
            {
                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newArmor = tmp.GetComponent<Items_Script>();
                newArmor.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[2].Clone();
                tmp_inventory.GetComponent<Inventory>().addItem(newArmor);
                Destroy(tmp);

                Destroy_Data = true;
            }
            else if (items_Data[index] == "Plate Wood Armor")
            {
                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newArmor = tmp.GetComponent<Items_Script>();
                newArmor.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[3].Clone();
                tmp_inventory.GetComponent<Inventory>().addItem(newArmor);
                Destroy(tmp);

                Destroy_Data = true;
            }
            else if (items_Data[index] == "Steel Armor")
            {
                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newArmor = tmp.GetComponent<Items_Script>();
                newArmor.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[4].Clone();
                tmp_inventory.GetComponent<Inventory>().addItem(newArmor);
                Destroy(tmp);

                Destroy_Data = true;
            }
            else if (items_Data[index] == "Rare Armor")
            {
                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newArmor = tmp.GetComponent<Items_Script>();
                newArmor.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[5].Clone();
                tmp_inventory.GetComponent<Inventory>().addItem(newArmor);
                Destroy(tmp);

                Destroy_Data = true;
            }

            //Materials
            else if (items_Data[index] == "Bone")
            {
                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newMaterials = tmp.GetComponent<Items_Script>();
                newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials[1];
                tmp_inventory.GetComponent<Inventory>().addItem(newMaterials);
                Destroy(tmp);

                Destroy_Data = true;
            }
            else if (items_Data[index] == "Gold Bar")
            {
                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newMaterials = tmp.GetComponent<Items_Script>();
                newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials[2];
                tmp_inventory.GetComponent<Inventory>().addItem(newMaterials);
                Destroy(tmp);

                Destroy_Data = true;
            }
            else if (items_Data[index] == "Golden Horn")
            {
                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newMaterials = tmp.GetComponent<Items_Script>();
                newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials[3];
                tmp_inventory.GetComponent<Inventory>().addItem(newMaterials);
                Destroy(tmp);

                Destroy_Data = true;
            }
            else if (items_Data[index] == "Iron Bar")
            {
                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newMaterials = tmp.GetComponent<Items_Script>();
                newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials[4];
                tmp_inventory.GetComponent<Inventory>().addItem(newMaterials);
                Destroy(tmp);

                Destroy_Data = true;
            }
            else if (items_Data[index] == "Iron Pot")
            {
                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newMaterials = tmp.GetComponent<Items_Script>();
                newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials[5];
                tmp_inventory.GetComponent<Inventory>().addItem(newMaterials);
                Destroy(tmp);

                Destroy_Data = true;
            }
            else if (items_Data[index] == "Slime Gem")
            {
                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newMaterials = tmp.GetComponent<Items_Script>();
                newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials[6];
                tmp_inventory.GetComponent<Inventory>().addItem(newMaterials);
                Destroy(tmp);

                Destroy_Data = true;
            }
            else if (items_Data[index] == "Slime Liquid")
            {
                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newMaterials = tmp.GetComponent<Items_Script>();
                newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials[7];
                tmp_inventory.GetComponent<Inventory>().addItem(newMaterials);
                Destroy(tmp);

                Destroy_Data = true;
            }
            else if (items_Data[index] == "Wolf Fang")
            {
                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newMaterials = tmp.GetComponent<Items_Script>();
                newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials[8];
                tmp_inventory.GetComponent<Inventory>().addItem(newMaterials);
                Destroy(tmp);

                Destroy_Data = true;
            }
            else if (items_Data[index] == "Wolf Skin")
            {
                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newMaterials = tmp.GetComponent<Items_Script>();
                newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials[9];
                tmp_inventory.GetComponent<Inventory>().addItem(newMaterials);
                Destroy(tmp);

                Destroy_Data = true;
            }
            else if (items_Data[index] == "Wood")
            {
                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newMaterials = tmp.GetComponent<Items_Script>();
                newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials[10];
                tmp_inventory.GetComponent<Inventory>().addItem(newMaterials);
                Destroy(tmp);

                Destroy_Data = true;
            }
            else if (items_Data[index] == "Corn")
            {
                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newMaterials = tmp.GetComponent<Items_Script>();
                newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials[14];
                tmp_inventory.GetComponent<Inventory>().addItem(newMaterials);
                Destroy(tmp);

                Destroy_Data = true;
            }
            else if (items_Data[index] == "Tomato")
            {
                GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                tmp.AddComponent<Items_Script>();
                Items_Script newMaterials = tmp.GetComponent<Items_Script>();
                newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials[13];
                tmp_inventory.GetComponent<Inventory>().addItem(newMaterials);
                Destroy(tmp);

                Destroy_Data = true;
            }
        }

        if (Destroy_Data)
        {
            WWWForm form = new WWWForm();
            form.AddField("item_Id", id_Data[index]);
            WWW url = new WWW(url_Delete, form);

            if (ctr_Data == 1)
            {
                Destroy_Data = true;
            }
            else
            {
                Destroy_Data = false;
            }
        }

        //Save.save_Anything_Player = true;
    }

    public static int page = 1;
    public static int BottomPage;
    public static int totalPage;
    
	// Update is called once per frame
	void Update () 
	{
        Check_Online_Shop();

        if (GameMaster.Online && !Button_Shop.Online_Buy)
        {
            for (int i = 0; i < ctr_Data; i++)
            {
                if (items_Place[i])
                {
                    //Consumables
                    if (items_Data[i] == "HP Potion")
                    {
                        GameObject.Find("Img_" + (i + 1)).GetComponent<Image>().sprite = Resources.Load<Sprite>("Consumables/HP_Potion");
                        GameObject.Find("Txt_" + (i + 1)).GetComponent<Text>().text = "Type : " + type_Data[i] + " " + items_Data[i] + " " + price_Data[i] + "G";
                    }

                    //Weapons
                    else if (items_Data[i] == "Rare Sword")
                    {
                        GameObject.Find("Img_" + (i + 1)).GetComponent<Image>().sprite = Resources.Load<Sprite>("Weapons/Sword");
                        GameObject.Find("Txt_" + (i + 1)).GetComponent<Text>().text = "Type : " + type_Data[i] + " " + items_Data[i] + " " + price_Data[i] + "G";
                    }
                    else if (items_Data[i] == "Decent Sword")
                    {
                        GameObject.Find("Img_" + (i + 1)).GetComponent<Image>().sprite = Resources.Load<Sprite>("Weapons/Sword_1");
                        GameObject.Find("Txt_" + (i + 1)).GetComponent<Text>().text = "Type : " + type_Data[i] + " " + items_Data[i] + " " + price_Data[i] + "G";
                    }
                    else if (items_Data[i] == "Bone Sword")
                    {
                        GameObject.Find("Img_" + (i + 1)).GetComponent<Image>().sprite = Resources.Load<Sprite>("Weapons/Sword_2");
                        GameObject.Find("Txt_" + (i + 1)).GetComponent<Text>().text = "Type : " + type_Data[i] + " " + items_Data[i] + " " + price_Data[i] + "G";
                    }
                    else if (items_Data[i] == "Wooden Sword")
                    {
                        GameObject.Find("Img_" + (i + 1)).GetComponent<Image>().sprite = Resources.Load<Sprite>("Weapons/Sword_3");
                        GameObject.Find("Txt_" + (i + 1)).GetComponent<Text>().text = "Type : " + type_Data[i] + " " + items_Data[i] + " " + price_Data[i] + "G";
                    }
                    else if (items_Data[i] == "Machette")
                    {
                        GameObject.Find("Img_" + (i + 1)).GetComponent<Image>().sprite = Resources.Load<Sprite>("Weapons/Sword_4");
                        GameObject.Find("Txt_" + (i + 1)).GetComponent<Text>().text = "Type : " + type_Data[i] + " " + items_Data[i] + " " + price_Data[i] + "G";
                    }

                    //Shields
                    else if (items_Data[i] == "Knight Shield")
                    {
                        GameObject.Find("Img_" + (i + 1)).GetComponent<Image>().sprite = Resources.Load<Sprite>("Shields/Shield");
                        GameObject.Find("Txt_" + (i + 1)).GetComponent<Text>().text = "Type : " + type_Data[i] + " " + items_Data[i] + " " + price_Data[i] + "G";
                    }
                    else if (items_Data[i] == "Wooden Shield")
                    {
                        GameObject.Find("Img_" + (i + 1)).GetComponent<Image>().sprite = Resources.Load<Sprite>("Shields/Shield_1");
                        GameObject.Find("Txt_" + (i + 1)).GetComponent<Text>().text = "Type : " + type_Data[i] + " " + items_Data[i] + " " + price_Data[i] + "G";
                    }
                    else if (items_Data[i] == "Normal Shield")
                    {
                        GameObject.Find("Img_" + (i + 1)).GetComponent<Image>().sprite = Resources.Load<Sprite>("Shields/Shield_2");
                        GameObject.Find("Txt_" + (i + 1)).GetComponent<Text>().text = "Type : " + type_Data[i] + " " + items_Data[i] + " " + price_Data[i] + "G";
                    }
                    else if (items_Data[i] == "Steel Shield")
                    {
                        GameObject.Find("Img_" + (i + 1)).GetComponent<Image>().sprite = Resources.Load<Sprite>("Shields/Shield_3");
                        GameObject.Find("Txt_" + (i + 1)).GetComponent<Text>().text = "Type : " + type_Data[i] + " " + items_Data[i] + " " + price_Data[i] + "G";
                    }
                    else if (items_Data[i] == "Rare Shield")
                    {
                        GameObject.Find("Img_" + (i + 1)).GetComponent<Image>().sprite = Resources.Load<Sprite>("Shields/Shield_4");
                        GameObject.Find("Txt_" + (i + 1)).GetComponent<Text>().text = "Type : " + type_Data[i] + " " + items_Data[i] + " " + price_Data[i] + "G";
                    }

                    //Armors
                    else if (items_Data[i] == "Knight Armor")
                    {
                        GameObject.Find("Img_" + (i + 1)).GetComponent<Image>().sprite = Resources.Load<Sprite>("Armors/Armor");
                        GameObject.Find("Txt_" + (i + 1)).GetComponent<Text>().text = "Type : " + type_Data[i] + " " + items_Data[i] + " " + price_Data[i] + "G";
                    }
                    else if (items_Data[i] == "Wooden Armor")
                    {
                        GameObject.Find("Img_" + (i + 1)).GetComponent<Image>().sprite = Resources.Load<Sprite>("Armors/Armor_1");
                        GameObject.Find("Txt_" + (i + 1)).GetComponent<Text>().text = "Type : " + type_Data[i] + " " + items_Data[i] + " " + price_Data[i] + "G";
                    }
                    else if (items_Data[i] == "Plate Wood Armor")
                    {
                        GameObject.Find("Img_" + (i + 1)).GetComponent<Image>().sprite = Resources.Load<Sprite>("Armors/Armor_2");
                        GameObject.Find("Txt_" + (i + 1)).GetComponent<Text>().text = "Type : " + type_Data[i] + " " + items_Data[i] + " " + price_Data[i] + "G";
                    }
                    else if (items_Data[i] == "Steel Armor")
                    {
                        GameObject.Find("Img_" + (i + 1)).GetComponent<Image>().sprite = Resources.Load<Sprite>("Armors/Armor_3");
                        GameObject.Find("Txt_" + (i + 1)).GetComponent<Text>().text = "Type : " + type_Data[i] + " " + items_Data[i] + " " + price_Data[i] + "G";
                    }
                    else if (items_Data[i] == "Rare Armor")
                    {
                        GameObject.Find("Img_" + (i + 1)).GetComponent<Image>().sprite = Resources.Load<Sprite>("Armors/Armor_4");
                        GameObject.Find("Txt_" + (i + 1)).GetComponent<Text>().text = "Type : " + type_Data[i] + " " + items_Data[i] + " " + price_Data[i] + "G";
                    }

                    //Materials
                    else if (items_Data[i] == "Bone")
                    {
                        GameObject.Find("Img_" + (i + 1)).GetComponent<Image>().sprite = Resources.Load<Sprite>("Materials/Bone");
                        GameObject.Find("Txt_" + (i + 1)).GetComponent<Text>().text = "Type : " + type_Data[i] + " " + items_Data[i] + " " + price_Data[i] + "G";
                    }
                    else if (items_Data[i] == "Gold Bar")
                    {
                        GameObject.Find("Img_" + (i + 1)).GetComponent<Image>().sprite = Resources.Load<Sprite>("Materials/Gold_Bar");
                        GameObject.Find("Txt_" + (i + 1)).GetComponent<Text>().text = "Type : " + type_Data[i] + " " + items_Data[i] + " " + price_Data[i] + "G";
                    }
                    else if (items_Data[i] == "Golden Horn")
                    {
                        GameObject.Find("Img_" + (i + 1)).GetComponent<Image>().sprite = Resources.Load<Sprite>("Materials/Golden_Horn");
                        GameObject.Find("Txt_" + (i + 1)).GetComponent<Text>().text = "Type : " + type_Data[i] + " " + items_Data[i] + " " + price_Data[i] + "G";
                    }
                    else if (items_Data[i] == "Iron Bar")
                    {
                        GameObject.Find("Img_" + (i + 1)).GetComponent<Image>().sprite = Resources.Load<Sprite>("Materials/Iron_Bar");
                        GameObject.Find("Txt_" + (i + 1)).GetComponent<Text>().text = "Type : " + type_Data[i] + " " + items_Data[i] + " " + price_Data[i] + "G";
                    }
                    else if (items_Data[i] == "Iron Pot")
                    {
                        GameObject.Find("Img_" + (i + 1)).GetComponent<Image>().sprite = Resources.Load<Sprite>("Materials/Iron_Pot");
                        GameObject.Find("Txt_" + (i + 1)).GetComponent<Text>().text = "Type : " + type_Data[i] + " " + items_Data[i] + " " + price_Data[i] + "G";
                    }
                    else if (items_Data[i] == "Slime Gem")
                    {
                        GameObject.Find("Img_" + (i + 1)).GetComponent<Image>().sprite = Resources.Load<Sprite>("Materials/Slime_Gem");
                        GameObject.Find("Txt_" + (i + 1)).GetComponent<Text>().text = "Type : " + type_Data[i] + " " + items_Data[i] + " " + price_Data[i] + "G";
                    }
                    else if (items_Data[i] == "Slime Liquid")
                    {
                        GameObject.Find("Img_" + (i + 1)).GetComponent<Image>().sprite = Resources.Load<Sprite>("Materials/Slime_Liquid");
                        GameObject.Find("Txt_" + (i + 1)).GetComponent<Text>().text = "Type : " + type_Data[i] + " " + items_Data[i] + " " + price_Data[i] + "G";
                    }
                    else if (items_Data[i] == "Wolf Fang")
                    {
                        GameObject.Find("Img_" + (i + 1)).GetComponent<Image>().sprite = Resources.Load<Sprite>("Materials/Wolf_Fang");
                        GameObject.Find("Txt_" + (i + 1)).GetComponent<Text>().text = "Type : " + type_Data[i] + " " + items_Data[i] + " " + price_Data[i] + "G";
                    }
                    else if (items_Data[i] == "Wolf Skin")
                    {
                        GameObject.Find("Img_" + (i + 1)).GetComponent<Image>().sprite = Resources.Load<Sprite>("Materials/Wolf_Skin");
                        GameObject.Find("Txt_" + (i + 1)).GetComponent<Text>().text = "Type : " + type_Data[i] + " " + items_Data[i] + " " + price_Data[i] + "G";
                    }
                    else if (items_Data[i] == "Wood")
                    {
                        GameObject.Find("Img_" + (i + 1)).GetComponent<Image>().sprite = Resources.Load<Sprite>("Materials/Wood");
                        GameObject.Find("Txt_" + (i + 1)).GetComponent<Text>().text = "Type : " + type_Data[i] + " " + items_Data[i] + " " + price_Data[i] + "G";
                    }
                    else if (items_Data[i] == "Tomato")
                    {
                        GameObject.Find("Img_" + (i + 1)).GetComponent<Image>().sprite = Resources.Load<Sprite>("Materials/Tomato");
                        GameObject.Find("Txt_" + (i + 1)).GetComponent<Text>().text = "Type : " + type_Data[i] + " " + items_Data[i] + " " + price_Data[i] + "G";
                    }
                    else if (items_Data[i] == "Corn")
                    {
                        GameObject.Find("Img_" + (i + 1)).GetComponent<Image>().sprite = Resources.Load<Sprite>("Materials/Corn");
                        GameObject.Find("Txt_" + (i + 1)).GetComponent<Text>().text = "Type : " + type_Data[i] + " " + items_Data[i] + " " + price_Data[i] + "G";
                    }
                }
                if (items_Place[i])
                {
                    if (ctr_Data != 1)
                    {
                        GameObject.Find("Img_" + (i + 1)).GetComponent<CanvasGroup>().alpha = 1;
                        GameObject.Find("Img_" + (ctr_Data + 1)).GetComponent<CanvasGroup>().alpha = 0;
                    }
                    else if (ctr_Data == 1)
                    {
                        GameObject.Find("Img_" + (ctr_Data + 1)).GetComponent<CanvasGroup>().alpha = 0;
                    }
                }

                if (ctr_Data == 1 && Destroy_Data)
                {
                    GameObject.Find("Img_1").GetComponent<CanvasGroup>().alpha = 0;
                    Destroy_Data = false;
                }

                /*
                if (GameObject.Find("Img_" + (i + 1)).GetComponent<Image>().sprite.name == "Apple")
                {
                    //GameObject.Find("Img_" + (i + 1)).GetComponent<CanvasGroup>().alpha = 0;
                }
                else
                {
                    //GameObject.Find("Img_" + (i + 1)).GetComponent<CanvasGroup>().alpha = 1;
                }
                */
            }
            
        }
        else if (GameMaster.Online && Button_Shop.Online_Buy)
        {
            int maxPage = 6;
            totalPage = Mathf.RoundToInt(Mathf.Ceil(ctr_Data_Online / maxPage));

            BottomPage = Mathf.RoundToInt(((page - 1) * maxPage) + 1);
            int TopPage = Mathf.RoundToInt((page) * maxPage);
            
            if (BottomPage <= 1)
            {
                BottomPage = 1;
                page = 1;
            }

            if (page >= totalPage)
            {
                page = totalPage;
            }

            for (int i = BottomPage - 1; i < TopPage; i++)
            {
                int Temp_Magic = ((i + 1) - (maxPage * (page - 1)));

                if (i < ctr_Data_Online)
                {
                    if (items_Place_online[i])
                    {
                        //Consumables
                        if (items_Data_online[i] == "HP Potion")
                        {
                            GameObject.Find("Img_Buy_" + Temp_Magic).GetComponent<Image>().sprite = Resources.Load<Sprite>("Consumables/HP_Potion");
                            GameObject.Find("Txt_Buy_" + Temp_Magic).GetComponent<Text>().text = "Type : " + type_Data_online[i] + " " + items_Data_online[i] + " " + price_Data_online[i] + "G";
                        }

                        //Weapons
                        else if (items_Data_online[i] == "Rare Sword")
                        {
                            GameObject.Find("Img_Buy_" + Temp_Magic).GetComponent<Image>().sprite = Resources.Load<Sprite>("Weapons/Sword");
                            GameObject.Find("Txt_Buy_" + Temp_Magic).GetComponent<Text>().text = "Type : " + type_Data_online[i] + " " + items_Data_online[i] + " " + price_Data_online[i] + "G";
                        }
                        else if (items_Data_online[i] == "Decent Sword")
                        {
                            GameObject.Find("Img_Buy_" + Temp_Magic).GetComponent<Image>().sprite = Resources.Load<Sprite>("Weapons/Sword_1");
                            GameObject.Find("Txt_Buy_" + Temp_Magic).GetComponent<Text>().text = "Type : " + type_Data_online[i] + " " + items_Data_online[i] + " " + price_Data_online[i] + "G";
                        }
                        else if (items_Data_online[i] == "Bone Sword")
                        {
                            GameObject.Find("Img_Buy_" + Temp_Magic).GetComponent<Image>().sprite = Resources.Load<Sprite>("Weapons/Sword_2");
                            GameObject.Find("Txt_Buy_" + Temp_Magic).GetComponent<Text>().text = "Type : " + type_Data_online[i] + " " + items_Data_online[i] + " " + price_Data_online[i] + "G";
                        }
                        else if (items_Data_online[i] == "Wooden Sword")
                        {
                            GameObject.Find("Img_Buy_" + Temp_Magic).GetComponent<Image>().sprite = Resources.Load<Sprite>("Weapons/Sword_3");
                            GameObject.Find("Txt_Buy_" + Temp_Magic).GetComponent<Text>().text = "Type : " + type_Data_online[i] + " " + items_Data_online[i] + " " + price_Data_online[i] + "G";
                        }
                        else if (items_Data_online[i] == "Machette")
                        {
                            GameObject.Find("Img_Buy_" + Temp_Magic).GetComponent<Image>().sprite = Resources.Load<Sprite>("Weapons/Sword_4");
                            GameObject.Find("Txt_Buy_" + Temp_Magic).GetComponent<Text>().text = "Type : " + type_Data_online[i] + " " + items_Data_online[i] + " " + price_Data_online[i] + "G";
                        }

                        //Shields
                        else if (items_Data_online[i] == "Knight Shield")
                        {
                            GameObject.Find("Img_Buy_" + Temp_Magic).GetComponent<Image>().sprite = Resources.Load<Sprite>("Shields/Shield");
                            GameObject.Find("Txt_Buy_" + Temp_Magic).GetComponent<Text>().text = "Type : " + type_Data_online[i] + " " + items_Data_online[i] + " " + price_Data_online[i] + "G";
                        }
                        else if (items_Data_online[i] == "Wooden Shield")
                        {
                            GameObject.Find("Img_Buy_" + Temp_Magic).GetComponent<Image>().sprite = Resources.Load<Sprite>("Shields/Shield_1");
                            GameObject.Find("Txt_Buy_" + Temp_Magic).GetComponent<Text>().text = "Type : " + type_Data_online[i] + " " + items_Data_online[i] + " " + price_Data_online[i] + "G";
                        }
                        else if (items_Data_online[i] == "Normal Shield")
                        {
                            GameObject.Find("Img_Buy_" + Temp_Magic).GetComponent<Image>().sprite = Resources.Load<Sprite>("Shields/Shield_2");
                            GameObject.Find("Txt_Buy_" + Temp_Magic).GetComponent<Text>().text = "Type : " + type_Data_online[i] + " " + items_Data_online[i] + " " + price_Data_online[i] + "G";
                        }
                        else if (items_Data_online[i] == "Steel Shield")
                        {
                            GameObject.Find("Img_Buy_" + Temp_Magic).GetComponent<Image>().sprite = Resources.Load<Sprite>("Shields/Shield_3");
                            GameObject.Find("Txt_Buy_" + Temp_Magic).GetComponent<Text>().text = "Type : " + type_Data_online[i] + " " + items_Data_online[i] + " " + price_Data_online[i] + "G";
                        }
                        else if (items_Data_online[i] == "Rare Shield")
                        {
                            GameObject.Find("Img_Buy_" + Temp_Magic).GetComponent<Image>().sprite = Resources.Load<Sprite>("Shields/Shield_4");
                            GameObject.Find("Txt_Buy_" + Temp_Magic).GetComponent<Text>().text = "Type : " + type_Data_online[i] + " " + items_Data_online[i] + " " + price_Data_online[i] + "G";
                        }

                        //Armors
                        else if (items_Data_online[i] == "Knight Armor")
                        {
                            GameObject.Find("Img_Buy_" + Temp_Magic).GetComponent<Image>().sprite = Resources.Load<Sprite>("Armors/Armor");
                            GameObject.Find("Txt_Buy_" + Temp_Magic).GetComponent<Text>().text = "Type : " + type_Data_online[i] + " " + items_Data_online[i] + " " + price_Data_online[i] + "G";
                        }
                        else if (items_Data_online[i] == "Wooden Armor")
                        {
                            GameObject.Find("Img_Buy_" + Temp_Magic).GetComponent<Image>().sprite = Resources.Load<Sprite>("Armors/Armor_1");
                            GameObject.Find("Txt_Buy_" + Temp_Magic).GetComponent<Text>().text = "Type : " + type_Data_online[i] + " " + items_Data_online[i] + " " + price_Data_online[i] + "G";
                        }
                        else if (items_Data_online[i] == "Plate Wood Armor")
                        {
                            GameObject.Find("Img_Buy_" + Temp_Magic).GetComponent<Image>().sprite = Resources.Load<Sprite>("Armors/Armor_2");
                            GameObject.Find("Txt_Buy_" + Temp_Magic).GetComponent<Text>().text = "Type : " + type_Data_online[i] + " " + items_Data_online[i] + " " + price_Data_online[i] + "G";
                        }
                        else if (items_Data_online[i] == "Steel Armor")
                        {
                            GameObject.Find("Img_Buy_" + Temp_Magic).GetComponent<Image>().sprite = Resources.Load<Sprite>("Armors/Armor_3");
                            GameObject.Find("Txt_Buy_" + Temp_Magic).GetComponent<Text>().text = "Type : " + type_Data_online[i] + " " + items_Data_online[i] + " " + price_Data_online[i] + "G";
                        }
                        else if (items_Data_online[i] == "Rare Armor")
                        {
                            GameObject.Find("Img_Buy_" + Temp_Magic).GetComponent<Image>().sprite = Resources.Load<Sprite>("Armors/Armor_4");
                            GameObject.Find("Txt_Buy_" + Temp_Magic).GetComponent<Text>().text = "Type : " + type_Data_online[i] + " " + items_Data_online[i] + " " + price_Data_online[i] + "G";
                        }

                        //Materials
                        else if (items_Data_online[i] == "Bone")
                        {
                            GameObject.Find("Img_Buy_" + Temp_Magic).GetComponent<Image>().sprite = Resources.Load<Sprite>("Materials/Bone");
                            GameObject.Find("Txt_Buy_" + Temp_Magic).GetComponent<Text>().text = "Type : " + type_Data_online[i] + " " + items_Data_online[i] + " " + price_Data_online[i] + "G";
                        }
                        else if (items_Data_online[i] == "Gold Bar")
                        {
                            GameObject.Find("Img_Buy_" + Temp_Magic).GetComponent<Image>().sprite = Resources.Load<Sprite>("Materials/Gold_Bar");
                            GameObject.Find("Txt_Buy_" + Temp_Magic).GetComponent<Text>().text = "Type : " + type_Data_online[i] + " " + items_Data_online[i] + " " + price_Data_online[i] + "G";
                        }
                        else if (items_Data_online[i] == "Golden Horn")
                        {
                            GameObject.Find("Img_Buy_" + Temp_Magic).GetComponent<Image>().sprite = Resources.Load<Sprite>("Materials/Golden_Horn");
                            GameObject.Find("Txt_Buy_" + Temp_Magic).GetComponent<Text>().text = "Type : " + type_Data_online[i] + " " + items_Data_online[i] + " " + price_Data_online[i] + "G";
                        }
                        else if (items_Data_online[i] == "Iron Bar")
                        {
                            GameObject.Find("Img_Buy_" + Temp_Magic).GetComponent<Image>().sprite = Resources.Load<Sprite>("Materials/Iron_Bar");
                            GameObject.Find("Txt_Buy_" + Temp_Magic).GetComponent<Text>().text = "Type : " + type_Data_online[i] + " " + items_Data_online[i] + " " + price_Data_online[i] + "G";
                        }
                        else if (items_Data_online[i] == "Iron Pot")
                        {
                            GameObject.Find("Img_Buy_" + Temp_Magic).GetComponent<Image>().sprite = Resources.Load<Sprite>("Materials/Iron_Pot");
                            GameObject.Find("Txt_Buy_" + Temp_Magic).GetComponent<Text>().text = "Type : " + type_Data_online[i] + " " + items_Data_online[i] + " " + price_Data_online[i] + "G";
                        }
                        else if (items_Data_online[i] == "Slime Gem")
                        {
                            GameObject.Find("Img_Buy_" + Temp_Magic).GetComponent<Image>().sprite = Resources.Load<Sprite>("Materials/Slime_Gem");
                            GameObject.Find("Txt_Buy_" + Temp_Magic).GetComponent<Text>().text = "Type : " + type_Data_online[i] + " " + items_Data_online[i] + " " + price_Data_online[i] + "G";
                        }
                        else if (items_Data_online[i] == "Slime Liquid")
                        {
                            GameObject.Find("Img_Buy_" + Temp_Magic).GetComponent<Image>().sprite = Resources.Load<Sprite>("Materials/Slime_Liquid");
                            GameObject.Find("Txt_Buy_" + Temp_Magic).GetComponent<Text>().text = "Type : " + type_Data_online[i] + " " + items_Data_online[i] + " " + price_Data_online[i] + "G";
                        }
                        else if (items_Data_online[i] == "Wolf Fang")
                        {
                            GameObject.Find("Img_Buy_" + Temp_Magic).GetComponent<Image>().sprite = Resources.Load<Sprite>("Materials/Wolf_Fang");
                            GameObject.Find("Txt_Buy_" + Temp_Magic).GetComponent<Text>().text = "Type : " + type_Data_online[i] + " " + items_Data_online[i] + " " + price_Data_online[i] + "G";
                        }
                        else if (items_Data_online[i] == "Wolf Skin")
                        {
                            GameObject.Find("Img_Buy_" + Temp_Magic).GetComponent<Image>().sprite = Resources.Load<Sprite>("Materials/Wolf_Skin");
                            GameObject.Find("Txt_Buy_" + Temp_Magic).GetComponent<Text>().text = "Type : " + type_Data_online[i] + " " + items_Data_online[i] + " " + price_Data_online[i] + "G";
                        }
                        else if (items_Data_online[i] == "Wood")
                        {
                            GameObject.Find("Img_Buy_" + Temp_Magic).GetComponent<Image>().sprite = Resources.Load<Sprite>("Materials/Wood");
                            GameObject.Find("Txt_Buy_" + Temp_Magic).GetComponent<Text>().text = "Type : " + type_Data_online[i] + " " + items_Data_online[i] + " " + price_Data_online[i] + "G";
                        }
                        else if (items_Data_online[i] == "Tomato")
                        {
                            GameObject.Find("Img_Buy_" + Temp_Magic).GetComponent<Image>().sprite = Resources.Load<Sprite>("Materials/Tomato");
                            GameObject.Find("Txt_Buy_" + Temp_Magic).GetComponent<Text>().text = "Type : " + type_Data_online[i] + " " + items_Data_online[i] + " " + price_Data_online[i] + "G";
                        }
                        else if (items_Data_online[i] == "Corn")
                        {
                            GameObject.Find("Img_Buy_" + Temp_Magic).GetComponent<Image>().sprite = Resources.Load<Sprite>("Materials/Corn");
                            GameObject.Find("Txt_Buy_" + Temp_Magic).GetComponent<Text>().text = "Type : " + type_Data_online[i] + " " + items_Data_online[i] + " " + price_Data_online[i] + "G";
                        }
                        else if (items_Data_online[i] == "")
                        {
                            GameObject.Find("Img_Buy_" + Temp_Magic).GetComponent<CanvasGroup>().alpha = 0;
                        }
                    }

                    if (items_Place_online[i])
                    {
                        if (ctr_Data_Online != 1)
                        {
                            GameObject.Find("Img_Buy_" + Temp_Magic).GetComponent<CanvasGroup>().alpha = 1;
                            //GameObject.Find("Img_Buy_" + Temp_Magic).GetComponent<CanvasGroup>().alpha = 0;
                        }
                        else if (ctr_Data_Online == 1)
                        {
                            GameObject.Find("Img_Buy_" + Temp_Magic).GetComponent<CanvasGroup>().alpha = 0;
                        }
                        else
                        {
                            GameObject.Find("Img_Buy_" + Temp_Magic).GetComponent<CanvasGroup>().alpha = 1;
                        }
                    }

                    if (ctr_Data_Online == 1 && Destroy_Data_Online)
                    {
                        GameObject.Find("Img_Buy_1").GetComponent<CanvasGroup>().alpha = 0;
                        Destroy_Data = false;
                    }
                }
                else
                {
                    if (Temp_Magic >= 7)
                    {

                    }
                    else
                    {
                        GameObject.Find("Img_Buy_" + Temp_Magic).GetComponent<CanvasGroup>().alpha = 0;
                    }
                }
            }
        }
	}

    public IEnumerator Online_Data()
    {
        isRunning = true;

        WWWForm form_Username = new WWWForm();
        //form_Username.AddField("username", "darren");
        form_Username.AddField("username", GameMaster.player_Name);

        WWW itemsData_Online = new WWW("http://keefem.000webhostapp.com/Select_Json.php",form_Username);
        //WWW itemsData_Online = new WWW("http://fldstts.000webhostapp.com/Select_Json.php", form_Username);

        //WWWForm form = new WWWForm();
        //form.AddField("api", "darren_select");
        //WWW url = new WWW(url_select, form);

        yield return itemsData_Online;

        string itemsDataString = itemsData_Online.text;

        id = itemsDataString.Split(';');
        items = itemsDataString.Split(';');
        type = itemsDataString.Split(';');
        price = itemsDataString.Split(';');

        items_Place = new bool[items.Length];

        id_Data = new string[items.Length];
        items_Data = new string[items.Length];
        type_Data = new string[items.Length];
        price_Data = new string[items.Length];

        ctr_Data = 0;

        for (int i = 0; i < items.Length - 1; i++)
        {
            items_Place[i] = true;

            id_Data[i] = GetDataValue(items[i], "Id:");
            items_Data[i] = GetDataValue(items[i], "Name:");
            type_Data[i] = GetDataValue(items[i], "Type:");
            price_Data[i] = GetDataValue(items[i], "Price:");

            ctr_Data++;
        }

        isRunning = false;
    }

    string[] username_Shop;
    string[] username_Shop_Data;

    public IEnumerator Online_Data_Sell()
    {
        isRunning = true;

        WWWForm form = new WWWForm();
        //form.AddField("username", "darren");
        form.AddField("username", GameMaster.player_Name);

        WWW itemsData = new WWW("http://keefem.000webhostapp.com/Select_Json_Online.php",form);
        //WWW itemsData = new WWW("http://fldstts.000webhostapp.com/Select_Json_Online.php", form);

        //WWWForm form = new WWWForm();
        //form.AddField("api", "darren_select");
        //WWW url = new WWW(url_select, form);

        yield return itemsData;

        string itemsDataString = itemsData.text;

        id_online = itemsDataString.Split(';');
        username_Shop = itemsDataString.Split(';');
        items_online = itemsDataString.Split(';');
        type_online = itemsDataString.Split(';');
        price_online = itemsDataString.Split(';');

        items_Place_online = new bool[items_online.Length];

        id_Data_online = new string[items_online.Length];
        username_Shop_Data = new string[items_online.Length];
        items_Data_online = new string[items_online.Length];
        type_Data_online = new string[items_online.Length];
        price_Data_online = new string[items_online.Length];

        ctr_Data_Online = 0;

        for (int i = 0; i < items_online.Length - 1; i++)
        {
            items_Place_online[i] = true;

            id_Data_online[i] = GetDataValue(items_online[i], "Id:");
            username_Shop_Data[i] = GetDataValue(username_Shop[i], "Username:");
            items_Data_online[i] = GetDataValue(items_online[i], "Name:");
            type_Data_online[i] = GetDataValue(items_online[i], "Type:");
            price_Data_online[i] = GetDataValue(items_online[i], "Price:");

            ctr_Data_Online++;
            /*
			items_Place [i] = true;
			items_Data [i] = GetDataValue (items [i], "Name:");
			type_Data [i] = GetDataValue (type [i], "Type:");
			price_Data [i] = GetDataValue (price [i], "Price:");
			ctr_Data++;
            */
        }

        isRunning = false ;
    }

    public static string Place_Online = "";
    bool isRunning;

    private void Check_Online_Shop()
    {
        if (GameMaster.Online && Place_Online == "Online" && !isRunning)
        {
            Debug.Log("A");
            StartCoroutine(Online_Data());
        }
        if (GameMaster.Online && Place_Online == "Online_Sell" && !isRunning)
        {
            Debug.Log("B");
            StartCoroutine(Online_Data_Sell());
        }
    }
}
