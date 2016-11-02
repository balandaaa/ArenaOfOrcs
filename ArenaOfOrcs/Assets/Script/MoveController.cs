using UnityEngine;
using System.Collections;

public class MoveController : MonoBehaviour
{

    public LayerMask ground;
    private Vector2 targetPosition;
    public float speed = 25;
    public bool lookRight = true;
    private Animator animator;
   

	private bool onGround; 
	public float jumpPressure;
	public float minJump;
	public float maxJumpPressure;
	private Rigidbody2D rb;

	void Start()
    {

        targetPosition = transform.position;
		animator = GetComponent<Animator>();
		onGround = true;
		jumpPressure = 0f;
		minJump = 5f;
		maxJumpPressure = 10f;
		rb = GetComponent<Rigidbody2D> ();
    }


    void Update()
    {
		onGround = true;
        transform.Rotate(0, 0, 0);
        // isGrounded = Physics2D.OverlapCircle(GroundCheck.position, 0.15f, ground);
       // isGrounded = Physics2D.Linecast(transform.position, GroundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        /*   float moveHorizontal = Input.GetAxis("Horizontal");
           Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
           rb.AddForce(movement);*/

        if (Input.GetKey(KeyCode.LeftArrow) && targetPosition.x > -15)
        {
            targetPosition -= new Vector2(0.25f * Time.deltaTime * speed, 0);

        }
        if (Input.GetKey(KeyCode.RightArrow) && targetPosition.x < 15)
        {
            targetPosition += new Vector2(0.25f * Time.deltaTime * speed, 0);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if (Random.Range(0f, 1.0f) > 0.5f)
                animator.SetTrigger("attack");
            else
                animator.SetTrigger("special");
        }
		if (onGround) {
			if (Input.GetButton ("Jump")) {
				if (jumpPressure < maxJumpPressure) {
					jumpPressure += Time.deltaTime * 10f;
				} else {
					jumpPressure = maxJumpPressure;
				}
			} else {
				if (jumpPressure > 0f) {
					jumpPressure = jumpPressure + minJump;
					transform.position = new Vector2(transform.position.x, transform.position.y + minJump);
					jumpPressure = 0f;
					onGround = false;
				}
			}
		}






        if (targetPosition.x > transform.position.x && !lookRight)
            Flip();
        if (targetPosition.x < transform.position.x && lookRight)
            Flip();

        var p = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        //Vector3 vel = targetPosition - transform.position;
        //vel = Vector3.ClampMagnitude(vel, speed * Time.deltaTime);
        //transform.position += vel;

        animator.SetFloat("speed", (transform.position - p).magnitude / Time.deltaTime);
    }
	void OnTriggerEnter(Collider other){
		if(other.gameObject.CompareTag("ground")){
			onGround = true;
		}
	}
    void FixedUpdate()
    {
       
    }
    public void Flip()
    {
        var s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
        lookRight = !lookRight;
    }
}
