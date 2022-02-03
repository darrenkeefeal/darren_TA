using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Blob : MonoBehaviour {

    public AudioClip Blob_Dead;
    public AudioClip Blob_Attack;

    public AudioSource audio_Source;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Blob.Blob_Attack_Sound)
        {
            audio_Source.clip = Blob_Attack;
            audio_Source.Play();
            Blob.Blob_Attack_Sound = false;
        }
        else if (Blob.Blob_Dead_Sound)
        {
            audio_Source.clip = Blob_Dead;
            audio_Source.Play();
            Blob.Blob_Dead_Sound = false;
        }
    }
}
