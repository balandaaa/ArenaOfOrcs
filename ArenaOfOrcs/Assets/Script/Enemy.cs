using UnityEngine;
using System.Collections;

public class Enemy : Character
{
	private Rigidbody2D rb;
	private Transform player;
	// Use this for initialization
	private Player playerController;
	public override void Start () {
		base.Start ();
		player = GameObject.Find ("Player").transform;
		rb = GetComponent<Rigidbody2D> ();
		GameObject gameControllerObject = GameObject.FindWithTag ("Player");
		if (gameControllerObject != null)
		{
			playerController = gameControllerObject.GetComponent <Player>();
		}
		if (playerController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	// Update is called once per frame
	void Update () {
		

		if (IsDead) {
			myAnimator.SetFloat ("dead", 1);
		} else {
			if (player.transform.position.x - transform.position.x < 0 || player.transform.position.x - transform.position.x > 1f) {
				Move ();
			} else {
				myAnimator.SetFloat ("walk", 0);
			}
		}

			if (!IsDead && Mathf.Abs (player.transform.position.x - transform.position.x) < 1.5f && (player.localScale.x>0 && transform.localScale.x < 0 || player.localScale.x<0 && transform.localScale.x > 0)) {
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
		//if (Input.GetKey(KeyCode.LeftControl)) {
		if (other.tag == "weapon" && !IsDead && player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("attack")) {
			StartCoroutine (TakeDamage (10));
			}
		//}

	}

	public void Attack ()
	{
		attack = true;
			//myAnimator.SetTrigger("attack");
			myAnimator.SetFloat ("attack", 1);
			Debug.Log ("ütök");

	}
	public override bool IsDead{
		get{
			return health <= 0;
		}
	}

	public override IEnumerator TakeDamage (int damage){
		health -= damage;
		if (!IsDead) {
			
			Move ();
			myAnimator.SetFloat ("attack", 1);
		} else {
			myAnimator.SetFloat ("dead", 1);
			yield return new WaitForSeconds(2);
			playerController.AddScore (10);
			Destroy(gameObject);
			yield return null;
		}
	}

	private void ResetValues(){
		attack = false;
	}

}
