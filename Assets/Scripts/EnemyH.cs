using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class EnemyH : MonoBehaviour
{

    private Rigidbody2D rig;
    private Animator anim;
    private bool isDead;

    private float lostPlayerTimer = 0f;
    public float lostPlayerCooldown = 2f;

    public int health = 0;

    public bool isFront;
    private Vector2 direction;

    public bool isLefth; // Identifica a direcao do inimigo 
    public float stopDistance;

    public Transform point;
    public Transform behind;

    public float speed;
    public float maxVision;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (isLefth) //vira para esquerda
        {
            transform.eulerAngles = new Vector2(0, 0);
            direction = Vector2.left;
        }
        else //vira para direira
        {
            transform.eulerAngles = new Vector2(0, 180);
            direction = Vector2.right;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMove()
    {
        if (isFront && !isDead)
        {
            anim.SetInteger("transition", 1);

            if (isLefth) //vira para esquerda
            {
                transform.eulerAngles = new Vector2(0, 0);
                direction = Vector2.left;
                rig.linearVelocity = new Vector2(-speed, rig.linearVelocity.y);
            }
            else //vira para direira
            {
                transform.eulerAngles = new Vector2(0, 180);
                direction = Vector2.right;
                rig.linearVelocity = new Vector2(speed, rig.linearVelocity.y);
            }
        }
      
    }

    private void FixedUpdate()
    {
        GetPlayer();

        if (isFront && !isDead)
        {
            OnMove();
            lostPlayerTimer = 0f; // resetamos o timer se está vendo o player
        }
        else
        {
            lostPlayerTimer += Time.fixedDeltaTime;

            if (lostPlayerTimer >= lostPlayerCooldown)
            {
                rig.linearVelocity = Vector2.zero;
                anim.SetInteger("transition", 0); // Idle ou parado
            }
        }
    }

    void GetPlayer()
    { 
    RaycastHit2D hit = Physics2D.Raycast(point.position, direction, maxVision); // logica do inimigo enchergar o player.

    if (hit.collider != null && !isDead)
    {
        if (hit.transform.CompareTag("Player"))
        {
            isFront = true;

            float distance = Vector2.Distance(transform.position, hit.transform.position);
            if (distance <= stopDistance)
            {
                isFront = false;
                rig.linearVelocity = Vector2.zero;

                hit.transform.GetComponent<Player>().OnHit();

                anim.SetInteger("transition", 2); // precisa fazer a animacao
            }
        }
        else
        {
            isFront = false; // outro objeto na frente, não é o player
        }
    }
    else
    {
        isFront = false; // nada foi atingido
    }

    // Verifica atrás do inimigo
    RaycastHit2D behindHit = Physics2D.Raycast(behind.position, -direction, maxVision);
    
    if (behindHit.collider != null && behindHit.transform.CompareTag("Player"))
    {
        // Player está nas costas do inimigo.
        isLefth = !isLefth;
        isFront = true;
    }
}

    public void OnHit()
    {
        anim.SetTrigger("hit");
        health--;
        if (health <= 0)
        {
            isDead = true;
            speed = 0;
            anim.SetTrigger("dead");
            Destroy(gameObject, 1f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(point.position, direction * maxVision);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(behind.position, -direction * maxVision);
    }
}
