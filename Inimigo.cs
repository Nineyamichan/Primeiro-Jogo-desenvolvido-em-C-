using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{

    private Rigidbody2D rig;
    private Animator anim;

    public float speed;
    public Transform rigthCol;
    public Transform leftCol;

    public Transform headPoint;
    private bool colliding;

    public LayerMask layer;
    public BoxCollider2D boxCollider2D;
    

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    
    void Update()
    {
        rig.velocity = new Vector2(speed, rig.velocity.y);

        colliding = Physics2D.Linecast(rigthCol.position, leftCol.position);

        if (colliding)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
            speed *= -1f;

        }
    }

    void OnCollisionEnter2D (Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            float height = col.contacts[0].point.y - headPoint.position.y;

            if(height > 0)
            {
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5, ForceMode2D.Impulse);
                speed = 0;
                anim.SetTrigger("die");
                boxCollider2D.enabled = false;
                
                rig.bodyType = RigidbodyType2D.Kinematic;
                Destroy(gameObject, 0.25f);
            }


        }
    }
}
