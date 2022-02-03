using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dont_Destroy_Player_World : MonoBehaviour {
    
    Scene Name_Scene;

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        Name_Scene = SceneManager.GetActiveScene();
        if (Name_Scene.name == "Loading_Screen" || GameMaster.Shop_Open)
        {
            GameObject.Find("Canvas_Player_World").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("Canvas_Player_World").GetComponent<Canvas>().sortingOrder = -1;
        }
        else if (Name_Scene.name == "Story_2")
        {
            GameObject.Find("Canvas_Player_World").GetComponent<CanvasGroup>().alpha = 0;
        }
        else
        {
            GameObject.Find("Canvas_Player_World").GetComponent<CanvasGroup>().alpha = 1;
            GameObject.Find("Canvas_Player_World").GetComponent<Canvas>().sortingOrder = 5;
        }
	}
}
