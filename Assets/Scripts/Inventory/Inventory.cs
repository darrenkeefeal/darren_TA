using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour {

	public static bool isMouse = false;

	protected static GameObject Player_Ref;

	protected RectTransform inventoryRect;
	protected float inventoryWidth, inventoryHeight;
	protected List<GameObject> allSlots;

	public int slots;
	public int rows;
	public float slotPaddingLeft, slotPaddingTop;
	public float slotSize;

	private RectTransform equipmentRect;

	public CanvasGroup canvasGroup_Inventory;

	private bool fadeIn;
	private bool fadeOut;

	public float fadeTime;

	private int emptySlots;

	public  int EmptySlots
	{
		get
		{ 
			return emptySlots;
		}
		set
		{ 
			emptySlots = value;
		}
	}

	private bool isOpen;
	public bool IsOpen
	{
		get { return isOpen; }
		set  { isOpen = value; }
	}

	private bool Sell_Item;
	public static int ctrSell;

	public Slots[] all_Items;

	void Awake()
	{
		all_Items = GameObject.Find("Inventory").GetComponentsInChildren<Slots> ();
	}

	// Use this for initialization
	protected virtual void Start () {
		isOpen = false;

		Player_Ref = GameObject.Find ("Player");

		canvasGroup_Inventory = GetComponent<CanvasGroup>();

		CreateLayout ();

		Inventory_Manager.Instance.MovingSlot = GameObject.Find ("MovingSlot").GetComponent<Slots> ();

		//Inventory_Manager.Instance.MovingSlot = GameObject.Find ("MovingSlot").GetComponent<Slots> ();
	}

	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0))
        {
            if (GameObject.Find("Canvas_Crafting_Temp").GetComponent<CanvasGroup>().alpha == 0)
            {
                if (!isMouse && Inventory_Manager.Instance.Dari != null && GameMaster.Shop_Open)
                {
                    Inventory_Manager.Instance.Dari.GetComponent<Image>().color = Color.white;

                    foreach (Items_Script item in Inventory_Manager.Instance.Dari.Items)
                    {
                        float anglex = Random.Range(-1, 2);
                        float angley = Random.Range(-1, 2);

                        Vector3 v = new Vector3(anglex, angley, 0);

                        GameObject tmpDrop = GameObject.Instantiate(Inventory_Manager.Instance.drop_Item, Player_Ref.transform.position + v, Quaternion.identity);
                        tmpDrop.AddComponent<Items_Script>();
                        tmpDrop.GetComponent<Items_Script>().Item = item.Item;

                        if (tmpDrop.GetComponent<Items_Script>().Item.ItemType == ItemType.Consumables)
                        {
                            Button_Yes_No_Sell.yes_no_Sell = true;
                            Button_Yes_No_Sell.sell_Item_Name = tmpDrop.GetComponent<Items_Script>().Item.ItemName;
                            Button_Yes_No_Sell.sell_Item_Type = "Consumable";
                            ctrSell++;
                        }
                        else if (tmpDrop.GetComponent<Items_Script>().Item.ItemType == ItemType.Material)
                        {
                            Button_Yes_No_Sell.yes_no_Sell = true;
                            Button_Yes_No_Sell.sell_Item_Name = tmpDrop.GetComponent<Items_Script>().Item.ItemName;
                            Button_Yes_No_Sell.sell_Item_Type = "Material";
                            ctrSell++;
                        }
                        else if (tmpDrop.GetComponent<Items_Script>().Item.ItemType == ItemType.Armor)
                        {
                            Button_Yes_No_Sell.yes_no_Sell = true;
                            Button_Yes_No_Sell.sell_Item_Name = tmpDrop.GetComponent<Items_Script>().Item.ItemName;
                            Button_Yes_No_Sell.sell_Item_Type = "Armor";
                        }
                        else if (tmpDrop.GetComponent<Items_Script>().Item.ItemType == ItemType.Shield)
                        {
                            Button_Yes_No_Sell.yes_no_Sell = true;
                            Button_Yes_No_Sell.sell_Item_Name = tmpDrop.GetComponent<Items_Script>().Item.ItemName;
                            Button_Yes_No_Sell.sell_Item_Type = "Shield";
                        }
                        else if (tmpDrop.GetComponent<Items_Script>().Item.ItemType == ItemType.Sword)
                        {
                            Button_Yes_No_Sell.yes_no_Sell = true;
                            Button_Yes_No_Sell.sell_Item_Name = tmpDrop.GetComponent<Items_Script>().Item.ItemName;
                            Button_Yes_No_Sell.sell_Item_Type = "Sword";
                        }
                        Destroy(tmpDrop);
                    }

                    Inventory_Manager.Instance.Dari.ClearSlot();
                    Destroy(GameObject.Find("Hover"));

                    emptySlots++;
                    Inventory_Manager.Instance.Dari = null;
                    Inventory_Manager.Instance.Sampai = null;

                }
                else if (!isMouse && Inventory_Manager.Instance.Dari != null)
                {
                    Inventory_Manager.Instance.Dari.GetComponent<Image>().color = Color.white;

                    foreach (Items_Script item in Inventory_Manager.Instance.Dari.Items)
                    {
                        float anglex = Random.Range(-1, 2);
                        float angley = Random.Range(-1, 2);

                        Vector3 v = new Vector3(anglex, angley, 0);

                        GameObject tmpDrop = GameObject.Instantiate(Inventory_Manager.Instance.drop_Item, Player_Ref.transform.position + v, Quaternion.identity);
                        tmpDrop.AddComponent<Items_Script>();
                        tmpDrop.GetComponent<Items_Script>().Item = item.Item;
                    }

                    Inventory_Manager.Instance.Dari.ClearSlot();
                    Destroy(GameObject.Find("Hover"));

                    emptySlots++;
                    Inventory_Manager.Instance.Dari = null;
                    Inventory_Manager.Instance.Sampai = null;
                }
                else if (!Inventory_Manager.Instance.eventSystem.IsPointerOverGameObject(-1) && !Inventory_Manager.Instance.MovingSlot.isEmpty)
                {
                    foreach (Items_Script item in Inventory_Manager.Instance.MovingSlot.Items)
                    {
                        float anglex = Random.Range(-1, 2);
                        float angley = Random.Range(-1, 2);

                        Vector3 v = new Vector3(anglex, angley, 0);

                        GameObject tmpDrop = GameObject.Instantiate(Inventory_Manager.Instance.drop_Item, Player_Ref.transform.position + v, Quaternion.identity);
                        tmpDrop.AddComponent<Items_Script>();
                        tmpDrop.GetComponent<Items_Script>().Item = item.Item;
                    }

                    Inventory_Manager.Instance.MovingSlot.ClearSlot();
                    Destroy(GameObject.Find("Hover"));
                }
            }
        }
		//Pengecekan Agar Tidak Semua Dapat Di Hover
		if (Inventory_Manager.Instance.HoverObject != null) 
		{
			Vector2 position;
			RectTransformUtility.ScreenPointToLocalPointInRectangle (Inventory_Manager.Instance.canvas.transform as RectTransform, Input.mousePosition, Inventory_Manager.Instance.canvas.worldCamera, out position);
			position.Set (position.x + 20, position.y - 20);
			Inventory_Manager.Instance.HoverObject.transform.position = Inventory_Manager.Instance.canvas.transform.TransformPoint (position);
		}
	}
    
	public void PointerExit()
	{
		isMouse = false;
	}

	public void PointerEnter()
	{
		if (canvasGroup_Inventory.alpha > 0) 
		{
			isMouse = true;
		}
	}

	public virtual void open()
	{
		if (!canvasGroup_Inventory) 
		{
			canvasGroup_Inventory = GetComponentInParent<CanvasGroup> ();
		}

		if (canvasGroup_Inventory.alpha > 0) 
		{
			isOpen = false;
			StartCoroutine ("FadeOut");
			PutItemBack ();
			HideToolTip ();
		} 
		else
		{
			isOpen = true;
			StartCoroutine ("FadeIn");
		}
	}

	public void ShowToolTip(GameObject slot)
	{
		Slots tmpSlot = slot.GetComponent<Slots> ();
		if (slot.GetComponentInParent<Inventory> ().isOpen && !tmpSlot.isEmpty && Inventory_Manager.Instance.HoverObject == null && !Inventory_Manager.Instance.selectSize.activeSelf) 
		{
			Inventory_Manager.Instance.txt_Tips_Visible_Object.text = tmpSlot.CurrentItem.GetToolTip ();
			Inventory_Manager.Instance.txt_Tips_Object.text = Inventory_Manager.Instance.txt_Tips_Visible_Object.text;

			Inventory_Manager.Instance.toolObject.SetActive (true);
			float xPos = slot.transform.position.x - 300;
			float yPos = slot.transform.position.y - slot.GetComponent<RectTransform> ().sizeDelta.y - 20;

            //Inventory_Manager.Instance.toolObject.transform.position = new Vector2 (xPos, yPos);
            Inventory_Manager.Instance.toolObject.transform.position = GameObject.Find("Pos_Stack").GetComponent<RectTransform>().transform.position;
        }
	}

	public void HideToolTip()
	{
		Inventory_Manager.Instance.toolObject.SetActive (false);
	}

	private void PutItemBack()
	{
		if (Inventory_Manager.Instance.Dari != null) 
		{
			Inventory_Manager.Instance.Dari.GetComponent<Image> ().color = Color.white;
			Destroy (GameObject.Find ("Hover"));
			Inventory_Manager.Instance.Dari = null;
		}
	}

	public void SetStackInfo(int maxSize)
	{
		Inventory_Manager.Instance.selectSize.SetActive (true);
		Inventory_Manager.Instance.toolObject.SetActive (false);
		Inventory_Manager.Instance.SplitSize = 0;
		Inventory_Manager.Instance.MaxSize = maxSize;
		Inventory_Manager.Instance.txtSize.text = Inventory_Manager.Instance.SplitSize.ToString ();
	}

	public virtual void CreateLayout()
	{
		allSlots = new List<GameObject> ();
		inventoryRect = GetComponent<RectTransform> ();
		emptySlots = slots;

		//Rumus Agar Mencetak UI Inventory Background
		if (slots > 0 && rows > 0 && slotSize > 0 && slotPaddingLeft > 0 && slotPaddingTop > 0)
		{
			inventoryWidth = (slots / rows) * (slotSize + slotPaddingLeft) + slotPaddingLeft;
			inventoryHeight = rows * (slotSize + slotPaddingTop) + slotPaddingTop;

			//Untuk Mengubah Width dan Height Background
			inventoryRect.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, inventoryWidth + 10);
			inventoryRect.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, inventoryHeight + 10);

			int kolom = slots / rows;

			for (int i = 0; i < rows; i++) {
				for (int j = 0; j < kolom; j++) {
					//Game Object dipanggil untuk nantinya dapat diakses
					GameObject newSlot = (GameObject)Instantiate (Inventory_Manager.Instance.slotPrefab);

					//Mengambil seluruh RectTransform childnya
					RectTransform rectSlot = newSlot.GetComponent<RectTransform> ();

					newSlot.name = "Slot";

					//Melakukan Set Parent ke Canvas
					newSlot.transform.SetParent (this.transform);

					//Rumus Untuk Mencetak Slot di dalamnya
					rectSlot.localPosition = new Vector3 (slotPaddingLeft * (j + 1) + (slotSize * j), -slotPaddingTop * (i + 1) - (slotSize * i));
					rectSlot.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, slotSize * Inventory_Manager.Instance.canvas.scaleFactor);
					rectSlot.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, slotSize * Inventory_Manager.Instance.canvas.scaleFactor);
					allSlots.Add (newSlot);

					newSlot.GetComponent<Button> ().onClick.AddListener
					(
						delegate {
							MoveItem (newSlot);
						}
					);
				}
			}
		}
	}

	//Pengecekan Bila Ada Item yang Sama Yang akan di stack
	public bool addItem(Items_Script Item)
	{
        if (Item.Item.MaxSize == 1)
        { 
			PlaceEmpty (Item);
            return true;
		} 
		else 
		{
			foreach (GameObject slot in allSlots) 
			{
				Slots tmp = slot.GetComponent<Slots> ();

				if (!tmp.isEmpty) 
				{
					//Pengecekan Jika Item yang ingin di Stack Sama Atau Tidak
					if (tmp.CurrentItem.Item.ItemName == Item.Item.ItemName && tmp.IsAvailable) {
						tmp.AddItem (Item);
						//emptySlot--
						return true;
					}
				}
			}
			if (emptySlots > 0) 
			{
				return PlaceEmpty (Item);
			}
		}
		return false;
	}

	//Pengecekan Bila Ada Tempat Kosong Di Inventory
	private bool PlaceEmpty(Items_Script Item)
	{
		foreach (GameObject slot in allSlots) 
		{
			Slots tmp = slot.GetComponent<Slots> ();
			if (tmp.isEmpty) 
			{
				tmp.AddItem (Item);
				return true;
			}
		}
		return false;
	}

	public virtual void MoveItem(GameObject clicked)
	{
		if (isOpen) 
		{
			CanvasGroup cg = clicked.transform.parent.GetComponent<CanvasGroup> ();

			if (cg != null && cg.alpha > 0 || clicked.transform.parent.parent.GetComponent<CanvasGroup>().alpha > 0) 
			{
				Inventory_Manager.Instance.Clicked = clicked;

				if (!Inventory_Manager.Instance.MovingSlot.isEmpty) 
				{
                    Slots tmp = clicked.GetComponent<Slots> ();

					if (tmp.isEmpty)
                    {
                        Debug.Log("A");
						tmp.AddItems (Inventory_Manager.Instance.MovingSlot.Items);
						Inventory_Manager.Instance.MovingSlot.Items.Clear ();
						Destroy (GameObject.Find ("Hover"));
					} 
					else if (!tmp.isEmpty && Inventory_Manager.Instance.MovingSlot.CurrentItem.Item.ItemType == tmp.CurrentItem.Item.ItemType && tmp.IsAvailable) 
					{
                        Debug.Log("B");
                        MergeStack (Inventory_Manager.Instance.MovingSlot, tmp);
					}
				}
				else if (Inventory_Manager.Instance.Dari == null && clicked.transform.parent.GetComponent<Inventory> ().isOpen && !Input.GetKey (KeyCode.LeftShift)) 
				{
					if (!clicked.GetComponent<Slots> ().isEmpty && !GameObject.Find ("Hover")) 
					{
                        Debug.Log("C");
                        Inventory_Manager.Instance.Dari = clicked.GetComponent<Slots> ();
						Inventory_Manager.Instance.Dari.GetComponent<Image> ().color = Color.gray;

						HoverIcon ();
					}
				} 
				else if (Inventory_Manager.Instance.Sampai == null && !Input.GetKey(KeyCode.LeftShift)) 
				{
					if (!clicked.transform.parent.GetComponent<Inventory> ().isOpen)
					{
						if (!clicked.GetComponent<Slots> ().isEmpty && !GameObject.Find ("Hover")) 
						{
                            Debug.Log("D");
                            Inventory_Manager.Instance.Dari = clicked.GetComponent<Slots> ();
							Inventory_Manager.Instance.Dari.GetComponent<Image> ().color = Color.gray;

							HoverIcon ();
						} 
						else 
						{
                            Debug.Log("E");
                            Inventory_Manager.Instance.Sampai = clicked.GetComponent<Slots> ();
							Destroy (GameObject.Find ("Hover"));
						}
					}
					else
					{
                        Debug.Log("F");
                        Inventory_Manager.Instance.Sampai = clicked.GetComponent<Slots> ();
						Destroy (GameObject.Find ("Hover"));
					}
				}

				if (Inventory_Manager.Instance.Sampai != null && Inventory_Manager.Instance.Dari != null) 
				{
					if (!Inventory_Manager.Instance.Sampai.isEmpty && Inventory_Manager.Instance.Dari.CurrentItem.Item.ItemName == Inventory_Manager.Instance.Sampai.CurrentItem.Item.ItemName && Inventory_Manager.Instance.Sampai.IsAvailable) 
					{
                        Debug.Log("G");
                        MergeStack (Inventory_Manager.Instance.Dari, Inventory_Manager.Instance.Sampai);
					} 
					else 
					{
                        Debug.Log("H");
                        Slots.SwapItems (Inventory_Manager.Instance.Dari, Inventory_Manager.Instance.Sampai);
					}

					Inventory_Manager.Instance.Dari.GetComponent<Image> ().color = Color.white;
					Inventory_Manager.Instance.Dari = null;
					Inventory_Manager.Instance.Sampai = null;

					Destroy (GameObject.Find ("Hover"));
				}
			}

			if (Crafting.Instance.isOpen) 
			{
				Crafting.Instance.UpdatePreview ();
			}
		}
	}

	private void HoverIcon()
	{
        if (GameObject.Find("Canvas_Crafting_Temp").GetComponent<CanvasGroup>().alpha == 0)
        {
            Inventory_Manager.Instance.HoverObject = (GameObject)Instantiate(Inventory_Manager.Instance.iconPrefab);
            Inventory_Manager.Instance.HoverObject.GetComponent<Image>().sprite = Inventory_Manager.Instance.Clicked.GetComponent<Image>().sprite;
            Inventory_Manager.Instance.HoverObject.name = "Hover";

            RectTransform hoverTransform = Inventory_Manager.Instance.HoverObject.GetComponent<RectTransform>();
            RectTransform clickTransform = Inventory_Manager.Instance.Clicked.GetComponent<RectTransform>();

            hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, clickTransform.sizeDelta.x - 15);
            hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, clickTransform.sizeDelta.y - 15);

            Inventory_Manager.Instance.HoverObject.transform.SetParent(GameObject.Find("Canvas_Inventory_Player").transform, true);
            Inventory_Manager.Instance.HoverObject.transform.localScale = Inventory_Manager.Instance.Clicked.gameObject.transform.localScale;

            Inventory_Manager.Instance.HoverObject.transform.GetChild(0).GetComponent<Text>().text = Inventory_Manager.Instance.MovingSlot.Items.Count > 1 ? Inventory_Manager.Instance.MovingSlot.Items.Count.ToString() : string.Empty;
        }
    }

	public void SplitStack()
	{
		Inventory_Manager.Instance.selectSize.SetActive (false);
		if (Inventory_Manager.Instance.SplitSize == Inventory_Manager.Instance.MaxSize) 
		{
			MoveItem (Inventory_Manager.Instance.Clicked);
		}
		else if(Inventory_Manager.Instance.SplitSize > 0)
		{
			Inventory_Manager.Instance.MovingSlot.Items = Inventory_Manager.Instance.Clicked.GetComponent<Slots> ().RemoveItems (Inventory_Manager.Instance.SplitSize);
			HoverIcon ();
		}
        Inventory_Manager.Instance.Dari.GetComponent<Image>().color = Color.white;
        Inventory_Manager.Instance.Dari = null;
        Inventory_Manager.Instance.Sampai = null;

        Destroy(GameObject.Find("Hover"));
    }

	public void ChangeText(int i)
	{
		Inventory_Manager.Instance.SplitSize = Inventory_Manager.Instance.SplitSize + i;

		if (Inventory_Manager.Instance.SplitSize < 0) 
		{
			Inventory_Manager.Instance.SplitSize = 0;
		} 
		if (Inventory_Manager.Instance.SplitSize > Inventory_Manager.Instance.MaxSize) 
		{
			Inventory_Manager.Instance.SplitSize = Inventory_Manager.Instance.MaxSize;
		}

		Inventory_Manager.Instance.txtSize.text = Inventory_Manager.Instance.SplitSize.ToString (); 
	}

	public void MergeStack(Slots source, Slots destination)
	{
		int max = destination.CurrentItem.Item.MaxSize - destination.Items.Count;
		int count = source.Items.Count < max ? source.Items.Count : max;

		for (int i = 0; i < count; i++) 
		{
			destination.AddItem (source.RemoveItem ());
		}

		if (source.Items.Count == 0) 
		{
			source.ClearSlot ();
			Destroy (GameObject.Find ("Hover"));
		}
	}

	//Fadein Fadeout Inventory
	protected virtual IEnumerator FadeOut()
	{
		if (!fadeOut) 
		{
			fadeOut = true;
			fadeIn = false;
			StopCoroutine ("FadeIn");

			float startAlpha = canvasGroup_Inventory.alpha;
			float rate = 1.0f / fadeTime;
			float progress = 0.0f;

			while (progress < 1.0) {
				canvasGroup_Inventory.alpha = Mathf.Lerp (startAlpha, 0, progress);
				progress += rate * Time.deltaTime;
				yield return null;
			}

			canvasGroup_Inventory.alpha = 0;
			fadeOut = false;
		}
	}

	private IEnumerator FadeIn()
	{
		if (!fadeIn) 
		{
			fadeOut = false;
			fadeIn = true;
			StopCoroutine ("FadeOut");

			float startAlpha = canvasGroup_Inventory.alpha;
			float rate = 1.0f / fadeTime;
			float progress = 0.0f;

			while (progress < 1.0) {
				canvasGroup_Inventory.alpha = Mathf.Lerp (startAlpha, 1, progress);
				progress += rate * Time.deltaTime;
				yield return null;
			}

			canvasGroup_Inventory.alpha = 1;
			fadeIn = false;
		}
	}
}
