using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wolf_Stats : MonoBehaviour {

	private Image content_wolf;
	private float currentFill_Wolf;
	private float currentValue_Wolf;

	public float MaxHp_Wolf{ get; set;}

	public float MycurrentValue_Wolf
	{
		get
		{ 
			return currentValue_Wolf;
		}
		set
		{ 
			//value = di player
			//Pengecekan agar tidak lebih dari 100;
			if (value > MaxHp_Wolf) 
			{
				currentValue_Wolf = MaxHp_Wolf;
			} 
			else if (value < 0) 
			{
				currentValue_Wolf = 0;
			} 
			else 
			{
				currentValue_Wolf = value;
			}

			//Membagi supaya dapat 0.5 jika currentValue 50 dan MaxHp 100
			currentFill_Wolf = currentValue_Wolf / MaxHp_Wolf;
		}
	}

	// Use this for initialization
	void Start () {
		content_wolf = GetComponent<Image> ();
	}

	// Update is called once per frame
	void Update () {
		content_wolf.fillAmount = currentFill_Wolf;
	}

	public void Initialize(float currentValue, float myMaxHP)
	{
		MaxHp_Wolf = myMaxHP;
		MycurrentValue_Wolf = currentValue;
	}
}
