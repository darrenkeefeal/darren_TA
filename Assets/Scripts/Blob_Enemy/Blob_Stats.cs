using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blob_Stats : MonoBehaviour {

	private Image content_blob;
	private float currentFill_Blob;
	private float currentValue_Blob;

	public float MaxHp_Blob{ get; set;}

	public float MycurrentValue_Blob
	{
		get
		{ 
			return currentValue_Blob;
		}
		set
		{ 
			//value = di player
			//Pengecekan agar tidak lebih dari 100;
			if (value > MaxHp_Blob) 
			{
				currentValue_Blob = MaxHp_Blob;
			} 
			else if (value < 0) 
			{
				currentValue_Blob = 0;
			} 
			else 
			{
				currentValue_Blob = value;
			}

			//Membagi supaya dapat 0.5 jika currentValue 50 dan MaxHp 100
			currentFill_Blob = currentValue_Blob / MaxHp_Blob;
		}
	}

	// Use this for initialization
	void Start () {
		content_blob = GetComponent<Image> ();
    }

	// Update is called once per frame
	void Update () {
		content_blob.fillAmount = currentFill_Blob;
    
	}

	public void Initialize(float currentValue, float myMaxHP)
	{
		MaxHp_Blob = myMaxHP;
		MycurrentValue_Blob = currentValue;
	}
}
