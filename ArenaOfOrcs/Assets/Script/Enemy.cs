﻿using UnityEngine;
using System.Collections;

public class Enemy : Character
{
	private Rigidbody2D rb;
	private Transform player;
	// Use this for initialization
	public override void Start () {
		base.Start ();
		eletero = 100;
		player = GameObject.Find ("Player").transform;
		rb = GetComponent<Rigidbody2D> ();
	}


	// Update is called once per frame
	void Update () {
		Debug.Log ("Enemy heal: ");
		Debug.Log(eletero);
		if (eletero < 0) {
			myAnimator.SetFloat ("dead", 1);
		} else {
			if (player.transform.position.x - transform.position.x < 0 || player.transform.position.x - transform.position.x > 1f) {
				Move ();
			} else {
				myAnimator.SetFloat ("walk", 0);
			}
		}
		if (Mathf.Abs (player.transform.position.x - transform.position.x) < 1.5f && (player.localScale.x>0 && transform.localScale.x < 0 || player.localScale.x<0 && transform.localScale.x > 0)) {
			Attack ();
			//myAnimator.SetTrigger("attack");
		} else {
			myAnimator.SetFloat ("attack", 0);
		}

	}
	public void FixedUpdate(){
		ResetValues ();
	}
	public void Move(){
		myAnimator.SetFloat ("walk", 1);
		//transform.Translate (GetDirection () * (movementSpeed * Time.deltaTime));
			if (GetDirection () == Vector2.left)
				rb.velocity = new Vector2 (1 * movementSpeed, rb.velocity.y);
			else
				rb.velocity = new Vector2 (-1 * movementSpeed, rb.velocity.y);

	}
	public Vector2 GetDirection(){
		return facingRight ? Vector2.left : Vector2.right;
	}
	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Edge")
		{
			ChangeDirection ();
		}
		if (other.tag == "weapon") {
			eletero -= 50;
		}
	}
	public void Attack ()
	{
		
			//myAnimator.SetTrigger("attack");
			myAnimator.SetFloat ("attack", 1);
			Debug.Log ("ütök");

	}
	private void ResetValues(){
		attack = false;
	}

}
