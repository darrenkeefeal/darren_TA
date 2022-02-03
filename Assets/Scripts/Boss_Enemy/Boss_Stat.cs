using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Stat : MonoBehaviour {

	private Image content_Boss;
	private float currentFill_Boss;
	private float currentValue_Boss;

	public float MaxHp_Boss{ get; set;}

	public float MycurrentValue_Boss
	{
		get
		{ 
			return currentValue_Boss;
		}
		set
		{ 
			//value = di player
			//Pengecekan agar tidak lebih dari 100;
			if (value > MaxHp_Boss) 
			{
				currentValue_Boss = MaxHp_Boss;
			} 
			else if (value < 0) 
			{
				currentValue_Boss = 0;
			} 
			else 
			{
				currentValue_Boss = value;
			}

			//Membagi supaya dapat 0.5 jika currentValue 50 dan MaxHp 100
			currentFill_Boss = currentValue_Boss / MaxHp_Boss;
		}
	}

	// Use this for initialization
	void Start () {
		content_Boss = GetComponent<Image> ();
	}

	// Update is called once per frame
	void Update () {
		content_Boss.fillAmount = currentFill_Boss;
	}

	public void Initialize(float currentValue, float myMaxHP)
	{
		MaxHp_Boss = myMaxHP;
		MycurrentValue_Boss = currentValue;
	}
}
