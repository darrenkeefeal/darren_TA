using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dont_Destroy_Chest : MonoBehaviour {

	public void Awake()
	{
		DontDestroyOnLoad (this.gameObject);

		if (FindObjectsOfType(GetType()).Length > 1) 
		{
			Destroy (gameObject);
		}

		//if (GameMaster.field) 
		//{
		//	gameObject.SetActive (true);
		//} 
		//else 
		//{
		//	gameObject.SetActive (false);
		//}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
