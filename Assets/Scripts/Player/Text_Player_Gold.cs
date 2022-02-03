using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text_Player_Gold : MonoBehaviour {

	public Text txt_Gold;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		txt_Gold.text = "Player Gold : " + GameMaster.Player_Gold + "G";
	}
}
