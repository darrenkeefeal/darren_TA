using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crafting_Place : MonoBehaviour 
{
	public Inventory Crafting;
	BoxCollider2D[] myCollider;

	Scene current_Scene;
	string scene_Name;

	void Start () 
	{
		if (Crafting == null) 
		{
			Crafting = FindObjectOfType<Crafting> ();
		}

		myCollider = gameObject.GetComponents<BoxCollider2D> ();

		current_Scene = SceneManager.GetActiveScene();
		scene_Name = current_Scene.name;
	}

	void Update()
	{
		if (GameMaster.house) 
		{
			this.GetComponent<SpriteRenderer> ().enabled = true;
			//this.GetComponent<BoxCollider2D> ().enabled = true;

			foreach (BoxCollider2D collider in myCollider) 
			{
				collider.enabled = true;
			}
		}
		else
		{
			this.GetComponent<SpriteRenderer> ().enabled = false;
			//this.GetComponent<BoxCollider2D> ().enabled = false

			foreach (BoxCollider2D collider in myCollider) 
			{
				collider.enabled = false;
			}
		}
	}
}
