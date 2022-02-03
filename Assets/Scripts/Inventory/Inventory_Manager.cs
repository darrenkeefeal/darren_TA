using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

public class Inventory_Manager : MonoBehaviour {

	public GameObject itemObject;

	private static Inventory_Manager instance;
	public static Inventory_Manager Instance
	{
		get
		{ 
			if (instance == null) 
			{
				instance = FindObjectOfType<Inventory_Manager> ();
			}
			return instance;
		}
	}

	public GameObject slotPrefab;
	public GameObject iconPrefab;

	private GameObject hoverObject;
	public GameObject HoverObject
	{
		get { return hoverObject; }
		set { hoverObject = value; }
	}

	public GameObject drop_Item;

	public GameObject toolObject;
	public Text txt_Tips_Visible_Object;
	public Text txt_Tips_Object;

	public Canvas canvas;

	private Slots dari, sampai;
	public Slots Dari
	{
		get { return dari; }
		set { dari = value; }
	}
	public Slots Sampai
	{
		get { return sampai; }
		set { sampai = value; }
	}

	private GameObject clicked;
	public GameObject Clicked
	{
		get { return clicked; }
		set { clicked = value; }
	}

	public GameObject selectSize;
	public Text txtSize;

	private int splitSize;
	public int SplitSize
	{
		get { return splitSize; }
		set { splitSize = value; }
	}
	private int maxSize;
	public int MaxSize
	{
		get { return maxSize; }
		set { maxSize = value; }
	}

	private Slots movingSlot;
	public Slots MovingSlot
	{
		get { return movingSlot; }
		set { movingSlot = value; }
	}

	public EventSystem eventSystem;

	private Item_Container itemContainer = new Item_Container();
	public Item_Container ItemContainer
	{
		get { return itemContainer; }
		set { itemContainer = value; }
	}

	public void Awake()
	{
		DontDestroyOnLoad (this.gameObject);

		if (FindObjectsOfType(GetType()).Length > 1) 
		{
			Destroy (gameObject);
		}

		if (!GameMaster.firstLoad)
		{
            Type[] itemTypes = { typeof(Equipment_Master), typeof(Weapon), typeof(Consumable), typeof(Materials) };

            if (Application.platform == RuntimePlatform.Android)
            {
                string streamingAssetsPath = Application.streamingAssetsPath;
                string realPath = System.IO.Path.Combine(streamingAssetsPath, "Items.xml");

                WWW reader = new WWW(realPath);

                while (!reader.isDone)
                {
                    //Wait Reader
                }
                
                XmlSerializer serializer_Android = new XmlSerializer(typeof(Item_Container), itemTypes);
                MemoryStream stream = new MemoryStream(reader.bytes);

                try
                {
                    if (stream != null)
                    {
                        itemContainer = (Item_Container)serializer_Android.Deserialize(stream);
                        itemContainer.Weapons.ForEach(x => x.genId());
                        itemContainer.Consumables.ForEach(x => x.genId());
                        itemContainer.Equipments.ForEach(x => x.genId());
                        itemContainer.Materials.ForEach(x => x.genId());
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }

                stream.Close();
            }

            else
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Item_Container), itemTypes);
                TextReader textReader = new StreamReader(Application.streamingAssetsPath + "/Items.xml");
                itemContainer = (Item_Container)serializer.Deserialize(textReader);
                itemContainer.Weapons.ForEach(x => x.genId());
                itemContainer.Consumables.ForEach(x => x.genId());
                itemContainer.Equipments.ForEach(x => x.genId());
                itemContainer.Materials.ForEach(x => x.genId());

                textReader.Close();
            }

			Crafting.Instance.CreateCrafting ();

			GameMaster.firstLoad = true;
		}
	}

	public void SetStackInfo(int maxSize)
	{
		selectSize.SetActive (true);
		toolObject.SetActive (false);
		splitSize = 0;
		this.maxSize = maxSize;
		txtSize.text = splitSize.ToString ();
	}
}
