using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour {

    [SerializeField]
    List<Sprite> tutorial_Sprite = new List<Sprite>(); 

    int tutorial;

    // Use this for initialization
    void Start () {
        tutorial = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void next_Tutorial()
    {
        tutorial++;

        if (tutorial >= 14)
        {
            SceneManager.LoadScene("Tutorial_1");
        }
        else
        {
            GameObject.Find("Img_Tutorial").GetComponent<Image>().sprite = tutorial_Sprite[tutorial];
        }
    }
}
