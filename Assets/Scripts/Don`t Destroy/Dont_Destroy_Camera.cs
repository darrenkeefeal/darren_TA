using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dont_Destroy_Camera : MonoBehaviour {

	public Camera camera;

	public void Awake()
	{
		DontDestroyOnLoad (this.gameObject);

		if (FindObjectsOfType(GetType()).Length > 1) 
		{
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (GameMaster.house) 
		{
			camera.backgroundColor = Color.white;
		}
		else
		{
			camera.backgroundColor = Color.black;
		}
	}
}
