using UnityEngine;
using System.Collections;

public abstract class Character : MonoBehaviour {
	public Animator myAnimator{ get; private set; }	
	[SerializeField]
	protected float movementSpeed;
	protected bool facingRight;
	protected bool attack;
	protected bool isGrounded;
	protected bool jump;
	[SerializeField]
	protected float jumpForce;
	// Use this for initialization
	public virtual void Start () {
		facingRight = true;
		myAnimator = GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	private float _eletero;
	public float eletero
	{
		get
		{
			return _eletero;
		}

		set
		{
			if (value <= 100)
				_eletero = value;
			else
				Debug.Log ("Max élet");
		}
	}

	public void ChangeDirection(){
		facingRight = !facingRight;
		//transform.localScale = new Vector2 (transform.localScale.x * -1, 0.5f);
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
		
}
