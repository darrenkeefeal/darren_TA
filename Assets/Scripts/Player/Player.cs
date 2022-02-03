using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Player : Characters {

	private static Player instance;
	public static Player Instance
	{
		get
		{ 
			if (instance == null) 
			{
				instance = GameObject.FindObjectOfType<Player> ();
			}
			return Player.instance;
		}
	}

	public static int lvl;
	public static bool lvlUp;

    public static float Temp_Exp;

	[SerializeField]
	private Stats hp;
	private float initHp = 100;

	[SerializeField]
	private EXP_Stats exp;
	private float maxExp = 100;
	private float newExp = 0;

	public static float EXP_Plus = 0;
	public static float HP_Minus = 0;
	public static float HP_Plus = 0;

	public static float Player_Hp;
	public static float Player_Max_Hp;
	public static float Player_Str;
	public static float Player_Def;

	public static Vector2 Player_Pos;

	public static GameObject Attacking_Pos;

	public static bool Attacking = false;
	public static bool Hoeing = false;

	public Inventory inventory;

	public Inventory player_Panel;

	private Inventory chest;

	private Inventory craft;

	private Inventory vendor;

	public static bool open_Chest;

	public bool stats;

	public Text txt_Status;

	public int baseStr;
	public int baseDef;

	private int str;
	public int Str
	{
		get { return str; }
		set { str = value; }
	}

	private int def;
	public int Def
	{
		get { return def; }
		set { def = value; }
	}

	public int Tstr;
	public int Tdef;

	public bool OkAttack;

	private GameObject Shop;

	public GameObject objectnya_Txt;
	Transform txt_Damage_Pos;
	public static string Damage_In;

	public static bool player_Dead;
	public static bool player_Continue;

	public static bool upgrade_weapon;

    public static bool audio_Sword;

    GameObject tmp_inventory;

    Scene City_Scene;

    private Quest_Manager quest_Manager;

    public void Awake()
	{
		DontDestroyOnLoad (this.gameObject);

		if (FindObjectsOfType(GetType()).Length > 1) 
		{
			Destroy (gameObject);
		}
	}

    public Slots[] equipmentSlots;

    protected override void Start ()
	{
        quest_Manager = FindObjectOfType<Quest_Manager>();

        equipmentSlots = GameObject.Find("Inventory").GetComponentsInChildren<Slots>();

        tmp_inventory = GameObject.Find("Inventory");

        hp.Initialize (initHp, initHp);
		exp.Initialize (newExp, maxExp);

		Player_Max_Hp = hp.MaxHp;

		lvl = 1;

		GameMaster.Player_Gold = 1000;

		SetStats (0, 0);

		lvlUp = false;

		Attacking_Pos = GameObject.Find ("Attack_Pos");

		open_Chest = false;

		OkAttack = true;

		txt_Damage_Pos = GameObject.Find ("Txt_Damage_Pos").GetComponent<Transform> ();

		base.Start ();
	}

	// Update is called once per frame
	//Base.Update Digunakan untuk mengoveride Update di Character (Start Anak Harus Dihilangkan)
	protected override void Update () {
    
        Temp_Exp = exp.MycurrentValue_EXP;

		GetInput ();

		Player_Hp = hp.MycurrentValue;

		Player_Pos = new Vector2 (transform.position.x, transform.position.y);

		Check_Game_Over ();

        Check_Continue();

		Check_Plus_Minus ();
        
        Check_Lvl_Up();

        if (GameMaster.Online)
        {
            Check_Online();
        }

        /*
		if (Attack_Pos.female_npc) 
		{
			SetStats (0, 0);
		}
		*/
        
        Check_Gold();

        Check_Online_Shop();

		base.Update ();
	}

    public static bool Temp_Online_Notice = true;

    private void Check_Online_Shop()
    {
        if (Temp_Online_Notice)
        {
            StartCoroutine(Gold_Online_Shop());
            Temp_Online_Notice = false;
            //Save.save_Anything_Player = true;
        }
    }

    string[] Gold;
    int[] Gold_Data;

    string url_Delete_Gold = "http://keefem.000webhostapp.com/Delete_Online_Gold.php";
    //string url_Delete_Gold = "http://fldstts.000webhostapp.com/Delete_Online_Gold.php";

    [SerializeField]
    Text txt_Notice_Gold_Player;

    int temp_Online_Gold;

    IEnumerator Gold_Online_Shop()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", GameMaster.player_Name);
        //form.AddField("username", "darrenk");

        //Items
        WWW goldData = new WWW("http://keefem.000webhostapp.com/Select_Online_Gold.php", form);
        //WWW goldData = new WWW("http://fldstts.000webhostapp.com/Select_Online_Gold.php", form);

        yield return goldData;

        string goldDataString = goldData.text;
        
        if (!string.IsNullOrEmpty(goldDataString))
        {
            Gold = goldDataString.Split(';');

            Gold_Data = new int[Gold.Length];
            
            for (int i = 0; i < Gold.Length - 1; i++)
            {
                Gold_Data[i] = int.Parse(GetDataValue(Gold[i], "Gold:"));

                temp_Online_Gold = temp_Online_Gold + Gold_Data[i];

                GameMaster.Player_Gold = GameMaster.Player_Gold + Gold_Data[i];
            }

            GameObject.Find("Notice_Gold").GetComponent<CanvasGroup>().alpha = 1;

            txt_Notice_Gold_Player.text = "Player Get " + temp_Online_Gold.ToString() + "G From Online Shop";
            temp_Online_Gold = 0;

            Invoke("Canvas_Notice_Gone",2);

            WWWForm form_Delete_Gold = new WWWForm();
            form_Delete_Gold.AddField("username", GameMaster.player_Name);
            WWW url = new WWW(url_Delete_Gold, form_Delete_Gold);

            System.Threading.Thread.Sleep(80);

            Button_Shop.updateGold = true;
        }

        WWW deleteGold = new WWW(url_Delete_Gold, form);
    }

    void Canvas_Notice_Gone()
    {
        GameObject.Find("Notice_Gold").GetComponent<CanvasGroup>().alpha = 0;
    }

    private void Check_Online()
    {
        City_Scene = SceneManager.GetActiveScene();

        if (City_Scene.name == "HomeTown" && GameMaster.Online)
        {
            GameObject.Find("Canvas_Button").GetComponent<Canvas>().sortingOrder = 4;
        }
        else if (City_Scene.name == "HomeTown" && GameMaster.Online && Button_Shop.Online_Buy)
        {
            GameObject.Find("Canvas_Button").GetComponent<Canvas>().sortingOrder = 4;
        }
        else
        {
            GameObject.Find("Canvas_Inventory_Player").GetComponent<Canvas>().sortingOrder = 3;
            if (City_Scene.name == "HomeTown" && !GameMaster.Online)
            {
                GameObject.Find("Canvas_Button").GetComponent<Canvas>().sortingOrder = 3;
            }
        }
    }

	private void Check_Continue()
	{
        if (player_Continue && GameMaster.field)
        {
            GameObject.Find("Canvas_Dead").GetComponent<Canvas>().sortingOrder = 0;
            GameObject.Find("Canvas_Dead").GetComponent<CanvasGroup>().alpha = 0;
            player_Continue = false;
            hp.Initialize(initHp, initHp);
            this.transform.localPosition = new Vector2(-2.27f, -1.59f);
            player_Dead = false;
            isDead = false;
            
            EXP_Plus = -(lvl * 10); 
        }
        else if (player_Continue && GameMaster.boss)
        {
            GameObject.Find("Canvas_Dead").GetComponent<Canvas>().sortingOrder = 0;
            GameObject.Find("Canvas_Dead").GetComponent<CanvasGroup>().alpha = 0;
            player_Continue = false;
            hp.Initialize(initHp, initHp);

            SceneManager.LoadScene("Loading_Screen");
            GameMaster.loadingScreen = true;

            GameMaster.enemySpawn = true;

            GameMaster.city = false;
            GameMaster.field = true;
            GameMaster.house = false;
            GameMaster.boss = false;

            GameMaster.fromBoss = false;
            GameMaster.fromHouse = false;

            this.transform.localPosition = new Vector2(-10.13f, 16.30f);
            player_Dead = false;
            isDead = false;

            EXP_Plus = -(lvl * 10);
        }
	}

	private void Check_Gold()
	{
		if (Button_Shop.updateGold) 
		{
			SetStats (0, 0);
			Button_Shop.updateGold = false;
		}
	}

	private void Check_Lvl_Up()
	{
		if (lvlUp) 
		{
			initHp += 2;
			hp.Initialize (initHp, initHp);
			Player_Max_Hp = hp.MaxHp;

			baseStr += 2;
			baseDef += 2;

			SetStats (0, 0);
		}
        if (GameMaster.Change_Day)
        {
            hp.Initialize(initHp, initHp);
            Player_Max_Hp = hp.MaxHp;
        }
	}

	private void Check_Plus_Minus()
	{
        if (EXP_Plus < 0)
        {
            exp.MycurrentValue_EXP = exp.MycurrentValue_EXP + EXP_Plus;
            if (exp.MycurrentValue_EXP <= 0)
            {
                exp.MycurrentValue_EXP = 0;
            }
            EXP_Plus = 0;
        }
        if (EXP_Plus > 0) {
			exp.MycurrentValue_EXP = exp.MycurrentValue_EXP + EXP_Plus;
			EXP_Plus = 0;
		}
		if (HP_Minus < 0) {
			hp.MycurrentValue = hp.MycurrentValue + HP_Minus;
			HP_Minus = 0;
		}
		if (HP_Minus > 0) {
            hp.MycurrentValue = hp.MycurrentValue - HP_Minus;
			HP_Minus = 0;
		}
		if (HP_Plus > 0) {
			hp.MycurrentValue = hp.MycurrentValue + HP_Plus;
			HP_Plus = 0;
		}
	}

	public void Check_Game_Over()
	{
		if (hp.MycurrentValue <= 0) 
		{
			isDead = true;
			player_Dead = isDead;

			GameObject.Find ("Canvas_Dead").GetComponent<Canvas> ().sortingOrder = 5;
			GameObject.Find ("Canvas_Dead").GetComponent<CanvasGroup> ().alpha = 1;
		}
	}

    string username;
    string item_name;
    int times_save;

    public static string Player_Move;
    public static bool Btn_Player_Attack;
    public static bool Btn_Player_Inventory;
    public static bool Btn_Player_Profile;
    public static bool Btn_Player_Informations;

    public static int ctr_Day_Farm1;
    public static int ctr_Day_Farm2;
    public static int ctr_Day_Farm3;
    public static int ctr_Day_Farm4;
    public static int ctr_Day_Farm5;
    public static int ctr_Day_Farm6;
    public static int ctr_Day_Farm7;
    public static int ctr_Day_Farm8;
    public static int ctr_Day_Farm9;

    private int temp_Use = 0;

    public void GetInput()
	{
		direction = Vector2.zero;

		if (!isDead && !GameMaster.Shop_Open && !GameMaster.loadingScreen) 
		{
            if (Input.GetKey (KeyCode.W) || Player_Move == "Up") {
				direction += Vector2.up;
				Player_Attack_Pos (0, 0.6f);
			}
			if (Input.GetKey (KeyCode.A) || Player_Move == "Left") {
				direction += Vector2.left;
				Player_Attack_Pos (-0.5f, 0);
			}
			if (Input.GetKey (KeyCode.S) || Player_Move == "Down") {
				direction += Vector2.down;
				Player_Attack_Pos (0, -0.6f);
			}
			if (Input.GetKey (KeyCode.D) || Player_Move == "Right") {
				direction += Vector2.right;
				Player_Attack_Pos (0.5f, 0);
			}
			if (Input.GetKeyDown (KeyCode.Q) || Btn_Player_Informations) {
				if(GameObject.Find("Crafting_List").GetComponent<CanvasGroup>().alpha == 0)
				{
					GameObject.Find ("Crafting_List").GetComponent<CanvasGroup> ().alpha = 1;
					GameObject.Find ("Canvas_Crafting").GetComponent<Canvas> ().sortingOrder = 4;
				}
				else if(GameObject.Find("Crafting_List").GetComponent<CanvasGroup>().alpha == 1)
				{
					GameObject.Find ("Crafting_List").GetComponent<CanvasGroup> ().alpha = 0;
					GameObject.Find ("Canvas_Crafting").GetComponent<Canvas> ().sortingOrder = 0;
				}
                Btn_Player_Informations = false;
			}
			if (Input.GetKeyDown (KeyCode.E)) {
				if (chest != null) 
				{
					chest.open ();
				}
			}
			if (Input.GetKeyDown (KeyCode.C)) {
				if (craft != null) 
				{
					craft.open ();
				}
			}
            if (Input.GetKeyDown(KeyCode.L))
            {
                /*
                ctr_Day_Farm1 = 3;                
                Farming.plant_Type = "Tomato";
                Farming.planted = true;

                ctr_Day_Farm5 = 6;
                Farming_5.plant_Type = "Corn";
                Farming_5.planted = true;

                ctr_Day_Farm4 = 1;
                Farming_4.plant_Type = "Corn";
                Farming_4.planted = true;
                */

                StartCoroutine(Load_Data());   
            }

            if (loading_Data)
            {
                //Level
                
                lvl = lvl_Data[0];

                maxExp += lvl * 100;
                exp.Initialize(newExp, maxExp);

                exp.MycurrentValue_EXP = exp_Data[0];

                GameMaster.Player_Gold = gold_Data[0];

                for (int i = 0; i < lvl - 1; i++)
                {
                    initHp += 2;
                    hp.Initialize(initHp, initHp);
                    Player_Max_Hp = hp.MaxHp;

                    baseStr += 2;
                    baseDef += 2;

                    SetStats(0, 0);
                }

                //Equipment
                for (int i = 0; i < 3; i++)
                {
                    //Sword
                    if (item_Data[i] == "Rare Sword")
                    {
                        GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                        tmp.AddComponent<Items_Script>();
                        Items_Script newWeapon = tmp.GetComponent<Items_Script>();
                        newWeapon.Item = (Items)Inventory_Manager.Instance.ItemContainer.Weapons[0].Clone();
                        newWeapon.Item.Durability = equipment_Durability_Data[i];
                        tmp_inventory.GetComponent<Inventory>().addItem(newWeapon);
                        Destroy(tmp);
                    }
                    else if (item_Data[i] == "Decent Sword")
                    {
                        GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                        tmp.AddComponent<Items_Script>();
                        Items_Script newWeapon = tmp.GetComponent<Items_Script>();
                        newWeapon.Item = (Items)Inventory_Manager.Instance.ItemContainer.Weapons[1].Clone();
                        newWeapon.Item.Durability = equipment_Durability_Data[i];
                        tmp_inventory.GetComponent<Inventory>().addItem(newWeapon);
                        Destroy(tmp);
                    }
                    else if (item_Data[i] == "Bone Sword")
                    {
                        GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                        tmp.AddComponent<Items_Script>();
                        Items_Script newWeapon = tmp.GetComponent<Items_Script>();
                        newWeapon.Item = (Items)Inventory_Manager.Instance.ItemContainer.Weapons[2].Clone();
                        newWeapon.Item.Durability = equipment_Durability_Data[i];
                        tmp_inventory.GetComponent<Inventory>().addItem(newWeapon);
                        Destroy(tmp);
                    }
                    else if (item_Data[i] == "Wooden Sword")
                    {
                        GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                        tmp.AddComponent<Items_Script>();
                        Items_Script newWeapon = tmp.GetComponent<Items_Script>();
                        newWeapon.Item = (Items)Inventory_Manager.Instance.ItemContainer.Weapons[3].Clone();
                        newWeapon.Item.Durability = equipment_Durability_Data[i];
                        tmp_inventory.GetComponent<Inventory>().addItem(newWeapon);
                        Destroy(tmp);
                    }
                    else if (item_Data[i] == "Machette")
                    {
                        GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                        tmp.AddComponent<Items_Script>();
                        Items_Script newWeapon = tmp.GetComponent<Items_Script>();
                        newWeapon.Item = (Items)Inventory_Manager.Instance.ItemContainer.Weapons[4].Clone();
                        newWeapon.Item.Durability = equipment_Durability_Data[i];
                        tmp_inventory.GetComponent<Inventory>().addItem(newWeapon);
                        Destroy(tmp);
                    }

                    //Armor
                    else if (item_Data[i] == "Rare Armor")
                    {
                        GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                        tmp.AddComponent<Items_Script>();
                        Items_Script newArmor = tmp.GetComponent<Items_Script>();
                        newArmor.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[0].Clone();
                        newArmor.Item.Durability = equipment_Durability_Data[i];
                        tmp_inventory.GetComponent<Inventory>().addItem(newArmor);
                        Destroy(tmp);
                    }
                    else if (item_Data[i] == "Wooden Armor")
                    {
                        GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                        tmp.AddComponent<Items_Script>();
                        Items_Script newArmor = tmp.GetComponent<Items_Script>();
                        newArmor.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[2].Clone();
                        newArmor.Item.Durability = equipment_Durability_Data[i];
                        tmp_inventory.GetComponent<Inventory>().addItem(newArmor);
                        Destroy(tmp);
                    }
                    else if (item_Data[i] == "Plate Wood Armor")
                    {
                        GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                        tmp.AddComponent<Items_Script>();
                        Items_Script newArmor = tmp.GetComponent<Items_Script>();
                        newArmor.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[3].Clone();
                        newArmor.Item.Durability = equipment_Durability_Data[i];
                        tmp_inventory.GetComponent<Inventory>().addItem(newArmor);
                        Destroy(tmp);
                    }
                    else if (item_Data[i] == "Steel Armor")
                    {
                        GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                        tmp.AddComponent<Items_Script>();
                        Items_Script newArmor = tmp.GetComponent<Items_Script>();
                        newArmor.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[4].Clone();
                        newArmor.Item.Durability = equipment_Durability_Data[i];
                        tmp_inventory.GetComponent<Inventory>().addItem(newArmor);
                        Destroy(tmp);
                    }
                    else if (item_Data[i] == "Knight Armor")
                    {
                        GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                        tmp.AddComponent<Items_Script>();
                        Items_Script newArmor = tmp.GetComponent<Items_Script>();
                        newArmor.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[5].Clone();
                        newArmor.Item.Durability = equipment_Durability_Data[i];
                        tmp_inventory.GetComponent<Inventory>().addItem(newArmor);
                        Destroy(tmp);
                    }

                    //Shield
                    else if (item_Data[i] == "Rare Shield")
                    {
                        GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                        tmp.AddComponent<Items_Script>();
                        Items_Script newShield = tmp.GetComponent<Items_Script>();
                        newShield.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[1].Clone();
                        newShield.Item.Durability = equipment_Durability_Data[i];
                        tmp_inventory.GetComponent<Inventory>().addItem(newShield);
                        Destroy(tmp);
                    }
                    else if (item_Data[i] == "Wooden Shield")
                    {
                        GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                        tmp.AddComponent<Items_Script>();
                        Items_Script newShield = tmp.GetComponent<Items_Script>();
                        newShield.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[6].Clone();
                        newShield.Item.Durability = equipment_Durability_Data[i];
                        tmp_inventory.GetComponent<Inventory>().addItem(newShield);
                        Destroy(tmp);
                    }
                    else if (item_Data[i] == "Normal Shield")
                    {
                        GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                        tmp.AddComponent<Items_Script>();
                        Items_Script newShield = tmp.GetComponent<Items_Script>();
                        newShield.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[7].Clone();
                        newShield.Item.Durability = equipment_Durability_Data[i];
                        tmp_inventory.GetComponent<Inventory>().addItem(newShield);
                        Destroy(tmp);
                    }
                    else if (item_Data[i] == "Steel Shield")
                    {
                        GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                        tmp.AddComponent<Items_Script>();
                        Items_Script newShield = tmp.GetComponent<Items_Script>();
                        newShield.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[8].Clone();
                        newShield.Item.Durability = equipment_Durability_Data[i];
                        tmp_inventory.GetComponent<Inventory>().addItem(newShield);
                        Destroy(tmp);
                    }
                    else if (item_Data[i] == "Knight Shield")
                    {
                        GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                        tmp.AddComponent<Items_Script>();
                        Items_Script newShield = tmp.GetComponent<Items_Script>();
                        newShield.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[9].Clone();
                        newShield.Item.Durability = equipment_Durability_Data[i];
                        tmp_inventory.GetComponent<Inventory>().addItem(newShield);
                        Destroy(tmp);
                    }
                }
                
                //Items
                for (int i = 0; i < 9; i++)
                {
                    //Seeds
                    if (items_user_Data[i] == "Tomato Seed")
                    {
                        for (int j = 0; j < times_Data[i]; j++)
                        {
                            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                            tmp.AddComponent<Items_Script>();
                            Items_Script newMaterial = tmp.GetComponent<Items_Script>();
                            newMaterial.Item = Inventory_Manager.Instance.ItemContainer.Materials[12];
                            tmp_inventory.GetComponent<Inventory>().addItem(newMaterial);
                            Destroy(tmp);
                        }
                    }
                    else if (items_user_Data[i] == "Corn Seed")
                    {
                        for (int j = 0; j < times_Data[i]; j++)
                        {
                            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                            tmp.AddComponent<Items_Script>();
                            Items_Script newMaterial = tmp.GetComponent<Items_Script>();
                            newMaterial.Item = Inventory_Manager.Instance.ItemContainer.Materials[11];
                            tmp_inventory.GetComponent<Inventory>().addItem(newMaterial);
                            Destroy(tmp);
                        }
                    }

                    //Vegetables
                    else if (items_user_Data[i] == "Tomato")
                    {
                        for (int j = 0; j < times_Data[i]; j++)
                        {
                            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                            tmp.AddComponent<Items_Script>();
                            Items_Script newMaterial = tmp.GetComponent<Items_Script>();
                            newMaterial.Item = Inventory_Manager.Instance.ItemContainer.Materials[13];
                            inventory.addItem(newMaterial);
                            Destroy(tmp);
                        }
                    }
                    else if (items_user_Data[i] == "Corn")
                    {
                        for (int j = 0; j < times_Data[i]; j++)
                        {
                            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                            tmp.AddComponent<Items_Script>();
                            Items_Script newMaterial = tmp.GetComponent<Items_Script>();
                            newMaterial.Item = Inventory_Manager.Instance.ItemContainer.Materials[14];
                            inventory.addItem(newMaterial);
                            Destroy(tmp);
                        }
                    }

                    //Consumables
                    else if (items_user_Data[i] == "HP Potion")
                    {
                        for (int j = 0; j < times_Data[i]; j++)
                        {
                            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                            tmp.AddComponent<Items_Script>();
                            Items_Script newConsumable = tmp.GetComponent<Items_Script>();
                            newConsumable.Item = Inventory_Manager.Instance.ItemContainer.Consumables[0];
                            tmp_inventory.GetComponent<Inventory>().addItem(newConsumable);
                            Destroy(tmp);
                        }
                    }

                    //Sword
                    else if (items_user_Data[i] == "Rare Sword")
                    {
                        for (int j = 0; j < times_Data[i]; j++)
                        {
                            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                            tmp.AddComponent<Items_Script>();
                            Items_Script newWeapon = tmp.GetComponent<Items_Script>();
                            newWeapon.Item = (Items)Inventory_Manager.Instance.ItemContainer.Weapons[0].Clone();
                            newWeapon.Item.Durability = durability_Data[i];
                            tmp_inventory.GetComponent<Inventory>().addItem(newWeapon);
                            Destroy(tmp);
                        }
                    }
                    else if (items_user_Data[i] == "Decent Sword")
                    {
                        for (int j = 0; j < times_Data[i]; j++)
                        {
                            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                            tmp.AddComponent<Items_Script>();
                            Items_Script newWeapon = tmp.GetComponent<Items_Script>();
                            newWeapon.Item = (Items)Inventory_Manager.Instance.ItemContainer.Weapons[1].Clone();
                            newWeapon.Item.Durability = durability_Data[i];
                            tmp_inventory.GetComponent<Inventory>().addItem(newWeapon);
                            Destroy(tmp);
                        }
                    }
                    else if (items_user_Data[i] == "Bone Sword")
                    {
                        for (int j = 0; j < times_Data[i]; j++)
                        {
                            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                            tmp.AddComponent<Items_Script>();
                            Items_Script newWeapon = tmp.GetComponent<Items_Script>();
                            newWeapon.Item = (Items)Inventory_Manager.Instance.ItemContainer.Weapons[2].Clone();
                            newWeapon.Item.Durability = durability_Data[i];
                            tmp_inventory.GetComponent<Inventory>().addItem(newWeapon);
                            Destroy(tmp);
                        }
                    }
                    else if (items_user_Data[i] == "Wooden Sword")
                    {
                        for (int j = 0; j < times_Data[i]; j++)
                        {
                            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                            tmp.AddComponent<Items_Script>();
                            Items_Script newWeapon = tmp.GetComponent<Items_Script>();
                            newWeapon.Item = (Items)Inventory_Manager.Instance.ItemContainer.Weapons[3].Clone();
                            newWeapon.Item.Durability = durability_Data[i];
                            tmp_inventory.GetComponent<Inventory>().addItem(newWeapon);
                            Destroy(tmp);
                        }
                    }
                    else if (items_user_Data[i] == "Machette")
                    {
                        for (int j = 0; j < times_Data[i]; j++)
                        {
                            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                            tmp.AddComponent<Items_Script>();
                            Items_Script newWeapon = tmp.GetComponent<Items_Script>();
                            newWeapon.Item = (Items)Inventory_Manager.Instance.ItemContainer.Weapons[4].Clone();
                            newWeapon.Item.Durability = durability_Data[i];
                            tmp_inventory.GetComponent<Inventory>().addItem(newWeapon);
                            Destroy(tmp);
                        }
                    }

                    //Shields
                    else if (items_user_Data[i] == "Rare Shield")
                    {
                        for (int j = 0; j < times_Data[i]; j++)
                        {
                            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                            tmp.AddComponent<Items_Script>();
                            Items_Script newShield = tmp.GetComponent<Items_Script>();
                            newShield.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[1].Clone();
                            newShield.Item.Durability = durability_Data[i];
                            tmp_inventory.GetComponent<Inventory>().addItem(newShield);
                            Destroy(tmp);
                        }
                    }
                    else if (items_user_Data[i] == "Wooden Shield")
                    {
                        for (int j = 0; j < times_Data[i]; j++)
                        {
                            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                            tmp.AddComponent<Items_Script>();
                            Items_Script newShield = tmp.GetComponent<Items_Script>();
                            newShield.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[6].Clone();
                            newShield.Item.Durability = durability_Data[i];
                            tmp_inventory.GetComponent<Inventory>().addItem(newShield);
                            Destroy(tmp);
                        }
                    }
                    else if (items_user_Data[i] == "Normal Shield")
                    {
                        for (int j = 0; j < times_Data[i]; j++)
                        {
                            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                            tmp.AddComponent<Items_Script>();
                            Items_Script newShield = tmp.GetComponent<Items_Script>();
                            newShield.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[7].Clone();
                            newShield.Item.Durability = durability_Data[i];
                            tmp_inventory.GetComponent<Inventory>().addItem(newShield);
                            Destroy(tmp);
                        }
                    }
                    else if (items_user_Data[i] == "Steel Shield")
                    {
                        for (int j = 0; j < times_Data[i]; j++)
                        {
                            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                            tmp.AddComponent<Items_Script>();
                            Items_Script newShield = tmp.GetComponent<Items_Script>();
                            newShield.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[8].Clone();
                            newShield.Item.Durability = durability_Data[i];
                            tmp_inventory.GetComponent<Inventory>().addItem(newShield);
                            Destroy(tmp);
                        }
                    }
                    else if (items_user_Data[i] == "Knight Shield")
                    {
                        for (int j = 0; j < times_Data[i]; j++)
                        {
                            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                            tmp.AddComponent<Items_Script>();
                            Items_Script newShield = tmp.GetComponent<Items_Script>();
                            newShield.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[9].Clone();
                            newShield.Item.Durability = durability_Data[i];
                            tmp_inventory.GetComponent<Inventory>().addItem(newShield);
                            Destroy(tmp);
                        }
                    }

                    //Armors
                    else if (items_user_Data[i] == "Rare Armor")
                    {
                        for (int j = 0; j < times_Data[i]; j++)
                        {
                            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                            tmp.AddComponent<Items_Script>();
                            Items_Script newArmor = tmp.GetComponent<Items_Script>();
                            newArmor.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[0].Clone();
                            newArmor.Item.Durability = durability_Data[i];
                            tmp_inventory.GetComponent<Inventory>().addItem(newArmor);
                            Destroy(tmp);
                        }
                    }
                    else if (items_user_Data[i] == "Wooden Armor")
                    {
                        for (int j = 0; j < times_Data[i]; j++)
                        {
                            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                            tmp.AddComponent<Items_Script>();
                            Items_Script newArmor = tmp.GetComponent<Items_Script>();
                            newArmor.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[2].Clone();
                            newArmor.Item.Durability = durability_Data[i];
                            tmp_inventory.GetComponent<Inventory>().addItem(newArmor);
                            Destroy(tmp);
                        }
                    }
                    else if (items_user_Data[i] == "Plate Wood Armor")
                    {
                        for (int j = 0; j < times_Data[i]; j++)
                        {
                            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                            tmp.AddComponent<Items_Script>();
                            Items_Script newArmor = tmp.GetComponent<Items_Script>();
                            newArmor.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[3].Clone();
                            newArmor.Item.Durability = durability_Data[i];
                            tmp_inventory.GetComponent<Inventory>().addItem(newArmor);
                            Destroy(tmp);
                        }
                    }
                    else if (items_user_Data[i] == "Steel Armor")
                    {
                        for (int j = 0; j < times_Data[i]; j++)
                        {
                            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                            tmp.AddComponent<Items_Script>();
                            Items_Script newArmor = tmp.GetComponent<Items_Script>();
                            newArmor.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[4].Clone();
                            newArmor.Item.Durability = durability_Data[i];
                            tmp_inventory.GetComponent<Inventory>().addItem(newArmor);
                            Destroy(tmp);
                        }
                    }
                    else if (items_user_Data[i] == "Knight Armor")
                    {
                        for (int j = 0; j < times_Data[i]; j++)
                        {
                            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                            tmp.AddComponent<Items_Script>();
                            Items_Script newArmor = tmp.GetComponent<Items_Script>();
                            newArmor.Item = (Items)Inventory_Manager.Instance.ItemContainer.Equipments[5].Clone();
                            newArmor.Item.Durability = durability_Data[i];
                            tmp_inventory.GetComponent<Inventory>().addItem(newArmor);
                            Destroy(tmp);
                        }
                    }

                    //Materials
                    else if (items_user_Data[i] == "Bone")
                    {
                        for (int j = 0; j < times_Data[i]; j++)
                        {
                            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                            tmp.AddComponent<Items_Script>();
                            Items_Script newMaterials = tmp.GetComponent<Items_Script>();
                            newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials[1];
                            tmp_inventory.GetComponent<Inventory>().addItem(newMaterials);
                            Destroy(tmp);
                        }
                    }
                    else if (items_user_Data[i] == "Gold Bar")
                    {
                        for (int j = 0; j < times_Data[i]; j++)
                        {
                            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                            tmp.AddComponent<Items_Script>();
                            Items_Script newMaterials = tmp.GetComponent<Items_Script>();
                            newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials[2];
                            tmp_inventory.GetComponent<Inventory>().addItem(newMaterials);
                            Destroy(tmp);
                        }
                    }
                    else if (items_user_Data[i] == "Golden Horn")
                    {
                        for (int j = 0; j < times_Data[i]; j++)
                        {
                            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                            tmp.AddComponent<Items_Script>();
                            Items_Script newMaterials = tmp.GetComponent<Items_Script>();
                            newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials[3];
                            tmp_inventory.GetComponent<Inventory>().addItem(newMaterials);
                            Destroy(tmp);
                        }
                    }
                    else if (items_user_Data[i] == "Iron Bar")
                    {
                        for (int j = 0; j < times_Data[i]; j++)
                        {
                            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                            tmp.AddComponent<Items_Script>();
                            Items_Script newMaterials = tmp.GetComponent<Items_Script>();
                            newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials[4];
                            tmp_inventory.GetComponent<Inventory>().addItem(newMaterials);
                            Destroy(tmp);
                        }
                    }
                    else if (items_user_Data[i] == "Iron Pot")
                    {
                        for (int j = 0; j < times_Data[i]; j++)
                        {
                            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                            tmp.AddComponent<Items_Script>();
                            Items_Script newMaterials = tmp.GetComponent<Items_Script>();
                            newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials[5];
                            tmp_inventory.GetComponent<Inventory>().addItem(newMaterials);
                            Destroy(tmp);
                        }
                    }
                    else if (items_user_Data[i] == "Slime Gem")
                    {
                        for (int j = 0; j < times_Data[i]; j++)
                        {
                            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                            tmp.AddComponent<Items_Script>();
                            Items_Script newMaterials = tmp.GetComponent<Items_Script>();
                            newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials[6];
                            tmp_inventory.GetComponent<Inventory>().addItem(newMaterials);
                            Destroy(tmp);
                        }
                    }
                    else if (items_user_Data[i] == "Slime Liquid")
                    {
                        for (int j = 0; j < times_Data[i]; j++)
                        {
                            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                            tmp.AddComponent<Items_Script>();
                            Items_Script newMaterials = tmp.GetComponent<Items_Script>();
                            newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials[7];
                            tmp_inventory.GetComponent<Inventory>().addItem(newMaterials);
                            Destroy(tmp);
                        }
                    }
                    else if (items_user_Data[i] == "Wolf Fang")
                    {
                        for (int j = 0; j < times_Data[i]; j++)
                        {
                            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                            tmp.AddComponent<Items_Script>();
                            Items_Script newMaterials = tmp.GetComponent<Items_Script>();
                            newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials[8];
                            tmp_inventory.GetComponent<Inventory>().addItem(newMaterials);
                            Destroy(tmp);
                        }
                    }
                    else if (items_user_Data[i] == "Wolf Skin")
                    {
                        for (int j = 0; j < times_Data[i]; j++)
                        {
                            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                            tmp.AddComponent<Items_Script>();
                            Items_Script newMaterials = tmp.GetComponent<Items_Script>();
                            newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials[9];
                            tmp_inventory.GetComponent<Inventory>().addItem(newMaterials);
                            Destroy(tmp);
                        }
                    }
                    else if (items_user_Data[i] == "Wood")
                    {
                        for (int j = 0; j < times_Data[i]; j++)
                        {
                            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                            tmp.AddComponent<Items_Script>();
                            Items_Script newMaterials = tmp.GetComponent<Items_Script>();
                            newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials[10];
                            tmp_inventory.GetComponent<Inventory>().addItem(newMaterials);
                            Destroy(tmp);
                        }
                    }
                    else if (items_user_Data[i] == "Corn")
                    {
                        for (int j = 0; j < times_Data[i]; j++)
                        {
                            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                            tmp.AddComponent<Items_Script>();
                            Items_Script newMaterials = tmp.GetComponent<Items_Script>();
                            newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials[14];
                            tmp_inventory.GetComponent<Inventory>().addItem(newMaterials);
                            Destroy(tmp);
                        }
                    }
                    else if (items_user_Data[i] == "Tomato")
                    {
                        for (int j = 0; j < times_Data[i]; j++)
                        {
                            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
                            tmp.AddComponent<Items_Script>();
                            Items_Script newMaterials = tmp.GetComponent<Items_Script>();
                            newMaterials.Item = Inventory_Manager.Instance.ItemContainer.Materials[13];
                            tmp_inventory.GetComponent<Inventory>().addItem(newMaterials);
                            Destroy(tmp);
                        }
                    }
                }

                //Farm
                ctr_Day_Farm1 = farm1_Data[0];
                ctr_Day_Farm2 = farm2_Data[0];
                ctr_Day_Farm3 = farm3_Data[0];
                ctr_Day_Farm4 = farm4_Data[0];
                ctr_Day_Farm5 = farm5_Data[0];
                ctr_Day_Farm6 = farm6_Data[0];
                ctr_Day_Farm7 = farm7_Data[0];
                ctr_Day_Farm8 = farm8_Data[0];
                ctr_Day_Farm9 = farm9_Data[0];

                /*
                Farming.dayCounter = farm1_Data[0];
                Farming_2.dayCounter = farm2_Data[0];
                Farming_3.dayCounter = farm3_Data[0];
                Farming_4.dayCounter = farm4_Data[0];
                Farming_5.dayCounter = farm5_Data[0];
                Farming_6.dayCounter = farm6_Data[0];
                Farming_7.dayCounter = farm7_Data[0];
                Farming_8.dayCounter = farm8_Data[0];
                Farming_9.dayCounter = farm9_Data[0];
                */

                Farming.plant_Type = farm1_Type[0];
                Farming_2.plant_Type = farm2_Type[0];
                Farming_3.plant_Type = farm3_Type[0];
                Farming_4.plant_Type = farm4_Type[0];
                Farming_5.plant_Type = farm5_Type[0];
                Farming_6.plant_Type = farm6_Type[0];
                Farming_7.plant_Type = farm7_Type[0];
                Farming_8.plant_Type = farm8_Type[0];
                Farming_9.plant_Type = farm9_Type[0];

                if (Farming.plant_Type != "None")
                {
                    Farming.planted = true;
                }
                if (Farming_2.plant_Type != "None")
                {
                    Farming_2.planted = true;
                }
                if (Farming_3.plant_Type != "None")
                {
                    Farming_3.planted = true;
                }
                if (Farming_4.plant_Type != "None")
                {
                    Farming_5.planted = true;
                }
                if (Farming_6.plant_Type != "None")
                {
                    Farming_6.planted = true;
                }
                if (Farming_7.plant_Type != "None")
                {
                    Farming_7.planted = true;
                }
                if (Farming_8.plant_Type != "None")
                {
                    Farming_8.planted = true;
                }
                if (Farming_9.plant_Type != "None")
                {
                    Farming_9.planted = true;
                }

                GameMaster.EndGame = end_Data[0];
                Debug.Log(GameMaster.EndGame);

                //Quest
                GameMaster.Questing_Global = quest_Data[0];
                Quest_Object.enemyCountKill = quest_Kill_Data[0];

                if (GameMaster.Questing_Global != 0)
                {
                    if (GameMaster.Questing_Global >= quest_Manager.quests.Length)
                    {
                        GameMaster.Questing_Global = 0;
                        Quest_Trigger.quest_Number = GameMaster.Questing_Global;
                        quest_Manager.quests[GameMaster.Questing_Global].gameObject.SetActive(true);
                    }
                    else
                    {
                        Quest_Trigger.quest_Number = GameMaster.Questing_Global;
                        quest_Manager.quests[GameMaster.Questing_Global].gameObject.SetActive(true);
                    }
                }
                loading_Data = false;
            }

            /*
			if (Input.GetKeyDown (KeyCode.H) && GameMaster.city) {
				StartCoroutine (Hoe ());
			}
			*/

            if (Input.GetKeyDown (KeyCode.Space) && OkAttack && GameMaster.boss || Btn_Player_Attack && OkAttack && GameMaster.boss) {
				//Coroutine digunakan untuk menjalankan suatu function di background
				//isAttack = true;
				//animator.SetTrigger("Attack_Player");
				StartCoroutine (Attack_Boss ());
                audio_Sword = true;

                Btn_Player_Attack = false;
            }
			if (Input.GetKeyDown (KeyCode.Space) && OkAttack && GameMaster.field || Btn_Player_Attack && OkAttack && GameMaster.field) 
			{
				//Coroutine digunakan untuk menjalankan suatu function di background
				//isAttack = true;
				//animator.SetTrigger("Attack_Player");
				StartCoroutine (Attack ());
                audio_Sword = true;

                Btn_Player_Attack = false;
            }
		}
		if (!isDead && !GameMaster.loadingScreen) 
		{
			if (Input.GetKeyDown (KeyCode.I) || Btn_Player_Inventory) {
				inventory.open ();
                Btn_Player_Inventory = false;
			}
			if (Input.GetKeyDown (KeyCode.P) || Btn_Player_Profile) {
				player_Panel.open ();
                Btn_Player_Profile = false;
			}
		}
	}

    public static string[] user;
    public static string[] user_Data;
    public static string[] items_user;
    public static string[] items_user_Data;
    public static string[] times;
    public static int[] times_Data;
    public static string[] durability;
    public static int[] durability_Data;

    public static int[] farm1_Data;
    public static string[] farm1_Type;
    public static int[] farm2_Data;
    public static string[] farm2_Type;
    public static int[] farm3_Data;
    public static string[] farm3_Type;
    public static int[] farm4_Data;
    public static string[] farm4_Type;
    public static int[] farm5_Data;
    public static string[] farm5_Type;
    public static int[] farm6_Data;
    public static string[] farm6_Type;
    public static int[] farm7_Data;
    public static string[] farm7_Type;
    public static int[] farm8_Data;
    public static string[] farm8_Type;
    public static int[] farm9_Data;
    public static string[] farm9_Type;

    public static int[] quest_Data;
    public static int[] quest_Kill_Data;

    public static string[] end_Data;

    public static string[] item_Data;
    public static int[] equipment_Durability_Data;

    public static int[] lvl_Data;
    public static int[] exp_Data;
    public static int[] gold_Data;

    public static bool  loading_Data;

    public IEnumerator Load_Data()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", GameMaster.player_Name);

        WWW itemsData = new WWW("http://keefem.000webhostapp.com/Load_Data.php", form);
        //WWW itemsData = new WWW("http://fldstts.000webhostapp.com/Load_Data.php", form);

        yield return itemsData;

        string itemsDataString = itemsData.text;
        
        user = itemsDataString.Split(';');
        items_user = itemsDataString.Split(';');
        times = itemsDataString.Split(';');

        user_Data = new string[user.Length];
        items_user_Data = new string[user.Length];
        times_Data = new int[user.Length];

        for (int i = 0; i < user_Data.Length - 1; i++)
        {
            user_Data[i] = GetDataValue(user[i], "Username:");
            items_user_Data[i] = GetDataValue(user[i], "Name:");
            times_Data[i] = int.Parse(GetDataValue(user[i], "Times:"));
            /*
			items_Place [i] = true;
			items_Data [i] = GetDataValue (items [i], "Name:");
			type_Data [i] = GetDataValue (type [i], "Type:");
			price_Data [i] = GetDataValue (price [i], "Price:");
			ctr_Data++;
            */
        }

        loading_Data = true;
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

    private void Player_Attack_Pos(float PosX,float PosY)
	{
		Vector3 thePosition = transform.localPosition;
		thePosition.x = PosX;
		thePosition.y = PosY;
		Attacking_Pos.transform.localPosition = thePosition;
	}

	private IEnumerator Hoe()
	{
		if(!isAttack && !isMoving && !isHoe && GameMaster.city)
		{
			isHoe = true;
			Hoeing = true;
			animator.SetBool ("Hoe", isHoe);
			//Untuk Waktu Menjalankan Sesuatu
			yield return new WaitForSeconds (0.7f);
			StopHoe ();
		}
	}

	private IEnumerator Attack_Boss()
	{
		if(!isAttack && !isMoving && !isHoe && GameMaster.boss)
		{
			isAttack = true;
			Attacking = true;
			animator.SetBool ("Attack", isAttack);
			//Untuk Waktu Menjalankan Sesuatu
			yield return new WaitForSeconds (0.5f);
			StopAttack ();
		}
	}

	private IEnumerator Attack()
	{
		if(!isAttack && !isMoving && !isHoe && GameMaster.field)
		{
			isAttack = true;
			Attacking = true;
			animator.SetBool ("Attack", isAttack);
			//Untuk Waktu Menjalankan Sesuatu
			yield return new WaitForSeconds (0.5f);
			StopAttack ();
		}
	}

	public void CloseAll()
	{
		if(player_Panel.IsOpen)
		{
			player_Panel.open ();
		}
		if(inventory.IsOpen)
		{
			inventory.open ();
		}
        if (GameObject.Find("Crafting_List").GetComponent<CanvasGroup>().alpha == 1)
        {
            GameObject.Find("Crafting_List").GetComponent<CanvasGroup>().alpha = 0;
        }
        if (Player_Move != null)
        {
            Player_Move = null;
        }
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Map_City") && !GameMaster.fromHouse) {

			CloseAll ();

			SceneManager.LoadScene ("Loading_Screen");
			GameMaster.loadingScreen = true;

			GameMaster.enemySpawn = true;

			GameMaster.city = true;
			GameMaster.field = false;
			GameMaster.house = false;
			GameMaster.boss = false;

			GameMaster.fromBoss = false;
            GameMaster.fromHouse = false;

			this.transform.position = new Vector3 (0.78f, 8, 0);
			Quest_Trigger.ctrSign = 1;
		}
		else if (other.CompareTag ("Map_City") && GameMaster.fromHouse) {

			CloseAll ();

			SceneManager.LoadScene ("Loading_Screen");
			GameMaster.loadingScreen = true;

			GameMaster.enemySpawn = true;

			GameMaster.city = true;
			GameMaster.field = false;
			GameMaster.house = false;
			GameMaster.boss = false;

			GameMaster.fromBoss = false;
			GameMaster.fromHouse = false;

            Player_Move = "None";

            this.transform.position = new Vector3 (-4.5f, -13f, 0);
			Quest_Trigger.ctrSign = 1;
		}
		else if (other.CompareTag ("Map_Field") && !GameMaster.fromBoss) {
			
			CloseAll ();

			SceneManager.LoadScene ("Loading_Screen");
			GameMaster.loadingScreen = true;

			GameMaster.enemySpawn = false;

			GameMaster.city = false;
			GameMaster.field = true;
			GameMaster.house = false;
			GameMaster.boss = false;

			GameMaster.fromBoss = false;
			GameMaster.fromHouse = false;

            Player_Move = "None";

            this.transform.position = new Vector3 (-2.3f, -3.25f, 0);

		}
		else if (other.CompareTag ("Map_Field") && GameMaster.fromBoss) {

			CloseAll ();

			SceneManager.LoadScene ("Loading_Screen");
			GameMaster.loadingScreen = true;

			GameMaster.enemySpawn = false;

			GameMaster.city = false;
			GameMaster.field = true;
			GameMaster.house = false;
			GameMaster.boss = false;

			GameMaster.fromBoss = false;
			GameMaster.fromHouse = false;

            Player_Move = "None";

            this.transform.position = new Vector3 (-9.9f, 18.348f, 0);

		}
		else if (other.CompareTag ("Map_Boss")) {

			CloseAll ();

			SceneManager.LoadScene ("Loading_Screen");
			GameMaster.loadingScreen = true;

			GameMaster.enemySpawn = false;

			GameMaster.city = false;
			GameMaster.field = false;
			GameMaster.house = false;
			GameMaster.boss = true;

			GameMaster.fromBoss = true;
			GameMaster.fromHouse = false;

            Player_Move = "None";

            this.transform.position = new Vector3 (0.085f, -10.96f, 0);
		}
		else if (other.CompareTag ("House")) {

			CloseAll ();

			SceneManager.LoadScene ("Loading_Screen");
			GameMaster.loadingScreen = true;

			GameMaster.enemySpawn = false;

			GameMaster.city = false;
			GameMaster.field = false;
			GameMaster.house = true;
			GameMaster.boss = false;

			GameMaster.fromBoss = false;
			GameMaster.fromHouse = true;

            Player_Move = "None";

            this.transform.position = new Vector3 (4.4f, -2.34f, 0);
		}
		else if (other.CompareTag ("Bed")) {
			GameObject.Find ("Canvas_Button_Yes_No_ChangeDay").GetComponent<Canvas> ().sortingOrder = 2;
			GameObject.Find ("Yes_No_ChangeDay").GetComponent<CanvasGroup> ().alpha = 1;
			GameObject.Find ("Yes_No_Upgrade").GetComponent<CanvasGroup> ().alpha = 0;
            GameObject.Find("Yes_No_Save").GetComponent<CanvasGroup>().alpha = 0;
        }

		if (other.CompareTag ("Items") && !isAttack) {
			inventory.addItem (other.GetComponent<Items_Script> ());
			Destroy (other.gameObject);
		}
		if (other.CompareTag ("Chest")) {
			OkAttack = false;
			chest = other.GetComponent<InventoryLink> ().chestInventory;
		}
		if (other.CompareTag ("Craft")) {
			OkAttack = false;
			craft = other.GetComponent<Crafting_Place> ().Crafting;

            GameObject.Find("Canvas_Crafting_Temp").GetComponent<CanvasGroup>().alpha = 1;

            if (craft != null)
            {
                craft.open();
            }
        }
		if (other.CompareTag ("Upgrade")) {
			GameObject.Find ("Canvas_Button_Yes_No_ChangeDay").GetComponent<Canvas> ().sortingOrder = 2;
			GameObject.Find ("Yes_No_Upgrade").GetComponent<CanvasGroup> ().alpha = 1;
			GameObject.Find ("Yes_No_ChangeDay").GetComponent<CanvasGroup> ().alpha = 0;
            GameObject.Find("Yes_No_Save").GetComponent<CanvasGroup>().alpha = 0;
        }
        if (other.CompareTag("Save"))
        {
            GameObject.Find("Canvas_Button_Yes_No_ChangeDay").GetComponent<Canvas>().sortingOrder = 2;
            GameObject.Find("Yes_No_Upgrade").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("Yes_No_ChangeDay").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("Yes_No_Save").GetComponent<CanvasGroup>().alpha = 1;
        }
        if (other.CompareTag ("Vendor")) {
			OkAttack = false;
			vendor = other.GetComponent<InventoryLink> ().chestInventory;
		}
		if (other.CompareTag ("Farm_NPC")) {
            
			GameObject.Find ("Canvas_Farm").GetComponent<Canvas> ().sortingOrder = 4;
			GameObject.Find ("Canvas_Farm").GetComponent<CanvasGroup> ().alpha = 1;

            Farm_NPC.tomato_Seed = 0;
            Farm_NPC.corn_Seed = 0;

            Farm_NPC.Check_Seed_Txt = true;
		}
		if(other.CompareTag("Cactus")){
			HP_Minus = -5;
			Damage_In = HP_Minus.ToString();

			Instantiate (objectnya_Txt, txt_Damage_Pos.position, Quaternion.identity);
		}
		if(other.CompareTag("Posion_Path")){
			HP_Minus = -2;
			Damage_In = HP_Minus.ToString();

			Instantiate (objectnya_Txt, txt_Damage_Pos.position, Quaternion.identity);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Chest") 
		{
			if (chest.IsOpen) 
			{
				chest.open ();
			}
			chest = null;
			OkAttack = true;
		}
		if (other.gameObject.tag == "Craft") 
		{
            Inventory_Manager.Instance.selectSize.SetActive(false);

            if (craft.IsOpen) 
			{
				craft.open ();
			}
			craft = null;
			OkAttack = true;

            GameObject.Find("Canvas_Crafting_Temp").GetComponent<CanvasGroup>().alpha = 0;
        }
		if (other.gameObject.tag == "Vendor") 
		{
			if (vendor.IsOpen) 
			{
				vendor.open ();
			}
			vendor = null;
			OkAttack = true;
		}
		if (other.CompareTag ("Upgrade")) 
		{
			GameObject.Find ("Canvas_Button_Yes_No_ChangeDay").GetComponent<Canvas> ().sortingOrder = 0;
			GameObject.Find ("Yes_No_Upgrade").GetComponent<CanvasGroup> ().alpha = 0;
		}
		if (other.gameObject.tag == "Bed") 
		{
			GameObject.Find ("Canvas_Button_Yes_No_ChangeDay").GetComponent<Canvas> ().sortingOrder = 0;
			GameObject.Find ("Yes_No_ChangeDay").GetComponent<CanvasGroup> ().alpha = 0;
		}
        if (other.gameObject.tag == "Save")
        {
            GameObject.Find("Canvas_Button_Yes_No_ChangeDay").GetComponent<Canvas>().sortingOrder = 0;
            GameObject.Find("Yes_No_Save").GetComponent<CanvasGroup>().alpha = 0;
        }
        if (other.CompareTag ("Farm_NPC")) {
            
            GameObject.Find ("Canvas_Farm").GetComponent<Canvas> ().sortingOrder = -1;
			GameObject.Find ("Canvas_Farm").GetComponent<CanvasGroup> ().alpha = 0;
        }
	}

	public void SetStats(int str, int def)
	{
        if (Quest_Object.playerBuff_Global == "Attack")
        {
            if (Quest_Object.Realationship_Guard == 0)
            {
                Quest_Object.Realationship_Guard = 1;
            }

            str = 0;

            Tstr = 3 * Quest_Object.Realationship_Guard;
		} 
		else if (Quest_Object.playerBuff_Global == string.Empty && str < 0) 
		{
			this.str = this.str + str * Quest_Object.Realationship_Guard;
			Tstr = 0;
        }
		else if (Quest_Object.playerBuff_Global == "Deff") 
		{
            if (Quest_Object.Realationship_Old_Woman == 0)
            {
                Quest_Object.Realationship_Old_Woman = 1;
            }
            Tdef = 3 * Quest_Object.Realationship_Old_Woman;
		} 
		else if (Quest_Object.playerBuff_Global == string.Empty && def < 0) 
		{
			this.def = this.def + def * Quest_Object.Realationship_Old_Woman;
			Tdef = 0;
		}

		if (lvlUp && Quest_Object.playerBuff_Global == string.Empty) 
		{
			lvl += 1;
			lvlUp = false;
			maxExp += lvl * 100;
			exp.Initialize (newExp, maxExp);
			this.str = str + baseStr;
			this.def = def + baseDef;
		}
		else if (lvlUp && Quest_Object.playerBuff_Global != string.Empty)
		{
			lvl += 1;
			lvlUp = false;
			maxExp += lvl * 100;
			exp.Initialize (newExp, maxExp);
			this.str = str + baseStr + Tstr;
			this.def = def + baseDef + Tdef;
		}

		if (!Button_Shop.updateGold) 
		{
            this.str = str + baseStr + Tstr;
			this.def = def + baseDef + Tdef;
		}

		txt_Status.text = string.Format ("Player Lvl : {3}\n\nPlayer HP : {0}\n\nPlayer Dmg : {1}\n\nPlayer Def : {2}\n\n Gold : {4}G", Player_Max_Hp, (this.str - 5) + "-" + this.str, this.def,lvl,GameMaster.Player_Gold);
	}
}
