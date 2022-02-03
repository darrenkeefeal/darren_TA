using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob_Tutorial : MonoBehaviour {

    public GameObject Objectnya_Blob;

    Transform Spawn_Blob, Spawn_Blob_1;

    bool first_Spawn = true;

    // Use this for initialization
    void Start () {
        Spawn_Blob = transform.Find("Blob");
        Spawn_Blob_1 = transform.Find("Blob_1");
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (first_Spawn)
        {
            Instantiate(Objectnya_Blob, Spawn_Blob.position, Quaternion.identity);
            Instantiate(Objectnya_Blob, Spawn_Blob_1.position, Quaternion.identity);
            first_Spawn = false;
        }
	}
}
