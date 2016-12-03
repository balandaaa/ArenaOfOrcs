using UnityEngine;
using System.Collections;

public abstract class Character : MonoBehaviour {
	public Animator myAnimator{ get; private set; }	
	[SerializeField]
	protected float movementSpeed;
	public bool facingRight;
	protected bool attack;
	protected bool isGrounded;
	protected bool jump;
	[SerializeField]
	protected float jumpForce;
	[SerializeField]
	protected float health;
	public abstract bool IsDead{ get;}
	protected EdgeCollider2D WeaponCollider;
	// Use this for initialization
	public virtual void Start () {
		facingRight = true;
		myAnimator = GetComponent<Animator> ();
		WeaponCollider= GameObject.Find ("orc_weapon").GetComponent<EdgeCollider2D> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}



	public void ChangeDirection(){
		facingRight = !facingRight;
		//transform.localScale = new Vector2 (transform.localScale.x * -1, 0.5f);
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
    public abstract void Damage(float damage);

}
