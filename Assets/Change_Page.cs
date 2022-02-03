using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_Page : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Next()
    {
        Online_Item_1.page++;
    }

    public void Prev()
    {
        Online_Item_1.page--;
    }
}
