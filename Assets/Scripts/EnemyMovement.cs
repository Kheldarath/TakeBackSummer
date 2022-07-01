using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    [SerializeField] Patrol myPatrol;
        
    bool goingLeft = false;
    bool goingRight = false;
    
    Rigidbody2D myRigidbody;
    SpriteRenderer mySprite;
    BoxCollider2D myBox;




    void Start()
    {
        myBox = GetComponent<BoxCollider2D>();
        mySprite = GetComponent<SpriteRenderer>();
        myRigidbody = GetComponent<Rigidbody2D>();        
    }

  
    void Update()
    {        

        myRigidbody.velocity = new Vector2(moveSpeed, 0f);
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "PatrolTrigger")
        {
            moveSpeed = -moveSpeed;
            FlipSpriteFacing();
        }
    }
    
    void FlipSpriteFacing()
    {
        transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), gameObject.transform.localScale.y);
    }
}
