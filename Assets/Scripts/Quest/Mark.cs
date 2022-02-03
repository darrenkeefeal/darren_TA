using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mark : MonoBehaviour {

	public int Delay;
	public SpriteRenderer mySprite;
	int ctr;
	bool toogle;

	Scene current_Scene;
	string scene_Name;

	// Use this for initialization
	void Start () 
	{
		current_Scene = SceneManager.GetActiveScene();
		scene_Name = current_Scene.name;

		this.transform.SetParent (GameObject.Find ("Canvas_Quest").transform);
		this.GetComponentInParent<Canvas> ().sortingOrder = 2;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (GameMaster.field && scene_Name == "Adventure_Fields")
		{
			if (GameMaster.Questing_Global <= 3) 
			{
				Flash (mySprite);
			}
			else
			{
				mySprite.enabled = false;
			}
		} 
		else if(GameMaster.city && scene_Name == "HomeTown")
		{
			if (GameMaster.Questing_Global >= 4 && GameMaster.Questing_Global <= 9) 
			{
				Flash (mySprite);
			}
			else 
			{
				mySprite.enabled = false;
			}
		}
		else if(GameMaster.boss)
		{
			mySprite.enabled = false;
		}
	}

	void Flash(SpriteRenderer spriteRender)
	{
		if (ctr >= Delay) 
		{
			ctr = 0;

			if (toogle) 
			{
				spriteRender.enabled = true;
				toogle = false;
			}
			else if (!toogle) 
			{
				spriteRender.enabled = false;
				toogle = true;
			}
		}
		else
		{
			ctr++;
		}
	}
}
