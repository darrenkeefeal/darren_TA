using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goblin_Stats : MonoBehaviour {

	private Image content_goblin;
	private float currentFill_Goblin;
	private float currentValue_Goblin;

	public float MaxHp_Goblin{ get; set;}

	public float MycurrentValue_Goblin
	{
		get
		{ 
			return currentValue_Goblin;
		}
		set
		{ 
			//value = di player
			//Pengecekan agar tidak lebih dari 100;
			if (value > MaxHp_Goblin) 
			{
				currentValue_Goblin = MaxHp_Goblin;
			} 
			else if (value < 0) 
			{
				currentValue_Goblin = 0;
			} 
			else 
			{
				currentValue_Goblin = value;
			}

			//Membagi supaya dapat 0.5 jika currentValue 50 dan MaxHp 100
			currentFill_Goblin = currentValue_Goblin / MaxHp_Goblin;
		}
	}

	// Use this for initialization
	void Start () {
		content_goblin = GetComponent<Image> ();
	}

	// Update is called once per frame
	void Update () {
		content_goblin.fillAmount = currentFill_Goblin;
	}

	public void Initialize(float currentValue, float myMaxHP)
	{
		MaxHp_Goblin = myMaxHP;
		MycurrentValue_Goblin = currentValue;
	}
}
