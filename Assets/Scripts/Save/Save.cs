using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour {

    public Slots[] equipmentSlots;
    public Slots[] secondarySlots;
    public Slots[] playerSlots;

    private Inventory chest;

    public static bool save_Anything_Player;
    public static bool save_Anything_Farm;

    // Use this for initialization
    void Start ()
    {
        equipmentSlots = GameObject.Find("Inventory").GetComponentsInChildren<Slots>();
        secondarySlots = GameObject.Find("Chest_Inventory").GetComponentsInChildren<Slots>();
        playerSlots = GameObject.Find("Player_Panel").GetComponentsInChildren<Slots>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (save_Anything_Player)
        {
            Save_Player();
        }
        if (save_Anything_Farm)
        {
            Save_Farm();
            Save_Player();
            save_Anything_Farm = false;
        }
    }

    public void Save_Farm()
    {
        username = GameMaster.player_Name;
        // username = "DarrenK";
        overwrite_Save = true;

        if (overwrite_Save)
        {
            WWWForm form_Farm_Delete = new WWWForm();
            form_Farm_Delete.AddField("username", username);
            WWW Farm = new WWW(url_Delete_Farm, form_Farm_Delete);

            System.Threading.Thread.Sleep(50);

            Debug.Log("DeleteSave");

            overwrite_Save = false;
        }

        //Farming
        if (string.IsNullOrEmpty(Farming.plant_Type))
        {
            Farming.plant_Type = "None";
        }
        if (string.IsNullOrEmpty(Farming_2.plant_Type))
        {
            Farming_2.plant_Type = "None";
        }
        if (string.IsNullOrEmpty(Farming_3.plant_Type))
        {
            Farming_3.plant_Type = "None";
        }
        if (string.IsNullOrEmpty(Farming_4.plant_Type))
        {
            Farming_4.plant_Type = "None";
        }
        if (string.IsNullOrEmpty(Farming_5.plant_Type))
        {
            Farming_5.plant_Type = "None";
        }
        if (string.IsNullOrEmpty(Farming_6.plant_Type))
        {
            Farming_6.plant_Type = "None";
        }
        if (string.IsNullOrEmpty(Farming_7.plant_Type))
        {
            Farming_7.plant_Type = "None";
        }
        if (string.IsNullOrEmpty(Farming_8.plant_Type))
        {
            Farming_8.plant_Type = "None";
        }
        if (string.IsNullOrEmpty(Farming_9.plant_Type))
        {
            Farming_9.plant_Type = "None";
        }

        WWWForm form_Save_Farming = new WWWForm();
        form_Save_Farming.AddField("username", username);
        form_Save_Farming.AddField("farm1", Farming.dayCounter);
        form_Save_Farming.AddField("farm1_type", Farming.plant_Type);
        form_Save_Farming.AddField("farm2", Farming_2.dayCounter);
        form_Save_Farming.AddField("farm2_type", Farming_2.plant_Type);
        form_Save_Farming.AddField("farm3", Farming_3.dayCounter);
        form_Save_Farming.AddField("farm3_type", Farming_3.plant_Type);
        form_Save_Farming.AddField("farm4", Farming_4.dayCounter);
        form_Save_Farming.AddField("farm4_type", Farming_4.plant_Type);
        form_Save_Farming.AddField("farm5", Farming_5.dayCounter);
        form_Save_Farming.AddField("farm5_type", Farming_5.plant_Type);
        form_Save_Farming.AddField("farm6", Farming_6.dayCounter);
        form_Save_Farming.AddField("farm6_type", Farming_6.plant_Type);
        form_Save_Farming.AddField("farm7", Farming_7.dayCounter);
        form_Save_Farming.AddField("farm7_type", Farming_7.plant_Type);
        form_Save_Farming.AddField("farm8", Farming_8.dayCounter);
        form_Save_Farming.AddField("farm8_type", Farming_8.plant_Type);
        form_Save_Farming.AddField("farm9", Farming_9.dayCounter);
        form_Save_Farming.AddField("farm9_type", Farming_9.plant_Type);

        WWW url_Save_Farm_WWW = new WWW(url_Farm, form_Save_Farming);
        System.Threading.Thread.Sleep(50);
    }

    public void Save_Player()
    {
        username = GameMaster.player_Name;
        // username = "DarrenK";
        overwrite_Save = true;

        if (overwrite_Save)
        {
            WWWForm form_Save_Delete = new WWWForm();
            form_Save_Delete.AddField("username", username);
            WWW url = new WWW(url_Overwrite, form_Save_Delete);

            System.Threading.Thread.Sleep(50);

            Debug.Log("DeleteSave");

            overwrite_Save = false;
        }

        WWWForm form_Save = new WWWForm();
        form_Save.AddField("username", username);

        Debug.Log(Player.Temp_Exp.ToString());

        //Level Exp Gold
        form_Save.AddField("lvl", Player.lvl);
        form_Save.AddField("exp", Player.Temp_Exp.ToString());
        form_Save.AddField("gold", GameMaster.Player_Gold.ToString());

        //Inventory
        foreach (Slots slot in equipmentSlots)
        {
            if (!slot.isEmpty)
            {
                item_name = slot.CurrentItem.Item.ItemName;
                times = slot.isCount;
                durability = slot.CurrentItem.Item.Durability;

                temp_Inventory = temp_Inventory + item_name + ";";
                temp_Inventory_Times = temp_Inventory_Times + times.ToString() + ";";
                temp_Durability = temp_Durability + durability.ToString() + ";";

                form_Save.AddField("items", temp_Inventory);
                form_Save.AddField("items_Times", temp_Inventory_Times);
                form_Save.AddField("items_Durability", temp_Durability);
            }
            else
            {
                item_name = "None";
                times = 0;
                durability = 0;

                temp_Inventory = temp_Inventory + item_name + ";";
                temp_Inventory_Times = temp_Inventory_Times + times.ToString() + ";";
                temp_Durability = temp_Durability + durability.ToString() + ";";

                form_Save.AddField("items", temp_Inventory);
                form_Save.AddField("items_Times", temp_Inventory_Times);
                form_Save.AddField("items_Durability", temp_Durability);
            }
        }

        //Equipment
        foreach (Slots slot in playerSlots)
        {
            if (!slot.isEmpty)
            {
                item_name_secondary = slot.CurrentItem.Item.ItemName;
                equipment_Durability = slot.CurrentItem.Item.Durability;

                temp_Equipment = temp_Equipment + item_name_secondary + ";";
                temp_equipment_Durability = temp_equipment_Durability + equipment_Durability.ToString() + ";";

                form_Save.AddField("equipment", temp_Equipment);
                form_Save.AddField("equipment_Durability", temp_equipment_Durability);
            }
            else
            {
                item_name_secondary = "None";
                equipment_Durability = 0;

                temp_Equipment = temp_Equipment + item_name_secondary + ";";
                temp_equipment_Durability = temp_equipment_Durability + equipment_Durability.ToString() + ";";

                form_Save.AddField("equipment", temp_Equipment);
                form_Save.AddField("equipment_Durability", temp_equipment_Durability);
            }
        }

        //Quests
        form_Save.AddField("quest", GameMaster.Questing_Global);
        form_Save.AddField("questKilled", Quest_Object.enemyCountKill);

        //Save
        WWW url_Save_Data = new WWW("http://keefem.000webhostapp.com/Saving_Player_Data.php", form_Save);
        //WWW url_Save_Data = new WWW("http://fldstts.000webhostapp.com/Saving_Player_Data.php", form_Save);
        System.Threading.Thread.Sleep(100);
        Debug.Log("Saving");

        temp_Inventory = "";
        temp_Inventory_Times = "";
        temp_Durability = "";
        temp_Equipment = "";
        temp_equipment_Durability = "";

        save_Anything_Player = false;
    }

    public void No_Save()
    {
        GameObject.Find("Yes_No_Save").GetComponent<CanvasGroup>().alpha = 0;
    }

    string username;

    string item_name;
    int times;

    string item_name_secondary;
    int times_secondary;

    /*
    string url_Delete_Save = "http://keefem.000webhostapp.com/Delete_Save_Data.php";
    string url_Save = "http://keefem.000webhostapp.com/Save_Data.php";
    */

    string url_Delete_Farm = "http://keefem.000webhostapp.com/Delete_Farm_Data.php";
    string url_Farm = "http://keefem.000webhostapp.com/Users_Farm.php";

    //string url_Delete_Farm = "http://fldstts.000webhostapp.com/Delete_Farm_Data.php";
    //string url_Farm = "http://fldstts.000webhostapp.com/Users_Farm.php";

    /*
    string url_Delete_Secondary = "http://keefem.000webhostapp.com/Delete_Inventory_Secondary_Data.php";
    string url_Inventory = "http://keefem.000webhostapp.com/Save_Inventory_Secondary.php";

    string url_Delete_Quest = "http://keefem.000webhostapp.com/Delete_Quest_Data.php";
    string url_Quest = "http://keefem.000webhostapp.com/Save_Quest.php";

    string url_Delete_Panel = "http://keefem.000webhostapp.com/Delete_Player_Panel.php";
    string url_Panel = "http://keefem.000webhostapp.com/Save_Player_Panel.php";

    string url_Delete_Level = "http://keefem.000webhostapp.com/Delete_Lvl.php";
    string url_Level = "http://keefem.000webhostapp.com/Save_Lvl.php";
    */

    string url_Overwrite = "http://keefem.000webhostapp.com/Overwrite_Player_Data.php";
    string url_Save_Data = "http://keefem.000webhostapp.com/Saving_Player_Data.php";

    //string url_Overwrite = "http://fldstts.000webhostapp.com/Overwrite_Player_Data.php";
    //string url_Save_Data = "http://fldstts.000webhostapp.com/Saving_Player_Data.php";

    bool overwrite_Save;

    string temp_Inventory;
    string temp_Inventory_Times;
    string temp_Equipment;

    int durability;
    string temp_Durability;

    int equipment_Durability;
    string temp_equipment_Durability;

    public void Yes_Save()
    {
        username = GameMaster.player_Name;
        // username = "DarrenK";
        overwrite_Save = true;

        if (overwrite_Save)
        {
            /*
            WWWForm form_Save_Delete = new WWWForm();
            form_Save_Delete.AddField("username", username);
            WWW url = new WWW(url_Delete_Save, form_Save_Delete);

            System.Threading.Thread.Sleep(100);

            WWWForm form_Secondary_Delete = new WWWForm();
            form_Secondary_Delete.AddField("username", username);
            WWW Secondary = new WWW(url_Delete_Secondary, form_Secondary_Delete);

            System.Threading.Thread.Sleep(100);

            WWWForm form_Quest_Delete = new WWWForm();
            form_Quest_Delete.AddField("username", username);
            WWW Quest = new WWW(url_Delete_Quest, form_Quest_Delete);

            System.Threading.Thread.Sleep(100);

            WWWForm form_Panel_Delete = new WWWForm();
            form_Panel_Delete.AddField("username", username);
            WWW Panel = new WWW(url_Delete_Panel, form_Panel_Delete);

            System.Threading.Thread.Sleep(100);

            WWWForm form_Level_Delete = new WWWForm();
            form_Level_Delete.AddField("username", username);
            WWW Level = new WWW(url_Delete_Level, form_Level_Delete);

            System.Threading.Thread.Sleep(100);
            */

            WWWForm form_Farm_Delete = new WWWForm();
            form_Farm_Delete.AddField("username", username);
            WWW Farm = new WWW(url_Delete_Farm, form_Farm_Delete);

            System.Threading.Thread.Sleep(50);

            WWWForm form_Save_Delete = new WWWForm();
            form_Save_Delete.AddField("username", username);
            WWW url = new WWW(url_Overwrite, form_Save_Delete);

            System.Threading.Thread.Sleep(100);

            Debug.Log("DeleteSave");

            overwrite_Save = false;
        }

        WWWForm form_Save = new WWWForm();
        form_Save.AddField("username", username);

        //Level Exp Gold
        form_Save.AddField("lvl", Player.lvl);
        form_Save.AddField("exp", Player.Temp_Exp.ToString());
        form_Save.AddField("gold", GameMaster.Player_Gold.ToString());

        //Inventory
        foreach (Slots slot in equipmentSlots)
        {
            if (!slot.isEmpty)
            {
                item_name = slot.CurrentItem.Item.ItemName;
                times = slot.isCount;
                durability = slot.CurrentItem.Item.Durability;

                temp_Inventory = temp_Inventory + item_name + ";";
                temp_Inventory_Times = temp_Inventory_Times + times.ToString() + ";";
                temp_Durability = temp_Durability + durability.ToString() + ";";
                
                form_Save.AddField("items", temp_Inventory);
                form_Save.AddField("items_Times", temp_Inventory_Times);
                form_Save.AddField("items_Durability", temp_Durability);
            }
            else
            {
                item_name = "None";
                times = 0;
                durability = 0;

                temp_Inventory = temp_Inventory + item_name + ";";
                temp_Inventory_Times = temp_Inventory_Times + times.ToString() + ";";
                temp_Durability = temp_Durability + durability.ToString() + ";";

                form_Save.AddField("items", temp_Inventory);
                form_Save.AddField("items_Times", temp_Inventory_Times);
                form_Save.AddField("items_Durability", temp_Durability);
            }
        }

        //Equipment
        foreach (Slots slot in playerSlots)
        {
            if (!slot.isEmpty)
            {
                item_name_secondary = slot.CurrentItem.Item.ItemName;
                equipment_Durability = slot.CurrentItem.Item.Durability;

                temp_Equipment = temp_Equipment + item_name_secondary + ";";
                temp_equipment_Durability = temp_equipment_Durability + equipment_Durability.ToString() + ";";

                form_Save.AddField("equipment", temp_Equipment);
                form_Save.AddField("equipment_Durability", temp_equipment_Durability);
            }
            else
            {
                item_name_secondary = "None";
                equipment_Durability = 0;

                temp_Equipment = temp_Equipment + item_name_secondary + ";";
                temp_equipment_Durability = temp_equipment_Durability + equipment_Durability.ToString() + ";";

                form_Save.AddField("equipment", temp_Equipment);
                form_Save.AddField("equipment_Durability", temp_equipment_Durability);
            }
        }

        //Quests
        form_Save.AddField("quest", GameMaster.Questing_Global);
        form_Save.AddField("questKilled", Quest_Object.enemyCountKill);

        //Save
        //WWW url_Save_Data = new WWW("http://keefem.000webhostapp.com/Saving_Player_Data.php", form_Save);
        WWW url_Save_Data = new WWW("http://fldstts.000webhostapp.com/Saving_Player_Data.php", form_Save);
        System.Threading.Thread.Sleep(50);
        Debug.Log("Saving");

        temp_Inventory = "";
        temp_Inventory_Times = "";
        temp_Durability = "";
        temp_Equipment = "";
        temp_equipment_Durability = "";

        /*
        //Level
        WWWForm form_Save_Level = new WWWForm();
        form_Save_Level.AddField("username", username);
        form_Save_Level.AddField("lvl", Player.lvl);
        form_Save_Level.AddField("exp", Player.Temp_Exp.ToString());
        form_Save_Level.AddField("gold", GameMaster.Player_Gold.ToString());

        WWW url_Save_Level_WWW = new WWW(url_Level, form_Save_Level);
        System.Threading.Thread.Sleep(100);

        //Equipment
        foreach (Slots slot in playerSlots)
        {
            if (!slot.isEmpty)
            {
                WWWForm form_Save_Panel = new WWWForm();
                form_Save_Panel.AddField("username", username);
                form_Save_Panel.AddField("item", slot.CurrentItem.Item.ItemName);

                WWW url_Save_Panel_WWW = new WWW(url_Panel, form_Save_Panel);
                System.Threading.Thread.Sleep(100);
            }
        }

        //Quest
        WWWForm form_Save_Quest = new WWWForm();
        form_Save_Quest.AddField("username", username);
        form_Save_Quest.AddField("quest", GameMaster.Questing_Global);
        form_Save_Quest.AddField("questKilled", Quest_Object.enemyCountKill);

        WWW url_Save_Quest_WWW = new WWW(url_Quest, form_Save_Quest);
        System.Threading.Thread.Sleep(100);
        */

        //Farming
        if (string.IsNullOrEmpty(Farming.plant_Type))
        {
            Farming.plant_Type = "None";
        }
        if (string.IsNullOrEmpty(Farming_2.plant_Type))
        {
            Farming_2.plant_Type = "None";
        }
        if (string.IsNullOrEmpty(Farming_3.plant_Type))
        {
            Farming_3.plant_Type = "None";
        }
        if (string.IsNullOrEmpty(Farming_4.plant_Type))
        {
            Farming_4.plant_Type = "None";
        }
        if (string.IsNullOrEmpty(Farming_5.plant_Type))
        {
            Farming_5.plant_Type = "None";
        }
        if (string.IsNullOrEmpty(Farming_6.plant_Type))
        {
            Farming_6.plant_Type = "None";
        }
        if (string.IsNullOrEmpty(Farming_7.plant_Type))
        {
            Farming_7.plant_Type = "None";
        }
        if (string.IsNullOrEmpty(Farming_8.plant_Type))
        {
            Farming_8.plant_Type = "None";
        }
        if (string.IsNullOrEmpty(Farming_9.plant_Type))
        {
            Farming_9.plant_Type = "None";
        }
        
        WWWForm form_Save_Farming = new WWWForm();
        form_Save_Farming.AddField("username", username);
        form_Save_Farming.AddField("farm1", Farming.dayCounter);
        form_Save_Farming.AddField("farm1_type", Farming.plant_Type);
        form_Save_Farming.AddField("farm2", Farming_2.dayCounter);
        form_Save_Farming.AddField("farm2_type", Farming_2.plant_Type);
        form_Save_Farming.AddField("farm3", Farming_3.dayCounter);
        form_Save_Farming.AddField("farm3_type", Farming_3.plant_Type);
        form_Save_Farming.AddField("farm4", Farming_4.dayCounter);
        form_Save_Farming.AddField("farm4_type", Farming_4.plant_Type);
        form_Save_Farming.AddField("farm5", Farming_5.dayCounter);
        form_Save_Farming.AddField("farm5_type", Farming_5.plant_Type);
        form_Save_Farming.AddField("farm6", Farming_6.dayCounter);
        form_Save_Farming.AddField("farm6_type", Farming_6.plant_Type);
        form_Save_Farming.AddField("farm7", Farming_7.dayCounter);
        form_Save_Farming.AddField("farm7_type", Farming_7.plant_Type);
        form_Save_Farming.AddField("farm8", Farming_8.dayCounter);
        form_Save_Farming.AddField("farm8_type", Farming_8.plant_Type);
        form_Save_Farming.AddField("farm9", Farming_9.dayCounter);
        form_Save_Farming.AddField("farm9_type", Farming_9.plant_Type);

        WWW url_Save_Farm_WWW = new WWW(url_Farm, form_Save_Farming);
        System.Threading.Thread.Sleep(50);

        /*
        //Equipment
        foreach (Slots slot in equipmentSlots)
        {
            if (!slot.isEmpty)
            {
                item_name = slot.CurrentItem.Item.ItemName;
                times = slot.isCount;

                WWWForm form_Save = new WWWForm();
                form_Save.AddField("username", username);
                form_Save.AddField("item_Name", item_name);
                form_Save.AddField("item_Times", times);

                WWW url_Save_WWW = new WWW(url_Save, form_Save);
                System.Threading.Thread.Sleep(100);
            }
            else
            {
                item_name = "None";
                times = 0;

                WWWForm form_Save = new WWWForm();
                form_Save.AddField("username", username);
                form_Save.AddField("item_Name", item_name);
                form_Save.AddField("item_Times", times);

                WWW url_Save_WWW = new WWW(url_Save, form_Save);
                System.Threading.Thread.Sleep(100);
            }
        }
        */
       
        /*
        //Secondary
        foreach (Slots slot in secondarySlots)
        {
            if (!slot.isEmpty)
            {
                item_name_secondary = slot.CurrentItem.Item.ItemName;
                times_secondary = slot.isCount;

                WWWForm form_Save_Secondary = new WWWForm();
                form_Save_Secondary.AddField("username", username);
                form_Save_Secondary.AddField("item_Name", item_name_secondary);
                form_Save_Secondary.AddField("item_Times", times_secondary);

                WWW url_Save_WWW = new WWW(url_Inventory, form_Save_Secondary);
                System.Threading.Thread.Sleep(100);
            }
        }    
        */
    }
}
