using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : Character
{
    private Rigidbody2D rb;


    [SerializeField]
    private Transform[] groundPoints;
    [SerializeField]
    private float groundRadius;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private Text healthText;
    [SerializeField]
    private Text scoreText;

    int score = 0;
    private Transform enemy;
    [SerializeField]
    private GameObject GameOverUI;
    [SerializeField]
    private GameObject PauseUI;
    [SerializeField]
    private GameObject NextButton;
    private Enemy enemyController;
    private float attackRange = 3f;
    private GameObject[] enemies;
    float pauseTimer = 0.2f;
    [SerializeField]
    private GameObject winUI;
    [SerializeField]
    private Text maxScoreText;
    int enemyKilledCounter = 0;
    [SerializeField]
    private Text enemyKilledText;
    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();


        //GameObject gameControllerObject = GameObject.FindWithTag("Enemy");

    }
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject item in enemies)
        {
            if (item != null)
            {
                if (Vector2.Distance(transform.position, item.transform.position) < attackRange)
                {
                    enemyController = item.GetComponent<Enemy>();
                }
            }
        }

        // Debug.Log(Vector2.Distance(transform.position, getNearesEnemy().transform.position));
        healthText.text = (int)health * 10 + "/100";
        scoreText.text = "Score: " + score;

        HandleInput();
        if (transform.position.y < -1 || IsDead)
        {
            EndGame();
        }
        if (enemies.Length == 0)
            win();
        if (Input.GetButtonDown("Paused"))
        {

            PauseUI.SetActive(true);

            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;

            }
            else
            {
                Time.timeScale = 1;
                PauseUI.SetActive(false);
            }
        }
    }
    void FixedUpdate()
    {

        //  enemy = GameObject.Find("Enemy").transform;
        if (!IsDead)
        {
            float horizontal = Input.GetAxis("Horizontal");
            isGrounded = IsGrounded();

            HandleMovement(horizontal);
            Flip(horizontal);
            HandleAttacks();
            ResetValues();
        }

    }
    private void HandleInput()
    {
        if (Input.GetButtonDown("Attack"))
        {
            attack = true;
        }
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    private void HandleMovement(float horizontal)
    {
        if (isGrounded && jump)
        {
            isGrounded = false;
            rb.AddForce(new Vector2(0, jumpForce));
            GetComponent<AudioSource>().Play();
        }
        rb.velocity = new Vector2(horizontal * movementSpeed, rb.velocity.y);
        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }
    private void HandleAttacks()
    {
        if (attack)
        {
            //WeaponCollider.enabled = true;

            if (Random.Range(0f, 1.0f) > 0.5f)
            {

                myAnimator.SetTrigger("attack");

                enemyController.Damage(10);
            }
            else
            {

                myAnimator.SetTrigger("special");

                enemyController.Damage(20);

            }
        }
        else
        {
            //WeaponCollider.enabled = false;
        }
    }
    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            ChangeDirection();
        }
    }
    private void ResetValues()
    {
        attack = false;
        jump = false;
    }
    private bool IsGrounded()
    {
        if (rb.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            //  StartCoroutine(TakeDamage(20));
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
    public void AddScore(int score)
    {
        this.score += score;
    }
    public void AddKilled()
    {
        enemyKilledCounter++;
    }
    void EndGame()
    {
        GameOverUI.SetActive(true);
        healthText.text = "";
        scoreText.text = "";

    }
    void win()
    {
        winUI.SetActive(true);
        maxScoreText.text = "Max Score: " + score;
        enemyKilledText.text = "Killed enemy: " + enemyKilledCounter;
        healthText.text = "";
        scoreText.text = "";
       if(SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex+1)
        {
            NextButton.SetActive(true);
        }
        else
        {
            NextButton.SetActive(false);
        }
        
    }
}
