using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Button_Shop : MonoBehaviour {

    public static bool Online_Buy;

	[SerializeField]
	private CanvasGroup canvas_Shop;

    [SerializeField]
    Text txt_Notice_Buy;

	GameObject tmp_inventory;

	public static bool updateGold;

	void Awake()
	{
		tmp_inventory = GameObject.Find ("Inventory");
	}

	// Use this for initialization
	void Start () 
	{

	}

	// Update is called once per frame
	void Update () {

	}

	public void GoBackCity()
	{
		canvas_Shop.alpha = 0;
		GameMaster.Shop_Open = false;
	}

	public void BuyPotion()
	{
		if (GameMaster.Player_Gold >= 120) 
		{
			GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);
			tmp.AddComponent<Items_Script> ();
			Items_Script newConsumable = tmp.GetComponent<Items_Script> ();
			newConsumable.Item = Inventory_Manager.Instance.ItemContainer.Consumables [0];
			tmp_inventory.GetComponent<Inventory> ().addItem (newConsumable);
			Destroy (tmp);

			GameMaster.Player_Gold = GameMaster.Player_Gold - 120;
			updateGold = true;

            GameObject.Find("Txt_Buy").GetComponent<CanvasGroup>().alpha = 1;
            txt_Notice_Buy.text = "You Just Bought " + newConsumable.Item.ItemName;

            StartCoroutine(Disable_Txt_Buy());
        }

        //Save.save_Anything_Player = true;
    }

	public void BuyTomatoSeed()
	{
		if (GameMaster.Player_Gold >= 50) 
		{
			GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);
			tmp.AddComponent<Items_Script> ();
			Items_Script newMaterial = tmp.GetComponent<Items_Script> ();
			newMaterial.Item = Inventory_Manager.Instance.ItemContainer.Materials [12];
			tmp_inventory.GetComponent<Inventory> ().addItem (newMaterial);
			Destroy (tmp);

			GameMaster.Player_Gold = GameMaster.Player_Gold - 50;
			updateGold = true;

            GameObject.Find("Txt_Buy").GetComponent<CanvasGroup>().alpha = 1;
            txt_Notice_Buy.text = "You Just Bought " + newMaterial.Item.ItemName;

            StartCoroutine(Disable_Txt_Buy());
        }

        //Save.save_Anything_Player = true;
    }

	public void BuyCornSeed()
	{
		if (GameMaster.Player_Gold >= 70) 
		{
			GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);
			tmp.AddComponent<Items_Script> ();
			Items_Script newMaterial = tmp.GetComponent<Items_Script> ();
			newMaterial.Item = Inventory_Manager.Instance.ItemContainer.Materials [11];
			tmp_inventory.GetComponent<Inventory> ().addItem (newMaterial);
			Destroy (tmp);

			GameMaster.Player_Gold = GameMaster.Player_Gold - 70;
			updateGold = true;

            GameObject.Find("Txt_Buy").GetComponent<CanvasGroup>().alpha = 1;
            txt_Notice_Buy.text = "You Just Bought " + newMaterial.Item.ItemName;

            StartCoroutine(Disable_Txt_Buy());
        }

        //Save.save_Anything_Player = true;
    }

    public void BuyWood()
    {
        if (GameMaster.Player_Gold >= 200)
        {
            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
            tmp.AddComponent<Items_Script>();
            Items_Script newMaterial = tmp.GetComponent<Items_Script>();
            newMaterial.Item = Inventory_Manager.Instance.ItemContainer.Materials[10];
            tmp_inventory.GetComponent<Inventory>().addItem(newMaterial);
            Destroy(tmp);

            GameMaster.Player_Gold = GameMaster.Player_Gold - 200;
            updateGold = true;

            GameObject.Find("Txt_Buy").GetComponent<CanvasGroup>().alpha = 1;
            txt_Notice_Buy.text = "You Just Bought " + newMaterial.Item.ItemName;

            StartCoroutine(Disable_Txt_Buy());
        }

        //Save.save_Anything_Player = true;
    }

    public void BuyIron()
    {
        if (GameMaster.Player_Gold >= 450)
        {
            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
            tmp.AddComponent<Items_Script>();
            Items_Script newMaterial = tmp.GetComponent<Items_Script>();
            newMaterial.Item = Inventory_Manager.Instance.ItemContainer.Materials[4];
            tmp_inventory.GetComponent<Inventory>().addItem(newMaterial);
            Destroy(tmp);

            GameMaster.Player_Gold = GameMaster.Player_Gold - 450;
            updateGold = true;

            GameObject.Find("Txt_Buy").GetComponent<CanvasGroup>().alpha = 1;
            txt_Notice_Buy.text = "You Just Bought " + newMaterial.Item.ItemName;

            StartCoroutine(Disable_Txt_Buy());
        }

        //Save.save_Anything_Player = true;
    }

    public void BuyBone()
    {
        if (GameMaster.Player_Gold >= 160)
        {
            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
            tmp.AddComponent<Items_Script>();
            Items_Script newMaterial = tmp.GetComponent<Items_Script>();
            newMaterial.Item = Inventory_Manager.Instance.ItemContainer.Materials[1];
            tmp_inventory.GetComponent<Inventory>().addItem(newMaterial);
            Destroy(tmp);

            GameMaster.Player_Gold = GameMaster.Player_Gold - 160;
            updateGold = true;

            GameObject.Find("Txt_Buy").GetComponent<CanvasGroup>().alpha = 1;
            txt_Notice_Buy.text = "You Just Bought " + newMaterial.Item.ItemName;

            StartCoroutine(Disable_Txt_Buy());
        }

        //Save.save_Anything_Player = true;
    }

    public void BuyGem()
    {
        if (GameMaster.Player_Gold >= 80)
        {
            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
            tmp.AddComponent<Items_Script>();
            Items_Script newMaterial = tmp.GetComponent<Items_Script>();
            newMaterial.Item = Inventory_Manager.Instance.ItemContainer.Materials[6];
            tmp_inventory.GetComponent<Inventory>().addItem(newMaterial);
            Destroy(tmp);

            GameMaster.Player_Gold = GameMaster.Player_Gold - 80;
            updateGold = true;

            GameObject.Find("Txt_Buy").GetComponent<CanvasGroup>().alpha = 1;
            txt_Notice_Buy.text = "You Just Bought " + newMaterial.Item.ItemName;

            StartCoroutine(Disable_Txt_Buy());
        }

        //Save.save_Anything_Player = true;
    }

    public void BuyFang()
    {
        if (GameMaster.Player_Gold >= 180)
        {
            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
            tmp.AddComponent<Items_Script>();
            Items_Script newMaterial = tmp.GetComponent<Items_Script>();
            newMaterial.Item = Inventory_Manager.Instance.ItemContainer.Materials[8];
            tmp_inventory.GetComponent<Inventory>().addItem(newMaterial);
            Destroy(tmp);

            GameMaster.Player_Gold = GameMaster.Player_Gold - 180;
            updateGold = true;

            GameObject.Find("Txt_Buy").GetComponent<CanvasGroup>().alpha = 1;
            txt_Notice_Buy.text = "You Just Bought " + newMaterial.Item.ItemName;

            StartCoroutine(Disable_Txt_Buy());
        }

        //Save.save_Anything_Player = true;
    }

    public void BuyWoodenSword()
    {
        if (GameMaster.Player_Gold >= 600)
        {
            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
            tmp.AddComponent<Items_Script>();
            Items_Script newWeapon = tmp.GetComponent<Items_Script>();
            newWeapon.Item = Inventory_Manager.Instance.ItemContainer.Weapons[3];
            tmp_inventory.GetComponent<Inventory>().addItem(newWeapon);
            Destroy(tmp);

            GameMaster.Player_Gold = GameMaster.Player_Gold - 600;
            updateGold = true;

            GameObject.Find("Txt_Buy").GetComponent<CanvasGroup>().alpha = 1;
            txt_Notice_Buy.text = "You Just Bought " + newWeapon.Item.ItemName;

            StartCoroutine(Disable_Txt_Buy());
        }

        //Save.save_Anything_Player = true;
    }

    public void BuyWoodenShield()
    {
        if (GameMaster.Player_Gold >= 450)
        {
            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
            tmp.AddComponent<Items_Script>();
            Items_Script newShield = tmp.GetComponent<Items_Script>();
            newShield.Item = Inventory_Manager.Instance.ItemContainer.Equipments[6];
            tmp_inventory.GetComponent<Inventory>().addItem(newShield);
            Destroy(tmp);

            GameMaster.Player_Gold = GameMaster.Player_Gold - 450;
            updateGold = true;

            GameObject.Find("Txt_Buy").GetComponent<CanvasGroup>().alpha = 1;
            txt_Notice_Buy.text = "You Just Bought " + newShield.Item.ItemName;

            StartCoroutine(Disable_Txt_Buy());
        }

        //Save.save_Anything_Player = true;
    }

    public void BuyNormalShield()
    {
        if (GameMaster.Player_Gold >= 600)
        {
            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
            tmp.AddComponent<Items_Script>();
            Items_Script newShield = tmp.GetComponent<Items_Script>();
            newShield.Item = Inventory_Manager.Instance.ItemContainer.Equipments[7];
            tmp_inventory.GetComponent<Inventory>().addItem(newShield);
            Destroy(tmp);

            GameMaster.Player_Gold = GameMaster.Player_Gold - 600;
            updateGold = true;

            GameObject.Find("Txt_Buy").GetComponent<CanvasGroup>().alpha = 1;
            txt_Notice_Buy.text = "You Just Bought " + newShield.Item.ItemName;

            StartCoroutine(Disable_Txt_Buy());
        }

        //Save.save_Anything_Player = true;
    }

    public void BuyWoodenArmor()
    {
        if (GameMaster.Player_Gold >= 550)
        {
            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
            tmp.AddComponent<Items_Script>();
            Items_Script newShield = tmp.GetComponent<Items_Script>();
            newShield.Item = Inventory_Manager.Instance.ItemContainer.Equipments[2];
            tmp_inventory.GetComponent<Inventory>().addItem(newShield);
            Destroy(tmp);

            GameMaster.Player_Gold = GameMaster.Player_Gold - 550;
            updateGold = true;

            GameObject.Find("Txt_Buy").GetComponent<CanvasGroup>().alpha = 1;
            txt_Notice_Buy.text = "You Just Bought " + newShield.Item.ItemName;

            StartCoroutine(Disable_Txt_Buy());
        }

        //Save.save_Anything_Player = true;
    }

    public void BuyNormalArmor()
    {
        if (GameMaster.Player_Gold >= 700)
        {
            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
            tmp.AddComponent<Items_Script>();
            Items_Script newShield = tmp.GetComponent<Items_Script>();
            newShield.Item = Inventory_Manager.Instance.ItemContainer.Equipments[4];
            tmp_inventory.GetComponent<Inventory>().addItem(newShield);
            Destroy(tmp);

            GameMaster.Player_Gold = GameMaster.Player_Gold - 700;
            updateGold = true;

            GameObject.Find("Txt_Buy").GetComponent<CanvasGroup>().alpha = 1;
            txt_Notice_Buy.text = "You Just Bought " + newShield.Item.ItemName;

            StartCoroutine(Disable_Txt_Buy());
        }

        //Save.save_Anything_Player = true;
    }

    public void BuySteelArmor()
    {
        if (GameMaster.Player_Gold >= 1000)
        {
            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
            tmp.AddComponent<Items_Script>();
            Items_Script newShield = tmp.GetComponent<Items_Script>();
            newShield.Item = Inventory_Manager.Instance.ItemContainer.Equipments[5];
            tmp_inventory.GetComponent<Inventory>().addItem(newShield);
            Destroy(tmp);

            GameMaster.Player_Gold = GameMaster.Player_Gold - 1000;
            updateGold = true;

            GameObject.Find("Txt_Buy").GetComponent<CanvasGroup>().alpha = 1;
            txt_Notice_Buy.text = "You Just Bought " + newShield.Item.ItemName;

            StartCoroutine(Disable_Txt_Buy());
        }

        //Save.save_Anything_Player = true;
    }

    public void BuyBoneSword()
    {
        if (GameMaster.Player_Gold >= 1200)
        {
            GameObject tmp = Instantiate(Inventory_Manager.Instance.itemObject);
            tmp.AddComponent<Items_Script>();
            Items_Script newWeapon = tmp.GetComponent<Items_Script>();
            newWeapon.Item = Inventory_Manager.Instance.ItemContainer.Weapons[2];
            tmp_inventory.GetComponent<Inventory>().addItem(newWeapon);
            Destroy(tmp);

            GameMaster.Player_Gold = GameMaster.Player_Gold - 1200;
            updateGold = true;

            GameObject.Find("Txt_Buy").GetComponent<CanvasGroup>().alpha = 1;
            txt_Notice_Buy.text = "You Just Bought " + newWeapon.Item.ItemName;

            StartCoroutine(Disable_Txt_Buy());
        }

        //Save.save_Anything_Player = true;
    }

    public void BuySword()
	{
		if (GameMaster.Player_Gold >= 1000) 
		{
			GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);
			tmp.AddComponent<Items_Script> ();
			Items_Script newWeapon = tmp.GetComponent<Items_Script> ();
			newWeapon.Item = Inventory_Manager.Instance.ItemContainer.Weapons [0];
			tmp_inventory.GetComponent<Inventory> ().addItem (newWeapon);
			Destroy (tmp);

			GameMaster.Player_Gold = GameMaster.Player_Gold - 1000;
			updateGold = true;

            GameObject.Find("Txt_Buy").GetComponent<CanvasGroup>().alpha = 1;
            txt_Notice_Buy.text = "You Just Bought " + newWeapon.Item.ItemName;

            StartCoroutine(Disable_Txt_Buy());
		}

        //Save.save_Anything_Player = true;
    }

	public void BuyShield()
	{
		if (GameMaster.Player_Gold >= 500) 
		{
			GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);
			tmp.AddComponent<Items_Script> ();
			Items_Script newShield = tmp.GetComponent<Items_Script> ();
			newShield.Item = Inventory_Manager.Instance.ItemContainer.Equipments [1];
			tmp_inventory.GetComponent<Inventory> ().addItem (newShield);
			Destroy (tmp);

			GameMaster.Player_Gold = GameMaster.Player_Gold - 500;
			updateGold = true;

            GameObject.Find("Txt_Buy").GetComponent<CanvasGroup>().alpha = 1;
            txt_Notice_Buy.text = "You Just Bought " + newShield.Item.ItemName;

            StartCoroutine(Disable_Txt_Buy());
        }

        //Save.save_Anything_Player = true;
    }

	public void BuyArmor()
	{
		if (GameMaster.Player_Gold >= 550) 
		{
			GameObject tmp = Instantiate (Inventory_Manager.Instance.itemObject);
			tmp.AddComponent<Items_Script> ();
			Items_Script newArmor = tmp.GetComponent<Items_Script> ();
			newArmor.Item = Inventory_Manager.Instance.ItemContainer.Equipments [0];
			tmp_inventory.GetComponent<Inventory> ().addItem (newArmor);
			Destroy (tmp);

			GameMaster.Player_Gold = GameMaster.Player_Gold - 550;
			updateGold = true;

            GameObject.Find("Txt_Buy").GetComponent<CanvasGroup>().alpha = 1;
            txt_Notice_Buy.text = "You Just Bought " + newArmor.Item.ItemName;

            StartCoroutine(Disable_Txt_Buy());
        }

        //Save.save_Anything_Player = true;
    }

    IEnumerator Disable_Txt_Buy()
    {
        yield return new WaitForSeconds(2);
        GameObject.Find("Txt_Buy").GetComponent<CanvasGroup>().alpha = 0;
        txt_Notice_Buy.text = "You Just Bought ";
    }

    private void OnTriggerEnter2D(Collider2D other)
	{
        if (other.CompareTag("Player"))
        {
            if (this.name.Equals("House_Vendor_Weapons"))
            {
                GameObject.Find("Canvas_Shop_Items").GetComponent<Canvas>().sortingOrder = -1;
                GameObject.Find("Canvas_Shop_Weapons").GetComponent<Canvas>().sortingOrder = 2;
                GameObject.Find("Canvas_Online_Shop").GetComponent<Canvas>().sortingOrder = -1;
                GameObject.Find("Canvas_Online_Shop_Buy").GetComponent<Canvas>().sortingOrder = -1;
                //GameObject.Find("Canvas_Inventory_Player").GetComponent<Canvas>().sortingOrder = 4;

                GameObject.Find("Canvas_Player_World").GetComponent<CanvasGroup>().alpha = 0;
                
                canvas_Shop.alpha = 1;
                Online_Buy = false;
                GameMaster.Shop_Open = true;
                GameMaster.Online = false;
            }
            else if (this.name.Equals("House_Vendor_Items"))
            {
                GameObject.Find("Canvas_Shop_Weapons").GetComponent<Canvas>().sortingOrder = -1;
                GameObject.Find("Canvas_Shop_Items").GetComponent<Canvas>().sortingOrder = 2;
                GameObject.Find("Canvas_Online_Shop").GetComponent<Canvas>().sortingOrder = -1;
                GameObject.Find("Canvas_Online_Shop_Buy").GetComponent<Canvas>().sortingOrder = -1;

                //GameObject.Find("Canvas_Inventory_Player").GetComponent<Canvas>().sortingOrder = 4;
                
                canvas_Shop.alpha = 1;
                Online_Buy = false;
                GameMaster.Shop_Open = true;
                GameMaster.Online = false;
            }
            else if (this.name.Equals("Online_House"))
            {
                GameObject.Find("Canvas_Shop_Weapons").GetComponent<Canvas>().sortingOrder = -1;
                GameObject.Find("Canvas_Shop_Items").GetComponent<Canvas>().sortingOrder = -1;
                GameObject.Find("Canvas_Online_Shop").GetComponent<Canvas>().sortingOrder = 3;
                GameObject.Find("Canvas_Online_Shop_Buy").GetComponent<Canvas>().sortingOrder = -1;
                GameObject.Find("Canvas_Inventory_Player").GetComponent<Canvas>().sortingOrder = 4;

                if (GameObject.Find("Inventory").GetComponent<CanvasGroup>().alpha == 0)
                {
                    Player.Btn_Player_Inventory = true;
                }

                canvas_Shop.alpha = 1;
                Online_Buy = false;
                GameMaster.Shop_Open = true;
                GameMaster.Online = true;

                Online_Item_1.Place_Online = "Online";
            }
            else if (this.name.Equals("Online_House_Buy"))
            {
                GameObject.Find("Canvas_Shop_Weapons").GetComponent<Canvas>().sortingOrder = -1;
                GameObject.Find("Canvas_Shop_Items").GetComponent<Canvas>().sortingOrder = -1;
                GameObject.Find("Canvas_Online_Shop").GetComponent<Canvas>().sortingOrder = -1;

                GameObject.Find("Canvas_Online_Shop_Buy").GetComponent<Canvas>().sortingOrder = 3;
                GameObject.Find("Canvas_Online_Shop_Buy").GetComponent<CanvasGroup>().alpha = 1;

                GameObject.Find("Canvas_Inventory_Player").GetComponent<Canvas>().sortingOrder = -1;

                Online_Buy = true;
                GameMaster.Shop_Open = true;
                GameMaster.Online = true;

                Online_Item_1.Place_Online = "Online_Sell";
            }
        }
	}

    public void btn_Sell()
    {
        if (GameObject.Find("Inventory").GetComponent<CanvasGroup>().alpha == 0)
        {
            Player.Btn_Player_Inventory = true;
        }

        if (GameObject.Find("Canvas_Shop_Weapons").GetComponent<CanvasGroup>().alpha == 1)
        {
            GameObject.Find("Img_Equip1").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("Img_Equip2").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("Img_Equip3").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("Img_Equip4").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("Img_Equip5").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("Img_Equip6").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("Img_Equip7").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("Img_Equip8").GetComponent<CanvasGroup>().alpha = 0;
        }

        if (GameObject.Find("Canvas_Shop_Items").GetComponent<CanvasGroup>().alpha == 1)
        {
            GameObject.Find("Img_Buy1").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("Img_Buy2").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("Img_Buy3").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("Img_Buy4").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("Img_Buy5").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("Img_Buy6").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("Img_Buy7").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("Img_Buy8").GetComponent<CanvasGroup>().alpha = 0;
        }
        
        GameObject.Find("Canvas_Inventory_Player").GetComponent<Canvas>().sortingOrder = 4;
    }

    public void btn_Buy()
    {
        if (GameObject.Find("Inventory").GetComponent<CanvasGroup>().alpha == 1)
        {
            GameObject.Find("Inventory").GetComponent<CanvasGroup>().alpha = 0;
            Player.Btn_Player_Inventory = false;
        }

        if (GameObject.Find("Canvas_Shop_Weapons").GetComponent<CanvasGroup>().alpha == 1)
        {
            GameObject.Find("Img_Equip1").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("Img_Equip2").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("Img_Equip3").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("Img_Equip4").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("Img_Equip5").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("Img_Equip6").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("Img_Equip7").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("Img_Equip8").GetComponent<CanvasGroup>().alpha = 1;
        }

        if (GameObject.Find("Canvas_Shop_Items").GetComponent<CanvasGroup>().alpha == 1)
        {
            GameObject.Find("Img_Buy1").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("Img_Buy2").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("Img_Buy3").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("Img_Buy4").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("Img_Buy5").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("Img_Buy6").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("Img_Buy7").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("Img_Buy8").GetComponent<CanvasGroup>().alpha = 1;
        }
    }

	private void OnTriggerExit2D(Collider2D other)
	{
        if (other.CompareTag("Player"))
        {
            if (this.name.Equals("House_Vendor_Weapons"))
            {
                GameObject.Find("Canvas_Shop_Weapons").GetComponent<Canvas>().sortingOrder = -1;

                GameObject.Find("Canvas_Inventory_Player").GetComponent<Canvas>().sortingOrder = 4;
                GameMaster.Shop_Open = false;
            }
            else if (this.name.Equals("House_Vendor_Items"))
            {
                GameObject.Find("Canvas_Shop_Items").GetComponent<Canvas>().sortingOrder = -1;

                GameObject.Find("Canvas_Inventory_Player").GetComponent<Canvas>().sortingOrder = 4;
                GameMaster.Shop_Open = false;
            }
            else if (this.name.Equals("Online_House"))
            {
                GameObject.Find("Canvas_Online_Shop").GetComponent<Canvas>().sortingOrder = -1;

                GameObject.Find("Canvas_Inventory_Player").GetComponent<Canvas>().sortingOrder = 4;
                GameMaster.Shop_Open = false;
                Online_Buy = false;
                GameMaster.Online = false;
                Online_Item_1.Place_Online = "";
            }
            else if (this.name.Equals("Online_House_Buy"))
            {
                GameObject.Find("Canvas_Online_Shop_Buy").GetComponent<Canvas>().sortingOrder = -1;
                GameObject.Find("Canvas_Online_Shop_Buy").GetComponent<CanvasGroup>().alpha = 0;

                GameObject.Find("Canvas_Inventory_Player").GetComponent<Canvas>().sortingOrder = 4;

                GameMaster.Shop_Open = false;
                Online_Buy = false;
                GameMaster.Online = false;
                Online_Item_1.Place_Online = "";
            }
        }
	}
}
