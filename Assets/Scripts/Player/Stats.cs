using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour {

	//Untuk Mengakses Image Harus UnityEngine UI
	//MycurrentValue digunakan untuk HP Player dipanggil di player.cs

	private Image content;
	private float currentFill;
	private float currentValue;
	public float MaxHp{ get; set;}

	[SerializeField]
	private Text Txt_HP;

	public float MycurrentValue
	{
		get
		{ 
			return currentValue;
		}
		set
		{ 
			//value = di player
			//Pengecekan agar tidak lebih dari 100;
			if (value > MaxHp) 
			{
				currentValue = MaxHp;
			} 
			else if (value < 0) 
			{
				currentValue = 0;
			} 
			else 
			{
				currentValue = value;
			}

			//Membagi supaya dapat 0.5 jika currentValue 50 dan MaxHp 100
			currentFill = currentValue / MaxHp;
			Txt_HP.text = MycurrentValue.ToString () + " / " + MaxHp.ToString ();
		}
	}

	// Use this for initialization
	void Start () {
		content = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		content.fillAmount = currentFill;
	}

	public void Initialize(float currentValue, float myMaxHP)
	{
		MaxHp = myMaxHP;
		MycurrentValue = currentValue;
	}
}
