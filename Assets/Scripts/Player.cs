using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rig;
    private PlayerAudio playerAudio;
    public Animator anim;
    public Transform point;

    public LayerMask enemyLayer;

    private Health healthSystem;

    public float radius;
    public float speed;
    public float jumpForce;

    private bool isJumping;
    private bool doubleJump;

    private bool isAttacking;

    private bool recovery;

    private static Player instance;
    

    public float invunerabilityTime = 0;
    public float invunerabilityDuration = 2;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rig = GetComponent<Rigidbody2D>(); 
        playerAudio = GetComponent<PlayerAudio>();
        healthSystem = GetComponent<Health>();
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
        if(invunerabilityTime > 0) invunerabilityTime -= Time.deltaTime;
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
                playerAudio.PlaySFX(playerAudio.jumpSoud);
            }
            else if (doubleJump)
            {
                anim.SetInteger("transition", 2);
                rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                doubleJump = false;
                playerAudio.PlaySFX(playerAudio.jumpSoud);
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
            playerAudio.PlaySFX(playerAudio.hitSound);
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


    public void OnHit()
    {
        if(invunerabilityTime > 0)
            return;

        invunerabilityTime = invunerabilityDuration;
        healthSystem.health--;


        if(healthSystem.health > 0)
        {
            anim.SetTrigger("hit");
        }
                     

        if (healthSystem.health <= 0)
        {
            anim.SetTrigger("dead");
            GameControler.instance.ShowGameOver();
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
            playerAudio.PlaySFX(playerAudio.itemSound);
            GameControler.instance.GetPlastic();
            Destroy(collision.gameObject, 0.05f);
        }

        if (collision.CompareTag("Glass"))
        {
            playerAudio.PlaySFX(playerAudio.itemSound);
            GameControler.instance.GetGlass();
            Destroy(collision.gameObject, 0.05f);
        }

        if (collision.CompareTag("Paper"))
        {
            playerAudio.PlaySFX(playerAudio.itemSound);
            GameControler.instance.GetPaper();
            Destroy(collision.gameObject, 0.05f);
        }

        if (collision.CompareTag("Can"))
        {
            playerAudio.PlaySFX(playerAudio.itemSound);
            GameControler.instance.GetCan();
            Destroy(collision.gameObject, 0.05f);
        }
    }
}
