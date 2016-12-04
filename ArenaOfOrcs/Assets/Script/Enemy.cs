using UnityEngine;
using System.Collections;

public class Enemy : Character
{
    private Rigidbody2D rb;
    private Transform player;
    private Player playerController;
    float deadTimer = 2f;
    [SerializeField]
    public override void Start()
    {
        base.Start();
        player = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        GameObject gameControllerObject = GameObject.FindWithTag("Player");
        if (gameControllerObject != null)
        {
            playerController = gameControllerObject.GetComponent<Player>();
        }
        if (playerController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }

    }

    // Update is called once per frame
    void Update()
    {

        float xDir = player.transform.position.x - transform.position.x;
        float yDir = player.transform.position.y - transform.position.y;
        if (yDir > -2 && yDir < 1 && (xDir < 0 && xDir > -4 && facingRight || xDir > 0 && xDir < 4 && !facingRight))
        {
            ChangeDirection();
        }
        
        if (IsDead)
        {

            deadTimer -= Time.deltaTime;
            myAnimator.SetFloat("dead", 1);
       
            if (deadTimer < 0)
            {
             
                   
                playerController.AddScore(10);
                Destroy(gameObject);

            }
        }
        else
        {
            if (player.transform.position.x - transform.position.x < 0 || player.transform.position.x - transform.position.x > 1f)
            {
                Move();
            }
            else
            {
                myAnimator.SetFloat("walk", 0);
            }
        }
        if (!IsDead && Vector2.Distance(transform.position, player.position) <= 1.5f)
        {
            Attack();
            GetComponent<AudioSource>().Play();
        }
        else
        {
            myAnimator.SetFloat("attack", 0);
        }
    }
    public void FixedUpdate()
    {
        ResetValues();
    }
    public void Move()
    {
        myAnimator.SetFloat("walk", 1);
        //transform.Translate (GetDirection () * (movementSpeed * Time.deltaTime));
        if (GetDirection() == Vector2.left)
            rb.velocity = new Vector2(1 * movementSpeed, rb.velocity.y);
        else
            rb.velocity = new Vector2(-1 * movementSpeed, rb.velocity.y);
    }
    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.left : Vector2.right;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Edge")
        {
            ChangeDirection();
        }
    }

    public void Attack()
    {
      
        attack = true;
        //myAnimator.SetTrigger("attack");
        myAnimator.SetFloat("attack", 1);
        playerController.Damage(0.03f);
        if (!IsDead)
        {
           
            Move();
            myAnimator.SetFloat("attack", 1);
        }
        else
        {

            myAnimator.SetFloat("dead", 1);
        }
    }
    public override bool IsDead
    {
        get
        {
            return health <= 0;
        }
    }

    public override void Damage(float damage)
    {
        health -= damage;

    }

    private void ResetValues()
    {
        attack = false;
    }

}
