using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Wolf : MonoBehaviour {

    public AudioClip Wolf_Dead;
    public AudioClip Wolf_Attack;

    public AudioSource audio_Source;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Wolf.wolf_Attack_Sound)
        {
            audio_Source.clip = Wolf_Attack;
            audio_Source.Play();
            Wolf.wolf_Attack_Sound = false;
        }
        else if (Wolf.wolf_Dead_Sound)
        {
            audio_Source.clip = Wolf_Dead;
            audio_Source.Play();
            Wolf.wolf_Dead_Sound = false;
        }
    }
}
