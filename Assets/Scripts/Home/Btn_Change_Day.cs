using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Btn_Change_Day : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Yes_Upgrade()
	{
		GameObject.Find ("Canvas_Button_Yes_No_ChangeDay").GetComponent<Canvas> ().sortingOrder = 0;
		GameObject.Find ("Yes_No_Upgrade").GetComponent<CanvasGroup> ().alpha = 0;
		Player.upgrade_weapon = true;
	}

	public void No_Upgrade()
	{
		GameObject.Find ("Canvas_Button_Yes_No_ChangeDay").GetComponent<Canvas> ().sortingOrder = 0;
		GameObject.Find ("Yes_No_Upgrade").GetComponent<CanvasGroup> ().alpha = 0;
		Player.upgrade_weapon = false;
	}

	public void ChangeDay()
	{
		GameObject.Find ("Canvas_Button_Yes_No_ChangeDay").GetComponent<Canvas> ().sortingOrder = 0;
		GameObject.Find ("Yes_No_ChangeDay").GetComponent<CanvasGroup> ().alpha = 0;

		if (Farming.planted) 
		{
			Farming.changeDay_Farm1 = true;
		}
		if (Farming_2.planted) 
		{
			Farming_2.changeDay_Farm2 = true;
		}
		if (Farming_3.planted) 
		{
			Farming_3.changeDay_Farm3 = true;
		}
		if (Farming_4.planted) 
		{
			Farming_4.changeDay_Farm4 = true;
		}
		if (Farming_5.planted) 
		{
			Farming_5.changeDay_Farm5 = true;
		}
		if (Farming_6.planted) 
		{
			Farming_6.changeDay_Farm6 = true;
		}
		if (Farming_7.planted) 
		{
			Farming_7.changeDay_Farm7 = true;
		}
		if (Farming_8.planted) 
		{
			Farming_8.changeDay_Farm8 = true;
		}
		if (Farming_9.planted) 
		{
			Farming_9.changeDay_Farm9 = true;
		}

        GameMaster.Change_Day = true;

		SceneManager.LoadScene ("Loading_Screen");
		GameMaster.loadingScreen = true;

		GameMaster.enemySpawn = false;

		GameMaster.city = false;
		GameMaster.field = false;
		GameMaster.house = true;
		GameMaster.boss = false;

		GameMaster.fromBoss = false;
		GameMaster.fromHouse = true;

		GameObject.Find ("Player").GetComponent<Transform> ().position = new Vector2 (-0.72f, -1.35f);
        
        //Save.save_Anything_Farm = true;
	}

	public void No_ChangeDay()
	{
		GameObject.Find ("Canvas_Button_Yes_No_ChangeDay").GetComponent<Canvas> ().sortingOrder = 0;
		GameObject.Find ("Yes_No_ChangeDay").GetComponent<CanvasGroup> ().alpha = 0;
	}
}
