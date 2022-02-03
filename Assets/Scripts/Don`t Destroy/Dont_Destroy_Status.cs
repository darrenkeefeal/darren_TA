using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dont_Destroy_Status : MonoBehaviour {

	public CanvasGroup Canvas_Player;
    Scene Scene_Name;

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
        Scene_Name = SceneManager.GetActiveScene();
        if (GameMaster.Shop_Open || Scene_Name.name == "Story_2") 
		{
			Canvas_Player.alpha = 0;
		}
        else 
		{
			Canvas_Player.alpha = 1;
		}
	}
}
