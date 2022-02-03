using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Txt_Damage : MonoBehaviour {

	public Text txt_Damage_Object;
	public static string txt_Damage;

	private Transform pos;

	// Use this for initialization
	void Start () 
	{
		txt_Damage_Object.text = Player.Damage_In.ToString ();

		this.transform.SetParent (GameObject.Find ("Canvas_Inventory_Player").GetComponent<Transform> ().transform, false);
		pos = this.GetComponentInParent<RectTransform> ();
		this.transform.localPosition = new Vector2 (pos.localPosition.x, pos.localPosition.y + 50);
	}
	
	// Update is called once per frame
	void Update () 
	{
		pos.localPosition = new Vector2(pos.localPosition.x , pos.localPosition.y + 1.5f);
		if (GameObject.FindGameObjectsWithTag("Txt_Damage").Length > 1) 
		{
			Destroy (GameObject.FindGameObjectWithTag("Txt_Damage"));
		}
	}
}
