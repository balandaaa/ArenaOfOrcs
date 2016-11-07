using UnityEngine;
using System.Collections;

public class Enemy : Character {
	private IEnemyState curentState;
	// Use this for initialization
	private Rigidbody2D rb;
	public override void Start () {
		base.Start ();
		rb = GetComponent<Rigidbody2D> ();
		ChangeState (new IdleState ());
	}
	
	// Update is called once per frame
	void Update () {
		curentState.Execute ();
	}
	public void ChangeState(IEnemyState newState){
		if (curentState != null) {
			curentState.Exit ();
		}
		curentState = newState;
		curentState.Enter (this);
	}
	public void Move(){
		myAnimator.SetFloat ("speed", 1);
		transform.Translate (GetDirection () * (movementSpeed * Time.deltaTime));
	}
	public Vector2 GetDirection(){
		return facingRight ? Vector2.right : Vector2.left;
	}
}
