using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

	public static GameMaster Instance;

    public static string player_Name;

	public static bool firstLoad;
	public static bool enemySpawn;

	public static bool city;
	public static bool field;
	public static bool house;
	public static bool boss;
    public static bool newGame;
    public static bool tutorial;

    public static bool fromBoss;
	public static bool fromHouse;

	public static bool loadingScreen;

	public static bool Shop_Open;

	public static int Player_Gold;

	public static int Questing_Global = 0;

	public static string Enemy_Damage;

	public static bool NPC;

	public static bool Online;
	public static int Online_Max = 0;

    public static bool Change_Day;

    public static string EndGame;

	public void Awake()
	{
		field = true;

		DontDestroyOnLoad (this.gameObject);


		if (FindObjectsOfType(GetType()).Length > 1) 
		{
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
        Check_Online_Shop();
	}

    public IEnumerator Online_Data()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", GameMaster.player_Name);
        //form.AddField("username", "darren");

        WWW itemsData = new WWW("http://keefem.000webhostapp.com/Select_Ctr_Max_Online.php",form);
        //WWW itemsData = new WWW("http://fldstts.000webhostapp.com/Select_Ctr_Max_Online.php", form);

        //WWWForm form = new WWWForm();
        //form.AddField("api", "darren_select");
        //WWW url = new WWW(url_select, form);

        yield return itemsData;

        string itemsDataString = itemsData.text;

        if (itemsDataString != "")
        {
            Online_Max = int.Parse(itemsDataString);
            Debug.Log(Online_Max);
        }
    }

    private void Check_Online_Shop()
    {
        if (GameMaster.Online && Online_Item_1.Place_Online == "Online")
        {
            StartCoroutine(Online_Data());
        }
    }
}
