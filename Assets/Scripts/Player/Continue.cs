using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continue : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Yes_Continue()
	{
		Player.player_Continue = true;
        //Save.save_Anything_Player = true;
    }

	public void No_Continue()
	{

	}
}
