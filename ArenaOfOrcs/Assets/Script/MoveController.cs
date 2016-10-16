using UnityEngine;
using System.Collections;

public class MoveController : MonoBehaviour
{

    public LayerMask ground;
    public Transform GroundCheck;
    private Vector2 targetPosition;
    public float speed = 5;
    public bool lookRight = true;
    private Animator animator;
    public bool isGrounded = false;
    public bool isWall = false;
    public bool jump = false;
    GameObject wall;
    public Transform WallCheck;
    public float jumpForce = 1000f;
    void Start()
    {

        targetPosition = transform.position;
        animator = GetComponent<Animator>();
        wall = GameObject.Find("Cube");

    }


    void Update()
    {
        transform.Rotate(0, 0, 0);
        // isGrounded = Physics2D.OverlapCircle(GroundCheck.position, 0.15f, ground);
        isGrounded = Physics2D.Linecast(transform.position, GroundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        isWall = Physics2D.Linecast(transform.position, WallCheck.position, 1 << LayerMask.NameToLayer("Wall"));
        /*   float moveHorizontal = Input.GetAxis("Horizontal");
           Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
           rb.AddForce(movement);*/

        if (Input.GetKey(KeyCode.LeftArrow) && isWall && targetPosition.x > -15)
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
        if (Input.GetKey(KeyCode.UpArrow) && isGrounded)
        {
            jump = true;
            //isGrounded = false;
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
    void FixedUpdate()
    {
        if (jump && transform.position.y <= 0.0f)
        {
            jump = false;
            transform.position = new Vector2(transform.position.x, transform.position.y + jumpForce);
            // rb2d.AddForce(new Vector2(0f, jumpForce),ForceMode2D.Impulse);

        }
    }
    public void Flip()
    {
        var s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
        lookRight = !lookRight;
    }
}
