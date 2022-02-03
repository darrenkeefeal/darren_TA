using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Player : MonoBehaviour {

    public AudioClip Attack;
    public AudioClip Damage;

    public AudioSource audio_Source;

    // Use this for initialization
    void Start ()
    {
      
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Player.audio_Sword)
        {
            audio_Source.clip = Attack;
            audio_Source.Play();
            Player.audio_Sword = false;
        }
    }
}
