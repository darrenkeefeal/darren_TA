using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Video_2 : MonoBehaviour {

    public VideoPlayer vid;
    public GameObject video_Object;

    public AudioSource ost;

    Camera camera_Tmp;

    public static bool ending_Story = false;

    // Use this for initialization
    void Start ()
    {
        if (vid.GetComponent<Camera>() == null)
        {
            camera_Tmp = GameObject.Find("Main Camera").GetComponent<Camera>();
            vid.targetCamera = camera_Tmp;
        }
        
        vid.loopPointReached += CheckOver;
    }

    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        video_Object.SetActive(false);

        SceneManager.LoadScene("Loading_Screen");
        GameMaster.loadingScreen = true;

        GameMaster.enemySpawn = true;

        GameMaster.city = false;
        GameMaster.field = true;
        GameMaster.house = false;
        GameMaster.boss = false;

        GameMaster.fromBoss = false;
        GameMaster.fromHouse = false;

        GameObject.Find("Player").GetComponent<Transform>().localPosition = new Vector2(-10.13f, 16.30f);
    }

    // Update is called once per frame
    void Update ()
    {
        /*
        if (ending_Story)
        {
            vid.Play();
            vid.loopPointReached += CheckOver;
            ending_Story = false;
        }
        */
    }
}
