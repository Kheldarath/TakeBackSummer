using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Configurations
    //This enumerator lets the game engine know what our player is doing in the game. This allows us to more easily set animations
    private enum MOVESTATE {idle, running, jumping, falling }
    

    [SerializeField] private float jumpHeight = 14f;
    [SerializeField] private float runSpeed = 7f;
     [SerializeField] private LayerMask jumpableGround;
    
    private float dirx = 0f;

    private Rigidbody2D playerBox;
    private Collider2D coll;
    private SpriteRenderer sprite;
    private Animator playerAnim;



    // Start is called before the first frame update
    void Start()
    {
        playerBox = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        dirx = Input.GetAxisRaw("Horizontal");
        playerBox.velocity = new Vector2(dirx * runSpeed, playerBox.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            //vector 2 as we are 2dimensional 
            playerBox.velocity = new Vector2(playerBox.velocity.x, jumpHeight);
            
        }

        UpdateAnim();
    }

    /// <summary>
    /// Here we set up the Sprite animation direction and usage. dependent on user iunput.
    /// The enum is called to check the state of the player wether they are running jumping falling or standing still
    /// </summary>
    private void UpdateAnim()
    {
        MOVESTATE state;

        if (dirx > 0f)
        {
            state = MOVESTATE.running;
            sprite.flipX = false;
        }
        else if (dirx < 0f)
        {
            state = MOVESTATE.running;
            sprite.flipX = true;
        }
        else
        {
            state = MOVESTATE.idle;
        }

        //the below block has higher priority than running so that we don't get "running in the air"
        if (playerBox.velocity.y > .1f)
        {
            state = MOVESTATE.jumping;
        }
        else if (playerBox.velocity.y < -.1f)
        {
            state = MOVESTATE.falling;
        }
        //sets the state to the correct number so that the animator controller can pick up the change. ENUM's can be converted to int values using the (int)ENUM argument. so that each enum value gets converted to a digit, starting at 0. (like an array)
        playerAnim.SetInteger("moveState", (int)state);
    }

    /// <summary>
    /// Going to use this method to call a raycast to the ground to make sure we are on the ground before trying top jump.
    /// </summary>
    /// <returns></returns>
   private bool IsGrounded()
    {
       return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    } 
}
