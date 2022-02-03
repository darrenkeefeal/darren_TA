using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading_Screen : MonoBehaviour {

	[SerializeField]
	private float units;

	[SerializeField]
	private GameObject createPrefab;

	[SerializeField]
	private Image fill;

	[SerializeField]
	private Image chararter;

	private float fillAmount;
	private float x;

	// Use this for initialization
	void Start () 
	{
		x = GameObject.Find ("Background").GetComponent<RectTransform> ().localPosition.x - 300;
		StartCoroutine (BuildUnits());
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateBar ();
	}

	public IEnumerator BuildUnits()
	{
		for(int i = 0 ; i <= units ; i++)
		{
			fillAmount = i / units;
			x = x + 2.3f;
			yield return null;
		}
	}

	private void UpdateBar()
	{
		fill.fillAmount = fillAmount;
		chararter.rectTransform.localPosition = new Vector2 (x, this.transform.localPosition.y + 430);

		if (fillAmount == 1) 
		{
            if (GameMaster.Change_Day)
            {
                GameMaster.Change_Day = false;
            }
			chararter.GetComponent<Animator> ().enabled = false;
			GameMaster.loadingScreen = false;
            if (GameMaster.city)
            {
                SceneManager.LoadScene("HomeTown");
                Quest_Trigger.ctrSign = 1;
            }
            else if (GameMaster.field)
            {
                SceneManager.LoadScene("Adventure_Fields");
                Quest_Trigger.ctrSign = 0;
            }
            else if (GameMaster.boss)
            {
                SceneManager.LoadScene("Boss_Dugeon");
            }
            else if (GameMaster.house)
            {
                SceneManager.LoadScene("Home");
            }
            else if (GameMaster.tutorial)
            {
                SceneManager.LoadScene("Story_1");
            }
            else if (GameMaster.newGame)
            {
                SceneManager.LoadScene("Adventure_Fields");
                Quest_Trigger.ctrSign = 0;
                GameMaster.newGame = false;
            }
		}
	}
}
