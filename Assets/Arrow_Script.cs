using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Script : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    bool Fade = false;

	// Update is called once per frame
	void Update ()
    {
        if (this.GetComponent<CanvasGroup>().alpha == 1)
        {
            Fade = false;
        }
        if (this.GetComponent<CanvasGroup>().alpha == 0)
        {
            Fade = true;
        }

        if (!Fade)
        {
            this.GetComponent<CanvasGroup>().alpha = this.GetComponent<CanvasGroup>().alpha - 0.05f;
        }
        if (Fade)
        {
            this.GetComponent<CanvasGroup>().alpha = this.GetComponent<CanvasGroup>().alpha + 0.05f;
        }
    }
}
