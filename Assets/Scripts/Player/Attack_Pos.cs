using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attack_Pos : MonoBehaviour {

	private Collider2D col;

	public static bool female_npc;

	// Use this for initialization
	void Start () {
		col = gameObject.GetComponent<BoxCollider2D> ();
		col.enabled = false;

		female_npc = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Player.Attacking || Player.Hoeing || Tutorial_Move.Attacking) 
		{
			col.enabled = true;
		} 
		else 
		{
			col.enabled = false;
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Female_npc") 
		{
			female_npc = true;
		}
	}
}
