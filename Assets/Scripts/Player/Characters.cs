using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour {

	[SerializeField]
	private float speed;

	public static Vector2 direction;

	protected Animator animator;

	private Rigidbody2D rgbody;

	protected bool isAttack = false;

	protected bool isDead = false;

	protected bool isHoe = false;

	public bool isMoving
	{
		get
		{ 
			return direction.x != 0 || direction.y != 0;
		}
	}

	// Use this for initialization
	//Protected Virtual Void Harus digunakan untuk melakukan start pada anaknya (base.Start // base.Update dll)
	protected virtual void Start () {
		animator = GetComponent<Animator> ();
		rgbody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		Handle ();
	}

	private void FixedUpdate()
	{
		Move ();
	}

	public void Move()
	{
        //Player Transfrom
        //transform.Translate (direction * speed * Time.deltaTime);
        /*
		if (direction.x != 0 || direction.y != 0) 
		{
			AnimatorMovement (direction);
		} 
		else 
		{
			animator.SetLayerWeight (1, 0);
		}
		*/

        //Player Rigidbody2D
        //Normalized digunakan untuk membuat speed x , y = 1 , 1 tidak lebih
		rgbody.velocity = direction.normalized * speed;
	}

	public void Handle()
	{
		if (isMoving) 
		{
			ActiveLayer ("Walk_No_Weapon");
			animator.SetFloat ("x", direction.x);
			animator.SetFloat ("y", direction.y);
			StopAttack ();
		}
		else if (isAttack) 
		{
			ActiveLayer ("Attack");
		} 
		else if (isDead) 
		{
			ActiveLayer ("Dead");
		}
		else if(isHoe)
		{
			ActiveLayer ("Hoe");
		}
		else 
		{
			ActiveLayer ("Idle");
		}
	}

	public void ActiveLayer(string layerName)
	{
		//LayerCount digunakan untuk mengetahui semua layer yang ada di player
		for (int i = 0; i < animator.layerCount; i++) 
		{
			animator.SetLayerWeight (i, 0);
		}

		//Untuk mengaktifkan layer yang diminta
		//Function ini sama dengan animator.setLayerWeight(1,1)
		animator.SetLayerWeight (animator.GetLayerIndex (layerName), 1);
	}

	public void StopAttack()
	{
		isAttack = false;
		Player.Attacking = false;
        Tutorial_Move.Attacking = false;
		animator.SetBool ("Attack", isAttack);
	}

	public void StopHoe()
	{
		isHoe = false;
		Player.Hoeing = false;
		animator.SetBool ("Hoe", isAttack);
	}

}
