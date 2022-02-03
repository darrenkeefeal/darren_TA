using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Goblin : MonoBehaviour {

    public AudioClip Goblin_Dead;
    public AudioClip Goblin_Attack;

    public AudioSource audio_Source;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Goblin.goblin_Attack_Sound)
        {
            audio_Source.clip = Goblin_Attack;
            audio_Source.Play();
            Goblin.goblin_Attack_Sound = false;
        }
        else if (Goblin.goblin_Dead_Sound)
        {
            audio_Source.clip = Goblin_Dead;
            audio_Source.Play();
            Goblin.goblin_Dead_Sound = false;
        }
    }
}
