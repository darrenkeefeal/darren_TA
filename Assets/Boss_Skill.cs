using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Skill : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Destroy(gameObject, 2);
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.localPosition = new Vector2(this.transform.localPosition.x, this.transform.localPosition.y - 0.08f);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player.HP_Minus = -20;
        }
    }
}
