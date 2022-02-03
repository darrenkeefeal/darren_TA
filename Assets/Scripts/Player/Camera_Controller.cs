using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour {

	public GameObject player;

	private Vector3 offset;

	// Use this for initialization
	void Start () 
	{
		offset = transform.position - player.transform.position;
	}
	
	// LateUpdate Trakher dijalankan setelah semua sudah dijalankan
	void LateUpdate ()
	{
		transform.position = player.transform.position + offset;
	}
}
