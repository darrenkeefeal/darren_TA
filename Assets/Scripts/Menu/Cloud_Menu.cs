using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud_Menu : MonoBehaviour {

    public Transform trans_Cloud;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        trans_Cloud.localPosition = new Vector2(trans_Cloud.localPosition.x + 0.05f, trans_Cloud.localPosition.y);

        if (trans_Cloud.localPosition.x > 15f)
        {
            trans_Cloud.localPosition = new Vector2(-28.2f, trans_Cloud.localPosition.y);
        }
	}
}
