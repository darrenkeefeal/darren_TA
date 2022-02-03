using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Orc : MonoBehaviour {

    public AudioClip Orc_Dead;
    public AudioClip Orc_Attack;

    public AudioSource audio_Source;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Boss.orc_Attack_Sound)
        {
            audio_Source.clip = Orc_Attack;
            audio_Source.Play();
            Boss.orc_Attack_Sound = false;
        }
        else if (Boss.orc_Dead_Sound)
        {
            audio_Source.clip = Orc_Dead;
            audio_Source.Play();
            Boss.orc_Dead_Sound = false;
        }
    }
}
