using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blob_Intro : MonoBehaviour {

    public Text txt_IntroBlob;
    public static string txt_Damage_Enemy;

    private Transform pos;

    // Use this for initialization
    void Start () {
        this.transform.SetParent(this.transform, false);
        pos = this.GetComponentInParent<RectTransform>();
        this.transform.localPosition = new Vector2(pos.localPosition.x, pos.localPosition.y + 50);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
