using UnityEngine;
using System.Collections;

public class Enemy : Character
{
    private Rigidbody2D rb;
    private Transform player;
    // Use this for initialization
    private Player playerController;
    float timeLeft = 30;
    private bool tamatok;
    private float attackTimer;
    private float attackCooldown = 0.001f;
    private bool canAttack = true;
    float deadTimer =2f;

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
       
       // Debug.Log(deadTimer);
      
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

            //myAnimator.SetTrigger("attack");
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
        //if (Input.GetKey(KeyCode.LeftControl)) {
        //if (other.tag == "weapon" && !IsDead && player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("attack")) {
        	//Damage ();
        //	}
        //}


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



        Debug.Log("ütök");

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
