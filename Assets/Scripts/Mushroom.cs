using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    private Rigidbody2D rig;
    public Animator anim;

    private Health healthSystem;
    public float speed;
    
    public Transform point;
    public float radius;
    public LayerMask layer;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        healthSystem = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
        void FixedUpdate()
    {
        rig.linearVelocity = new Vector2(speed, rig.linearVelocity.y);
        OnCollision();
    }

    void OnCollision()
    {
        // movimentacao do inimigo
        Collider2D hit = Physics2D.OverlapCircle(point.position, radius, layer);        

        if (hit != null)
        {            
            // chamado quando o inimigo e chamado em um objeto que tenha a layer selecionada.
            Debug.Log("Bateu!");
            speed = -speed;

            if (transform.eulerAngles.y == 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }        
    }

    public void OnHit()
    {
        anim.SetTrigger("hit");
        healthSystem.health--;
        if(healthSystem.health <= 0)
        {
            speed = 0;
            anim.SetTrigger("dead");
            Destroy(gameObject, 1f);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(point.position, radius);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<Player>().OnHit();
        }
    }

}
