using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_Manager : MonoBehaviour {

	public GameObject Dialog_Box;
	public Text Txt_Dialog;

	public bool dialogOk;

	public string[] dialogLine;
	public int currentLine;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		/*
		if (dialogOk && Input.GetKeyDown(KeyCode.Space)) 
		{
			Dialog_Box.SetActive (false);
			dialogOk = false;
		}
		*/
	}

	public void ShowDialogFull(string dialog , float x , float y)
	{
		this.transform.localPosition = new Vector2 (x, y);

		dialogOk = true;
		Dialog_Box.SetActive (true);
		Txt_Dialog.text = dialog;
	}

	public void DestroyDialog()
	{
		Dialog_Box.SetActive (false);
		dialogOk = false;
	}

	public void ShowDialog()
	{
		dialogOk = true;
		Dialog_Box.SetActive (true);
	}
}
