using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Close_Split : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Close_Stack()
    {
        Inventory_Manager.Instance.selectSize.SetActive(false);
    }
}
