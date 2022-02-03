using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Users : MonoBehaviour {

    GameObject input_Username;
    InputField txt_Username;

    GameObject input_Password;
    InputField txt_Password;

    GameObject input_Confirm;
    InputField txt_Confirm;

    [SerializeField]
    Text txt_Alert;

    bool first;
    bool active_InputField;

    // Use this for initialization
    void Start ()
    {
        first = true;

        input_Username = GameObject.Find("Username");
        input_Password = GameObject.Find("Password");
        input_Confirm = GameObject.Find("Confirm");
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (GameObject.Find("New_User").GetComponent<CanvasGroup>().alpha == 1)
        {
            txt_Username = input_Username.GetComponent<InputField>();
            txt_Password = input_Password.GetComponent<InputField>();
            txt_Confirm = input_Confirm.GetComponent<InputField>();

            txt_Alert = GameObject.Find("Txt_Alert").GetComponent<Text>();
            
        }
        else if(GameObject.Find("New_User").GetComponent<CanvasGroup>().alpha == 0)
        {
            input_Username.GetComponent<InputField>().DeactivateInputField();
            input_Password.GetComponent<InputField>().DeactivateInputField();
            input_Confirm.GetComponent<InputField>().DeactivateInputField();
        }

        if (login_Ok)
        {
            StartCoroutine(Login_Check());
        }
    }

    public void Cancel()
    {
        GameObject.Find("New_User").GetComponent<CanvasGroup>().alpha = 0;

        input_Username.GetComponent<InputField>().DeactivateInputField();
        input_Password.GetComponent<InputField>().DeactivateInputField();
        input_Confirm.GetComponent<InputField>().DeactivateInputField();
    }

    public void Canvas_Sign_Up()
    {
        GameObject.Find("New_User").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.Find("Btn_Login").GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("Btn_New_User").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.Find("Confirm").GetComponent<CanvasGroup>().alpha = 1;

        input_Username.GetComponent<InputField>().ActivateInputField();
        input_Password.GetComponent<InputField>().ActivateInputField();
        input_Confirm.GetComponent<InputField>().ActivateInputField();
    }

    public void Open_Notice()
    {
        GameObject.Find("Btn_Notice").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.Find("Canvas_Notice").GetComponent<Canvas>().sortingOrder = 2;

        GameObject.Find("New_User").GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("Btn_Login").GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("Btn_New_User").GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("Confirm").GetComponent<CanvasGroup>().alpha = 0;
    }

    public void Close_Notice()
    {
        GameObject.Find("Btn_Notice").GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("Canvas_Notice").GetComponent<Canvas>().sortingOrder = -1;
    }

    public void Canvas_Login()
    {
        GameObject.Find("New_User").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.Find("Confirm").GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("Btn_New_User").GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("Btn_Login").GetComponent<CanvasGroup>().alpha = 1;

        input_Username.GetComponent<InputField>().ActivateInputField();
        input_Password.GetComponent<InputField>().ActivateInputField();
        input_Confirm.GetComponent<InputField>().ActivateInputField();
    }

    bool login_Ok = false;

    public void Login()
    {
        if (GameObject.Find("New_User").GetComponent<CanvasGroup>().alpha == 1)
        {
            if (txt_Username.text != string.Empty && txt_Password.text != string.Empty)
            {
                StartCoroutine(First_Login_Check());
            }
        }
    }

    public void Sign_Up()
    {
        if (txt_Username.text != string.Empty && txt_Password.text != string.Empty && txt_Confirm.text != string.Empty)
        {
            if (txt_Password.text == txt_Confirm.text)
            {
                first = false;
                StartCoroutine(SignUp_Check());
            }
            else
            {
                GameObject.Find("Alert").GetComponent<CanvasGroup>().alpha = 1;
                txt_Alert.text = "Password Not Same Please Input Again";

                Invoke("Canvas_Alert", 2);

                txt_Username.text = "";
                txt_Password.text = "";
                txt_Confirm.text = "";
            }
        }
    }

    void Canvas_Alert()
    {
        GameObject.Find("Alert").GetComponent<CanvasGroup>().alpha = 0;
    }

    IEnumerator First_Login_Check()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", txt_Username.text);
        form.AddField("password", txt_Password.text);

        WWW itemsData = new WWW("http://keefem.000webhostapp.com/Checking_User_Login.php", form);
        //WWW itemsData = new WWW("http://fldstts.000webhostapp.com/Checking_User_Login.php", form);

        yield return itemsData;

        if (itemsData.text == "Ok")
        {
            login_Ok = true;
        }
        else
        {
            GameObject.Find("Alert").GetComponent<CanvasGroup>().alpha = 1;
            txt_Alert.text = "Username Or Password Wrong";

            Invoke("Canvas_Alert", 2);
        }
    }

    string[] farm_1;
    string[] farm_2;
    string[] farm_3;
    string[] farm_4;
    string[] farm_5;
    string[] farm_6;
    string[] farm_7;
    string[] farm_8;
    string[] farm_9;

    string[] farm_1_Type;
    string[] farm_2_Type;
    string[] farm_3_Type;
    string[] farm_4_Type;
    string[] farm_5_Type;
    string[] farm_6_Type;
    string[] farm_7_Type;
    string[] farm_8_Type;
    string[] farm_9_Type;

    string[] quest;
    string[] quest_Kill;

    string[] end;

    string[] lvl;
    string[] exp;
    string[] gold;

    string[] item;
    string[] equip_Durability;

    IEnumerator Login_Check()
    {
        WWWForm form = new WWWForm();
        form.AddField("darren_Api", "Darren");
        form.AddField("username", txt_Username.text);

        //Items
        //WWW itemsData = new WWW("http://keefem.000webhostapp.com/Load_Data.php", form);

        WWW itemsData = new WWW("http://keefem.000webhostapp.com/Checking_User_Inventory.php", form);
        //WWW itemsData = new WWW("http://fldstts.000webhostapp.com/Checking_User_Inventory.php", form);

        yield return itemsData;
        
        string itemsDataString = itemsData.text;

        Player.user = itemsDataString.Split(';');
        Player.items_user = itemsDataString.Split(';');
        Player.times = itemsDataString.Split(';');
        Player.durability = itemsDataString.Split(';');

        Player.user_Data = new string[Player.user.Length];
        Player.items_user_Data = new string[Player.user.Length];
        Player.times_Data = new int[Player.user.Length];
        Player.durability_Data = new int[Player.user.Length];

        //Debug.Log(itemsDataString);

        for (int i = 0; i < Player.user_Data.Length - 1; i++)
        {
            Player.user_Data[i] = GetDataValue(Player.user[i], "Username:");
            Player.items_user_Data[i] = GetDataValue(Player.items_user[i], "Name:");
            Player.times_Data[i] = int.Parse(GetDataValue(Player.times[i], "Times:"));
            Player.durability_Data[i] = int.Parse(GetDataValue(Player.durability[i], "Durability:"));
             
            /*
			items_Place [i] = true;
			items_Data [i] = GetDataValue (items [i], "Name:");
			type_Data [i] = GetDataValue (type [i], "Type:");
			price_Data [i] = GetDataValue (price [i], "Price:");
			ctr_Data++;
            */
        }

        System.Threading.Thread.Sleep(100);

        //Level
        //WWW lvlData = new WWW("http://keefem.000webhostapp.com/Load_Lvl.php", form);

        WWW lvlData = new WWW("http://keefem.000webhostapp.com/Checking_User_Level.php", form);
        //WWW lvlData = new WWW("http://fldstts.000webhostapp.com/Checking_User_Level.php", form);

        yield return lvlData;

        string lvlDataString = lvlData.text;

        Player.user = lvlDataString.Split(';');
        lvl = lvlDataString.Split(';');
        exp = lvlDataString.Split(';');
        gold = lvlDataString.Split(';');

        Player.lvl_Data = new int[Player.user.Length];
        Player.exp_Data = new int[Player.user.Length];
        Player.gold_Data = new int[Player.user.Length];

        for (int i = 0; i < lvl.Length - 1; i++)
        {
            Player.lvl_Data[i] = int.Parse(GetDataValue(lvl[i], "Lvl:"));
            Player.exp_Data[i] = int.Parse(GetDataValue(exp[i], "Exp:"));
            Player.gold_Data[i] = int.Parse(GetDataValue(gold[i], "Gold:"));
        }

        System.Threading.Thread.Sleep(100);

        //Equipment
        //WWW equipmentData = new WWW("http://keefem.000webhostapp.com/Load_Player_Panel.php", form);

        WWW equipmentData = new WWW("http://keefem.000webhostapp.com/Checking_User_Equipment.php", form);
        //WWW equipmentData = new WWW("http://fldstts.000webhostapp.com/Checking_User_Equipment.php", form);

        yield return equipmentData;

        string equipmentDataString = equipmentData.text;

        Player.user = equipmentDataString.Split(';');
        item = equipmentDataString.Split(';');
        equip_Durability = equipmentDataString.Split(';');

        Player.item_Data = new string[item.Length];
        Player.equipment_Durability_Data = new int[item.Length];

        for (int i = 0; i < item.Length - 1; i++)
        {
            Player.item_Data[i] = GetDataValue(item[i], "Name:");
            Player.equipment_Durability_Data[i] = int.Parse(GetDataValue(equip_Durability[i], "Durability:"));
        }

        //Farming
        WWW farmingData = new WWW("http://keefem.000webhostapp.com/Load_Farm.php", form);
        //WWW farmingData = new WWW("http://fldstts.000webhostapp.com/Load_Farm.php", form);

        yield return farmingData;

        string farmingDataString = farmingData.text;

        Player.user = farmingDataString.Split(';');
        farm_1 = farmingDataString.Split(';');
        farm_1_Type = farmingDataString.Split(';');
        farm_2 = farmingDataString.Split(';');
        farm_2_Type = farmingDataString.Split(';');
        farm_3 = farmingDataString.Split(';');
        farm_3_Type = farmingDataString.Split(';');
        farm_4 = farmingDataString.Split(';');
        farm_4_Type = farmingDataString.Split(';');
        farm_5 = farmingDataString.Split(';');
        farm_5_Type = farmingDataString.Split(';');
        farm_6 = farmingDataString.Split(';');
        farm_6_Type = farmingDataString.Split(';');
        farm_7 = farmingDataString.Split(';');
        farm_7_Type = farmingDataString.Split(';');
        farm_8 = farmingDataString.Split(';');
        farm_8_Type = farmingDataString.Split(';');
        farm_9 = farmingDataString.Split(';');
        farm_9_Type = farmingDataString.Split(';');

        Player.user_Data = new string[farm_1.Length];

        Player.farm1_Data = new int[farm_1.Length];
        Player.farm2_Data = new int[farm_1.Length];
        Player.farm3_Data = new int[farm_1.Length];
        Player.farm4_Data = new int[farm_1.Length];
        Player.farm5_Data = new int[farm_1.Length];
        Player.farm6_Data = new int[farm_1.Length];
        Player.farm7_Data = new int[farm_1.Length];
        Player.farm8_Data = new int[farm_1.Length];
        Player.farm9_Data = new int[farm_1.Length];

        Player.farm1_Type = new string[farm_1.Length];
        Player.farm2_Type = new string[farm_1.Length];
        Player.farm3_Type = new string[farm_1.Length];
        Player.farm4_Type = new string[farm_1.Length];
        Player.farm5_Type = new string[farm_1.Length];
        Player.farm6_Type = new string[farm_1.Length];
        Player.farm7_Type = new string[farm_1.Length];
        Player.farm8_Type = new string[farm_1.Length];
        Player.farm9_Type = new string[farm_1.Length];

        for (int i = 0; i < farm_1.Length - 1; i++)
        {
            Player.user_Data[i] = GetDataValue(farm_1[i], "Username:");

            Player.farm1_Data[i] = int.Parse(GetDataValue(farm_1[i], "Farm1:"));
            Player.farm2_Data[i] = int.Parse(GetDataValue(farm_2[i], "Farm2:"));
            Player.farm3_Data[i] = int.Parse(GetDataValue(farm_3[i], "Farm3:"));
            Player.farm4_Data[i] = int.Parse(GetDataValue(farm_4[i], "Farm4:"));
            Player.farm5_Data[i] = int.Parse(GetDataValue(farm_5[i], "Farm5:"));
            Player.farm6_Data[i] = int.Parse(GetDataValue(farm_6[i], "Farm6:"));
            Player.farm7_Data[i] = int.Parse(GetDataValue(farm_7[i], "Farm7:"));
            Player.farm8_Data[i] = int.Parse(GetDataValue(farm_8[i], "Farm8:"));
            Player.farm9_Data[i] = int.Parse(GetDataValue(farm_9[i], "Farm9:"));

            Player.farm1_Type[i] = GetDataValue(farm_1_Type[i], "Farm1_Type:");
            Player.farm2_Type[i] = GetDataValue(farm_1_Type[i], "Farm2_Type:");
            Player.farm3_Type[i] = GetDataValue(farm_1_Type[i], "Farm3_Type:");
            Player.farm4_Type[i] = GetDataValue(farm_1_Type[i], "Farm4_Type:");
            Player.farm5_Type[i] = GetDataValue(farm_1_Type[i], "Farm5_Type:");
            Player.farm6_Type[i] = GetDataValue(farm_1_Type[i], "Farm6_Type:");
            Player.farm7_Type[i] = GetDataValue(farm_1_Type[i], "Farm7_Type:");
            Player.farm8_Type[i] = GetDataValue(farm_1_Type[i], "Farm8_Type:");
            Player.farm9_Type[i] = GetDataValue(farm_1_Type[i], "Farm9_Type:");
        }

        System.Threading.Thread.Sleep(100);

        //Questing
        //WWW questData = new WWW("http://keefem.000webhostapp.com/Load_Quest.php", form);

        WWW questData = new WWW("http://keefem.000webhostapp.com/Checking_User_Quest.php", form);
        //WWW questData = new WWW("http://fldstts.000webhostapp.com/Checking_User_Quest.php", form);

        yield return questData;

        string questDataString = questData.text;

        Debug.Log(questDataString);
        
        Player.user = questDataString.Split(';');
        quest = questDataString.Split(';');
        quest_Kill = questDataString.Split(';');

        Player.quest_Data = new int[quest.Length];
        Player.quest_Kill_Data = new int[quest.Length];

        for (int i = 0; i < Player.quest_Data.Length - 1; i++)
        {
            Player.user_Data[i] = GetDataValue(Player.user[i], "Username:");
            Player.quest_Data[i] = int.Parse(GetDataValue(quest[i], "Quest:"));
            Player.quest_Kill_Data[i] = int.Parse(GetDataValue(quest_Kill[i], "QuestKilled:"));
        }

        WWW endData = new WWW("http://keefem.000webhostapp.com/Checking_User_End.php", form);
        //WWW endData = new WWW("http://fldstts.000webhostapp.com/Checking_User_End.php", form);

        yield return endData;

        string endDataString = endData.text;

        Debug.Log(endDataString);

        Player.user = endDataString.Split(';');
        end = endDataString.Split(';');

        Player.end_Data = new string[end.Length];

        for (int i = 0; i < Player.end_Data.Length - 1; i++)
        {
            Player.user_Data[i] = GetDataValue(Player.user[i], "Username:");
            Player.end_Data[i] = GetDataValue(end[i], "End:");
        }

        System.Threading.Thread.Sleep(100);

        Player.loading_Data = true;

        GameMaster.player_Name = txt_Username.text;

        SceneManager.LoadScene("Loading_Screen");
        GameMaster.field = true;
        Player.loading_Data = true;
        login_Ok = false;
    }

    string GetDataValue(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains("|"))
        {
            value = value.Remove(value.IndexOf("|"));
        }
        return value;
    }

    IEnumerator SignUp_Check()
    {
        if (!first)
        {
            WWWForm form = new WWWForm();
            form.AddField("username", txt_Username.text);
            form.AddField("password", txt_Password.text);

            //WWW itemsData = new WWW("http://keefem.000webhostapp.com/New_Game_Users.php", form);

            WWW itemsData = new WWW("http://keefem.000webhostapp.com/Saving_New_Player.php", form);
            //WWW itemsData = new WWW("http://fldstts.000webhostapp.com/Saving_New_Player.php", form);

            yield return itemsData;

            if (itemsData.text == "Sucsess")
            {
                GameMaster.player_Name = txt_Username.text;

                //Save Data
                Save_Player_Data_First();

                SceneManager.LoadScene("Loading_Screen");
                GameMaster.tutorial = true;
            }
            else
            {
                GameObject.Find("Alert").GetComponent<CanvasGroup>().alpha = 1;
                txt_Alert.text = "Username Already Used";

                Invoke("Canvas_Alert", 2);
            }

            first = true;
        }
    }

    string url_Farm = "http://keefem.000webhostapp.com/Users_Farm.php";
    //string url_Farm = "http://fldstts.000webhostapp.com/Users_Farm.php";

    void Save_Player_Data_First()
    {
        WWWForm form_Save = new WWWForm();
        form_Save.AddField("username", txt_Username.text);

        //Level Exp Gold
        form_Save.AddField("lvl", 1);
        form_Save.AddField("exp", 0);
        form_Save.AddField("gold", 1000);

        //Inventory
        form_Save.AddField("items", "None;None;None;None;None;None;None;None;None;None;None;None;");
        form_Save.AddField("items_Times", "0;0;0;0;0;0;0;0;0;0;0;0;");
        form_Save.AddField("items_Durability", "0;0;0;0;0;0;0;0;0;0;0;0;");
        
        //Equipment
        form_Save.AddField("equipment", "None;None;None;");
        form_Save.AddField("equipment_Durability", "0;0;0;");

        //Quests
        form_Save.AddField("quest", 0);
        form_Save.AddField("questKilled", 0);

        //Save
        WWW url_Save_Data = new WWW("http://keefem.000webhostapp.com/Saving_Player_Data.php", form_Save);
        //WWW url_Save_Data = new WWW("http://fldstts.000webhostapp.com/Saving_Player_Data.php", form_Save);
        System.Threading.Thread.Sleep(100);
        Debug.Log("Saving");

        //Farm
        WWWForm form_Save_Farming = new WWWForm();
        form_Save_Farming.AddField("username", GameMaster.player_Name);
        form_Save_Farming.AddField("farm1", 0);
        form_Save_Farming.AddField("farm1_type", "None");
        form_Save_Farming.AddField("farm2", 0);
        form_Save_Farming.AddField("farm2_type", "None");
        form_Save_Farming.AddField("farm3", 0);
        form_Save_Farming.AddField("farm3_type", "None");
        form_Save_Farming.AddField("farm4", 0);
        form_Save_Farming.AddField("farm4_type", "None");
        form_Save_Farming.AddField("farm5", 0);
        form_Save_Farming.AddField("farm5_type", "None");
        form_Save_Farming.AddField("farm6", 0);
        form_Save_Farming.AddField("farm6_type", "None");
        form_Save_Farming.AddField("farm7", 0);
        form_Save_Farming.AddField("farm7_type", "None");
        form_Save_Farming.AddField("farm8", 0);
        form_Save_Farming.AddField("farm8_type", "None");
        form_Save_Farming.AddField("farm9", 0);
        form_Save_Farming.AddField("farm9_type", "None");

        WWW url_Save_Farm_WWW = new WWW(url_Farm, form_Save_Farming);

        WWWForm form_ending = new WWWForm();
        form_ending.AddField("username", txt_Username.text);
        form_ending.AddField("end", "no");

        WWW url_Save_End = new WWW("http://keefem.000webhostapp.com/End_User.php", form_ending);
        //WWW url_Save_End = new WWW("http://fldstts.000webhostapp.com/End_User.php", form_ending);

        System.Threading.Thread.Sleep(100);
    }
}
