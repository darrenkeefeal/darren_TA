using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXP_Stats : MonoBehaviour {

	private Image content_EXP;
	private float currentFill_EXP;
	private float currentValue_EXP;
	public float MaxExp{ get; set;}

	[SerializeField]
	private Text Txt_EXP;

	public float MycurrentValue_EXP
	{
		get
		{ 
			return currentValue_EXP;
		}
		set
		{ 
			//value = di player
			//Pengecekan agar tidak lebih dari MaxEXP;
			if (value >= MaxExp) 
			{
				currentValue_EXP = 0;
				Player.lvlUp = true;
			} 
			else 
			{
				currentValue_EXP = value;
			}

			//Membagi supaya dapat 0.5 jika currentValue 50 dan MaxHp 100
			currentFill_EXP = currentValue_EXP / MaxExp;
			Txt_EXP.text = MycurrentValue_EXP.ToString () + " / " + MaxExp.ToString ();
		}
	}

	// Use this for initialization
	void Start () {
		content_EXP = GetComponent<Image> ();
	}

	// Update is called once per frame
	void Update () {
		content_EXP.fillAmount = currentFill_EXP;
	}

	public void Initialize(float currentValue_EXP, float myMaxEXP )
	{
		MaxExp = myMaxEXP;
		MycurrentValue_EXP = currentFill_EXP;
	}
}
