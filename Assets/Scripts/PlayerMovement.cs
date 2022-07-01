using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;




public class PlayerMovement : MonoBehaviour
{
    //Configurations
    //This enumerator lets the game engine know what our player is doing in the game. This allows us to more easily set animations
    private enum MOVESTATE {idle, running, jumping, falling, climbing}

    Vector2 moveInput;

    [SerializeField] private float jumpHeight = 14f;
    [SerializeField] private float runSpeed = 7f;
    [SerializeField] private float climbSpeed = 5f;
    [SerializeField] private LayerMask jumpableGround;

    private Player player;
    private Rigidbody2D playerBox;
    private CapsuleCollider2D myBody;
    private BoxCollider2D myFeet;
    private SpriteRenderer sprite;
    private Animator playerAnim;

    private Vector2 x_playerMovement, y_playerMovement;

    private float dirx = 0f;
    private bool isClimbing = false;    
    private float gravityAtStart;


    private void Awake()
    {
        player = GetComponent<Player>();
        playerBox = GetComponent<Rigidbody2D>();
        myBody = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        //playerBox = GetComponent<Rigidbody2D>();
        //coll = GetComponent<Collider2D>();
        //sprite = GetComponent<SpriteRenderer>();
        playerAnim = GetComponent<Animator>();
        gravityAtStart = playerBox.gravityScale;
    }

    
    void Update()
    {
        if (player.isAlive)
        {//playerBox.velocity = new Vector2(dirx * runSpeed, playerBox.velocity.y);        
            Run();
            ClimbLadder();
            UpdateAnim();
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }
    
    void OnJump(InputValue value)
    {
        //if(!coll.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        if (player.isAlive)
        {
            if (value.isPressed && IsGrounded())
            {
                //vector 2 as we are 2dimensional 
                playerBox.velocity += new Vector2(playerBox.velocity.x, jumpHeight);

            }
        }
    }

    void ClimbLadder()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ladder"))) 
        {
            playerBox.gravityScale = gravityAtStart;
            isClimbing = false;
            return; 
        }

        y_playerMovement = new Vector2(playerBox.velocity.x, moveInput.y * climbSpeed);
        playerBox.velocity = y_playerMovement;
        playerBox.gravityScale = 0f;
        isClimbing = true;
    }
    void Run()
    {
        x_playerMovement = new Vector2(moveInput.x * runSpeed, playerBox.velocity.y);
        playerBox.velocity = x_playerMovement; 
    }

    /// <summary>
    /// Here we set up the Sprite animation direction and usage. dependent on user iunput.
    /// The enum is called to check the state of the player wether they are running jumping falling or standing still
    /// </summary>
    private void UpdateAnim()
    {
        MOVESTATE state;

        if (playerBox.velocity.x > 0f)
        {
            state = MOVESTATE.running;
            sprite.flipX = false;
        }
        else if (playerBox.velocity.x < 0f)
        {
            state = MOVESTATE.running;
            sprite.flipX = true;
        }
        else
        {
            state = MOVESTATE.idle;
        }

        //the below block has higher priority than running so that we don't get "running in the air"
        if (playerBox.velocity.y > .1f && !IsGrounded())
        {
            state = MOVESTATE.jumping;
        }
        else if (playerBox.velocity.y < -.1f && !IsGrounded())
        {
            state = MOVESTATE.falling;
        }

        if (isClimbing)
        {
            state = MOVESTATE.climbing;
        }
        //sets the state to the correct number so that the animator controller can pick up the change. ENUM's can be converted to int values using the (int)ENUM argument. so that each enum value gets converted to a digit, starting at 0. (like an array)
        playerAnim.SetInteger("moveState", (int)state);
    }
        
   private bool IsGrounded()
    {
        return (myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")));
       //return Physics2D.BoxCast(myBody.bounds.center, myBody.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.gameObject.tag == "Enemy")
        {
            player.killPlayer();
        }
    }
}
