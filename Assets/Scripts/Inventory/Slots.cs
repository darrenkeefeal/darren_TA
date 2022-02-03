using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slots : MonoBehaviour, IPointerClickHandler {

	private Image Image_Equip;

	private Stack<Items_Script> items = new Stack<Items_Script>();
	public Stack<Items_Script> Items
	{
		get 
		{
			return items;
		}
		set 
		{
			items = value;
		}
	}

	public Text txtStack; 
	public Sprite slotEmpty;
	public Sprite slotHighlight;

	private CanvasGroup canvasGroup;

	public ItemType canContain;

    GameObject tmp_inventory;

    private bool isClickable = true;
	public bool IsClickable
	{
		get { return isClickable; }
		set { isClickable = value; }
	}

	public bool isEmpty
	{
		get
		{
			return items.Count == 0;
		}
	}

	public bool IsAvailable
	{
		get 
		{
			return CurrentItem.Item.MaxSize > items.Count;
		}
	}

    public int isCount
    {
        get
        {
            return items.Count;
        }
    }

	public Items_Script CurrentItem
	{
		get
		{ 
			return items.Peek ();
		}
	}

    public static bool Use_Item_Ok;

	// Use this for initialization
	void Start () {

        Use_Item_Ok = false;

        items = new Stack<Items_Script> ();
		RectTransform RectSlot = GetComponent<RectTransform> ();
		RectTransform RectTxt = txtStack.GetComponent<RectTransform> ();

		//Supaya Jadi Int (Mengubah Size Txt nya Berapa % Dari Rectslot)
		double percent = RectSlot.sizeDelta.x * 0.60;
		int txtScale = (int)percent;
		txtStack.resizeTextMaxSize = txtScale;
		txtStack.resizeTextMinSize = txtScale;

		RectTxt.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, RectSlot.sizeDelta.x);
		RectTxt.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, RectSlot.sizeDelta.y);

		if (transform.parent != null) 
		{
			Transform p = transform.parent;

			while(canvasGroup == null && p != null)
			{
				canvasGroup = p.GetComponent<CanvasGroup> ();
				p = p.parent;
			}
		}
	}

    bool Ok_Use;
    int ctr_Finger;

    // Update is called once per frame
    void Update ()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (!isEmpty && transform.parent.GetComponent<Inventory>().IsOpen && GameObject.Find("Crafting").GetComponent<CanvasGroup>().alpha == 1)
            {
                if (Items.Count > 1)
                {
                    Vector2 pos;
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(Inventory_Manager.Instance.canvas.transform as RectTransform, Input.mousePosition, Inventory_Manager.Instance.canvas.worldCamera, out pos);
                    Inventory_Manager.Instance.selectSize.SetActive(true);
                    //Inventory_Manager.Instance.selectSize.transform.position = Inventory_Manager.Instance.canvas.transform.TransformPoint(pos) + new Vector3(-150, 10, 0);

                    Inventory_Manager.Instance.selectSize.transform.position = GameObject.Find("Pos_Stack").GetComponent<RectTransform>().transform.position;

                    Inventory_Manager.Instance.SetStackInfo(items.Count);
                }
            }
        }
    }

	public void AddItem(Items_Script item)
	{
		if (isEmpty) 
		{
			transform.parent.GetComponent<Inventory> ().EmptySlots--;
		}

		items.Push (item);

		if (items.Count > 1) 
		{
			txtStack.text = items.Count.ToString ();
		}

		ChangeSprite (item.spriteNeutral, item.spriteHighlight);
	}

	public void AddItems(Stack<Items_Script> items)
	{
		this.items = new Stack<Items_Script> (items);

		if (items.Count > 1) 
		{
			txtStack.text = items.Count.ToString ();
		} 
		else 
		{
			txtStack.text = string.Empty;
		}

		ChangeSprite (CurrentItem.spriteNeutral, CurrentItem.spriteHighlight);
	}

	//Untuk Mengganti Sprite ketika Di Hover
	private void ChangeSprite(Sprite Neutral , Sprite HighLight)
	{
		GetComponent<Image> ().sprite = Neutral;

		SpriteState st = new SpriteState ();
		st.highlightedSprite = HighLight;
		st.pressedSprite = Neutral;

		GetComponent<Button> ().spriteState = st;
	}

	public void UseItem()
	{
		if (!isEmpty && isClickable) 
		{
			items.Peek ().use (this);
			if (items.Count > 1) 
			{
				txtStack.text = items.Count.ToString ();
			} 
			else 
			{
				txtStack.text = string.Empty;
			}

			if (isEmpty) 
			{
				ChangeSprite (slotEmpty, slotHighlight);
				transform.parent.GetComponent<Inventory> ().EmptySlots++;
			}
		}
	}

	public Stack<Items_Script> RemoveItems(int itemCount)
	{
		Stack<Items_Script> tmp = new Stack<Items_Script>();
		for (int i = 0; i < itemCount; i++) {
			tmp.Push (items.Pop ());
		}

		if (items.Count > 1) 
		{
			txtStack.text = items.Count.ToString ();
		} 
		else 
		{
			txtStack.text = string.Empty;
		}

		return tmp;
	}

	//Supaya Tidak Lebih itemnya
	public Items_Script RemoveItem()
	{
		if (!isEmpty) 
		{
			Items_Script tmp;
			tmp = items.Pop ();

			if (items.Count > 1) 
			{
				txtStack.text = items.Count.ToString ();
			} 
			else 
			{
				txtStack.text = string.Empty;
			}

			if (isEmpty) 
			{
				ClearSlot ();
			}

			return tmp;
		}

		return null;
	}

	public void ClearSlot()
	{
		items.Clear ();
		ChangeSprite (slotEmpty, slotHighlight);
		txtStack.text = string.Empty;

		if (transform.parent != null) 
		{
			transform.parent.GetComponent<Inventory> ().EmptySlots++;
		}
	}

	public static void SwapItems(Slots dari , Slots ke)
	{
		ItemType movingType = dari.CurrentItem.Item.ItemType;

		if (ke != null && dari != null) 
		{
			bool calStat = dari.transform.parent == Player_Panel.Instance.transform || ke.transform.parent == Player_Panel.Instance.transform;

			if (canSwap(dari,ke)) 
			{
				Stack<Items_Script> tmpKe = new Stack<Items_Script> (ke.Items);
				ke.AddItems (dari.Items);

				if (tmpKe.Count == 0) 
				{
					ke.transform.parent.GetComponent<Inventory> ().EmptySlots--;
					dari.ClearSlot ();
				}
				else 
				{
					dari.AddItems (tmpKe);
				}
			}

			if (calStat) 
			{
				Player_Panel.Instance.CalStats ();
			}
		}

	}

	private static bool canSwap(Slots dari , Slots ke)
	{
		ItemType dariType = dari.CurrentItem.Item.ItemType;

		if (ke.canContain == dari.canContain) 
		{
			return true;
		}
		if (ke.canContain == dariType)
		{
			return true;
		}
		if (ke.canContain == ItemType.Free && (ke.isEmpty || ke.CurrentItem.Item.ItemType == dariType)) 
		{
			return true;
		}

		return false;
	}

    int ctrCount;
    float countTimer;

    public static bool Temp_Use;

	public void OnPointerClick(PointerEventData eventData)
	{
        /*
        if (canvasGroup.alpha > 0 && canvasGroup != null)
        {
            if (GameMaster.Shop_Open || GameMaster.Online)
            {
                
            }
            else
            {
                UseItem();
                Inventory_Manager.Instance.Dari.GetComponent<Image>().color = Color.white;
                Inventory_Manager.Instance.Dari = null;
                Inventory_Manager.Instance.Sampai = null;

                Destroy(GameObject.Find("Hover"));
            }
        }
        */

        if (eventData.button == PointerEventData.InputButton.Right && !GameObject.Find("Hover") && canvasGroup.alpha > 0 && canvasGroup != null)
        {
            UseItem();
        }
        else if (eventData.button == PointerEventData.InputButton.Left && Input.GetKey(KeyCode.LeftShift) && !isEmpty && !GameObject.Find("Hover") && transform.parent.GetComponent<Inventory>().IsOpen)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(Inventory_Manager.Instance.canvas.transform as RectTransform, Input.mousePosition, Inventory_Manager.Instance.canvas.worldCamera, out pos);
            Inventory_Manager.Instance.selectSize.SetActive(true);
            //Inventory_Manager.Instance.selectSize.transform.position = Inventory_Manager.Instance.canvas.transform.TransformPoint(pos) + new Vector3(-150, 10,0);

            Inventory_Manager.Instance.selectSize.transform.position = GameObject.Find("Pos_Stack").GetComponent<RectTransform>().transform.position;

            Inventory_Manager.Instance.SetStackInfo(items.Count);
        }
        else if (!isEmpty && transform.parent.GetComponent<Inventory>().IsOpen && GameObject.Find("Canvas_Crafting_Temp").GetComponent<CanvasGroup>().alpha == 1)
        {
            if (Items.Count > 1)
            {
                Vector2 pos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(Inventory_Manager.Instance.canvas.transform as RectTransform, Input.mousePosition, Inventory_Manager.Instance.canvas.worldCamera, out pos);
                Inventory_Manager.Instance.selectSize.SetActive(true);
                //Inventory_Manager.Instance.selectSize.transform.position = Inventory_Manager.Instance.canvas.transform.TransformPoint(pos) + new Vector3(-150, 10, 0);

                Inventory_Manager.Instance.selectSize.transform.position = GameObject.Find("Pos_Stack").GetComponent<RectTransform>().transform.position;

                Inventory_Manager.Instance.SetStackInfo(items.Count);
            }
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            /*
            if (!isEmpty && transform.parent.GetComponent<Inventory>().IsOpen && GameObject.Find("Canvas_Shop_Items").GetComponent<CanvasGroup>().alpha == 1)
            {

            }
            else if (!isEmpty && transform.parent.GetComponent<Inventory>().IsOpen && GameObject.Find("Canvas_Shop_Weapons").GetComponent<CanvasGroup>().alpha == 1)
            {

            }
            else if (!isEmpty && transform.parent.GetComponent<Inventory>().IsOpen && GameObject.Find("Canvas_Online_Shop").GetComponent<CanvasGroup>().alpha == 1)
            {

            }
            else if (canvasGroup.alpha > 0 && canvasGroup != null)
            {
                UseItem();
                Inventory_Manager.Instance.Dari.GetComponent<Image>().color = Color.white;
                Inventory_Manager.Instance.Dari = null;
                Inventory_Manager.Instance.Sampai = null;

                Destroy(GameObject.Find("Hover"));
            }
            */
            if (canvasGroup.alpha > 0 && canvasGroup != null)
            {
                if (GameMaster.Shop_Open || GameMaster.Online)
                {

                }
                else
                {
                    UseItem();
                    Inventory_Manager.Instance.Dari.GetComponent<Image>().color = Color.white;
                    Inventory_Manager.Instance.Dari = null;
                    Inventory_Manager.Instance.Sampai = null;

                    Destroy(GameObject.Find("Hover"));
                }
            }
        }
    }
	
}
