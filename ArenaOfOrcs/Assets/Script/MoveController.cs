using UnityEngine;
using System.Collections;

public class MoveController : MonoBehaviour
{
	private Rigidbody2D rb;
	private Animator myAnimator;	
	[SerializeField]
	private float movementSpeed;
	private bool facingRight;
	private bool attack;
	[SerializeField]
	private Transform[] groundPoints;
	[SerializeField]
	private float groundRadius;
	[SerializeField]
	private LayerMask whatIsGround;
	private bool isGrounded;
	private bool jump;
	[SerializeField]
	private float jumpForce;
	void Start(){
		facingRight = true;
		rb = GetComponent<Rigidbody2D> ();
		myAnimator = GetComponent<Animator> ();
	}
	void Update(){
		HandleInput ();
		if (transform.position.y < -1) {
			//Application.LoadLevel (Application.loadedLevel);

		}
	}
	void FixedUpdate(){
		float horizontal = Input.GetAxis ("Horizontal");
		isGrounded = IsGrounded ();
	
		HandleMovement (horizontal);
		Flip (horizontal);
		HandleAttacks ();
		ResetValues ();
	}
	private void HandleInput(){
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			attack = true;
		}
		if (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") >= 0) {
			jump = true;
		}
	}
		
	private void HandleMovement(float horizontal){
		if (isGrounded && jump) {
			isGrounded = false;
			rb.AddForce(new Vector2(0,jumpForce));
		}
		rb.velocity = new Vector2 (horizontal * movementSpeed, rb.velocity.y);
		myAnimator.SetFloat ("speed", Mathf.Abs(horizontal));
	}
	private void HandleAttacks(){
		if (attack) {
			if (Random.Range(0f, 1.0f) > 0.5f)
				myAnimator.SetTrigger("attack");
			else
				myAnimator.SetTrigger("special");
		}
	}
	private void Flip(float horizontal){
		if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight) {
			facingRight = !facingRight;
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
	}
	private void ResetValues(){
		attack = false;
		jump = false;
	}
	private bool IsGrounded(){
		if (rb.velocity.y<=0) {
			foreach (Transform point in groundPoints) {
				Collider2D[] colliders = Physics2D.OverlapCircleAll (point.position, groundRadius, whatIsGround);
				for (int i = 0; i < colliders.Length; i++) {
					if (colliders[i].gameObject != gameObject) {
						return true;
					}
				}
			}
		}
		return false;
	}

}
