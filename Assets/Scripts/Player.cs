using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rig;
    public Animator anim;
    public Transform point;

    public LayerMask enemyLayer;

    public int health;
    public float radius;
    public float speed;
    public float jumpForce;

    private bool isJumping;
    private bool doubleJump;

    private bool isAttacking;

    private bool recovery;

    private static Player instance;
    private void Awake()
    {
        DontDestroyOnLoad(this); //mantém um objeto entre cenas.

        if (instance == null) //checando se o instace é null.
        {
            instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();        
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Attack();
    }

    private void FixedUpdate()
    {
        Move();        
    }

    void Move()
    {
        float movement = Input.GetAxis("Horizontal");

        rig.linearVelocity = new Vector2(movement * speed, rig.linearVelocity.y);

        if (movement > 0)
        {
            if (!isJumping && !isAttacking)
            {
                anim.SetInteger("transition", 1);
            }
            
            transform.eulerAngles = new Vector3 (0, 0, 0);
        }
        
        if (movement < 0)
        {
            if (!isJumping && !isAttacking)
            {
                anim.SetInteger("transition", 1);
            }
            
            transform.eulerAngles = new Vector3 (0, 180, 0);
        }
        if (movement == 0 && !isJumping && !isAttacking)
        {
            anim.SetInteger("transition", 0);
        }
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJumping)
            {
                anim.SetInteger("transition", 2);
                rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isJumping = true;
                doubleJump = true;
            }
            else if (doubleJump)
            {
                anim.SetInteger("transition", 2);
                rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                doubleJump = false;
            }
        }
            
    }

    void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            isAttacking = true;
            anim.SetInteger("transition", 3);

            Collider2D hit = Physics2D.OverlapCircle(point.position, radius, enemyLayer);

            if (hit != null)
            {            

                if (hit.GetComponent<Mushroom>())
                {
                    hit.GetComponent<Mushroom>().OnHit();
                }

                if (hit.GetComponent<EnemyH>())
                {
                    hit.GetComponent<EnemyH>().OnHit();
                }
            }

            StartCoroutine(OnAttack());
        }
       
    }

    IEnumerator OnAttack()
    {
        yield return new WaitForSeconds(0.611f);
        isAttacking = false;
    }

    float recoveryCount;
    public void OnHit()

    {
        recoveryCount += Time.deltaTime;

        if (recoveryCount >= 2f)
        {
            anim.SetTrigger("hit");

            recoveryCount = 0f;   
            
        }     
                     

        if (health <= 0 && !recovery)
        {
            recovery = true;
            anim.SetTrigger("dead");
            //Gamer Over aqui
        }
        
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(point.position, radius);
    }

    void OnCollisionEnter2D(Collision2D colisor)
    {
        if (colisor.gameObject.layer == 6)
        {
            isJumping = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            OnHit();
        }

        if (collision.CompareTag("Plastic"))
        {
            GameControler.instance.GetPlastic();
            Destroy(collision.gameObject, 0.05f);
        }

        if (collision.CompareTag("Glass"))
        {
            GameControler.instance.GetGlass();
            Destroy(collision.gameObject, 0.05f);
        }

        if (collision.CompareTag("Paper"))
        {
            GameControler.instance.GetPaper();
            Destroy(collision.gameObject, 0.05f);
        }

        if (collision.CompareTag("Can"))
        {
            GameControler.instance.GetCan();
            Destroy(collision.gameObject, 0.05f);
        }
    }
}
