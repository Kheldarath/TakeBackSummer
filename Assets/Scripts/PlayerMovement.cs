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
    [SerializeField] private float groundCheckDistance; //this 1.415 in inspector.
    [SerializeField] private LayerMask theGround;
    

    private Rigidbody2D playerBox;
    private CapsuleCollider2D coll;
    private BoxCollider2D feet;
    private SpriteRenderer sprite;
    private Animator playerAnim;


    private float dirx = 0f;
    private bool isClimbing = false;    
    private Vector2 x_playerMovement, y_playerMovement;
    private float gravityAtStart;


    private void Awake()
    {
        playerBox = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        feet = GetComponent<BoxCollider2D>();
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
        //playerBox.velocity = new Vector2(dirx * runSpeed, playerBox.velocity.y);        
        Run();        
        ClimbLadder();
        UpdateAnim();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }
    
    void OnJump(InputValue value)
    {
        //if(!coll.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }


        if (value.isPressed && IsGrounded())
        {
            //vector 2 as we are 2dimensional 
            playerBox.velocity += new Vector2(playerBox.velocity.x, jumpHeight);

        }
    }

    void ClimbLadder()
    {
        if (!coll.IsTouchingLayers(LayerMask.GetMask("Ladder"))) 
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

    /// <summary>
    /// Going to use this method to call a raycast to the ground to make sure we are on the ground before trying top jump.
    /// </summary>
    /// <returns></returns>
   private bool IsGrounded()
    {
        Vector2 checkpos = transform.position - new Vector3(0f, feet.size.y / 2);
        return Physics2D.Raycast(checkpos, Vector2.down, groundCheckDistance, theGround);        
    }

    /* -------------The below is a beginning for if I want to re-implement slopes. At the moment this method is clunky and abrasive ----*/

    //private void SlopeCheck()
    //{
    //    Vector2 checkPos = transform.position - new Vector3(0f, feet.size.y / 2);
    //    SlopeCheckVertical(checkPos);
    //}

    //private void SlopeCheckHorizontal(Vector2 checkpos)
    //{
    //    RaycastHit2D hitRight = Physics2D.Raycast(checkpos, Vector2.right);
    //    RaycastHit2D hitLeft = Physics2D.Raycast(checkpos, Vector2.left);
    //}

    //private void SlopeCheckVertical(Vector2 checkpos)
    //{
    //    RaycastHit2D hit = Physics2D.Raycast(checkpos, Vector2.down, groundCheckDistance, theGround);

    //    if (hit)
    //    {
    //        Debug.DrawRay(hit.point, hit.normal, Color.red);
    //    }
    //}
 }
