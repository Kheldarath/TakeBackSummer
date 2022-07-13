using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    //this script runs as soon as the shot object is instantiated by OnFire();

    Rigidbody2D myRigidBody;
    [SerializeField] float shotSpeed = 10f;
    float ySpeed = 0f;
    float xSpeed;
    Player player;
    
    void Start()
    {
        player = FindObjectOfType<Player>(); //get a reference to player for origin point
        myRigidBody = GetComponent<Rigidbody2D>(); //get own body
        if (player.GetComponent<SpriteRenderer>().flipX) //check which way player is facing
        {
            //this makes sure the Bullet moves in respect of the player's facing, by checking the sprite flip bool
            xSpeed = -player.gameObject.transform.localScale.x * shotSpeed; //changes speed to minus value
            gameObject.GetComponent<SpriteRenderer>().flipX = true; //flips the sprite to represent
        }
        else
        {
            xSpeed = player.gameObject.transform.localScale.x * shotSpeed; //otherwise sprite will continue in a positive x direction
        }        
    }

    void Update()
    {
        myRigidBody.velocity = new Vector2(xSpeed, ySpeed); //set movement
    }

    private void OnTriggerEnter2D(Collider2D coll) //this checks what the shot has hit so we can destroy it
    {
        if(coll.tag == "Enemy")
        {
            EnemyMovement enemy = coll.GetComponent<EnemyMovement>(); //gets the enemymovement class component from the enemy hit
            enemy.SendMessage("HurtMe"); //triggers the enemies' hurt me script
            Destroy(gameObject);//destroys the shot object
        }
        else if(coll.tag == "Scenery") //hopefully checks if its hit something like a wall
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 5f); //otherwise we destroy the object 5 seconds after instantiation.
        }
    }
}
