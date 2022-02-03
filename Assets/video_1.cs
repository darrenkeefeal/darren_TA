using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class video_1 : MonoBehaviour {

    public VideoPlayer vid;
    public AudioSource ost;

    // Use this for initialization
    void Start ()
    {
        vid.loopPointReached += CheckOver;
    }

    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        ost.Stop();
        SceneManager.LoadScene("Tutorial");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
