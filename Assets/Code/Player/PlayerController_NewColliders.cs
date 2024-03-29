using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is for me and my friends game. And we, Tor Andersson and Simon Kalmi Claesson remain in overship of this code.
// License: https://github.com/simonkalmiclaesson/StrandedInDungeons/blob/main/license.md

public class PlayerController_NewColliders : MonoBehaviour
{
    // Script Setup
    private Rigidbody2D rb2D;
    public Collider2D standingCollider;
    public Collider2D sneakingCollider;
    public Collider2D crouchingCollider;
    public Animator animator;
    public Collider2D AlwaysCollider;

    [Space]

    [SerializeField]
    LayerMask groundMask;

    [SerializeField]
    LayerMask wallMask;

    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    [Range(0,2)]
    float groundCheckRadius;

    [Space]

    public float movespeed = 3f;
    public float sprintmodifier = 1.2f;
    public float sneakmodifier = 2f;
    public float crouchmodifier = 2f;
    public float jumpforce = 40f;
    public float walljumpforce = 20f;
    public bool isSprinting = false;
    public bool isSneaking = false;
    public bool isCrouching = false;
    public int walljumps = 3;
    private int i_walljumps = 0;
    public float walljumpbounce = 10f;
    public float walljump2ndJumpmodifier = 0.5f;

    //states
    public bool canJump = false;
    public bool canWallJump = false;

    public bool AllowWalk = true;
    public bool AllowSprint = true;
    public bool AllowJump = true;
    public bool AllowSneak = true;
    public bool AllowCrouch = true;
    public bool AllowWallJump = true;

    // Define moves
    private float moveHorizontal;
    private float moveVertical;

    //Misc
    private bool sprintKey;
    private bool sneakKey;
    private bool crouchKey;

    //Debug (make public to see)
    public bool debugStandingColliderEnabled;
    public bool debugSneakingColliderEnabled;
    public bool debugCrouchingColliderEnabled;
    public float debugTimeFixedDeltaTime = 0f;

    //Items
    [SerializeField]
    ItemBehavior items;

    // Start is called before the first frame update
    void Start()
    {
        //Auto Disable crouch collider
        crouchingCollider.enabled = false;

        //New Collider Systen
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Has items debug
        if (items.hasItem == true)
        {
            Debug.Log(items.hasItem);
        }

        debugStandingColliderEnabled = standingCollider.enabled;
        debugSneakingColliderEnabled = sneakingCollider.enabled;
        debugCrouchingColliderEnabled = crouchingCollider.enabled;

        //Get Horizontal movement
        moveHorizontal = Input.GetAxisRaw("Horizontal");
       
        //Jumping & Walljumping
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            moveVertical = 1;        
            //Jump
            if (canJump)
            {
                //Move character vericaly (Jump if character is not already jumping and a veritcal input is given)
                if (AllowJump && moveVertical > 0.1f)
                {
                    rb2D.AddForce(new Vector2(0f, moveVertical * jumpforce), ForceMode2D.Impulse);
                }
            }
            //WallJump
            else if (i_walljumps > 0)
            {
                if (AllowWallJump && moveVertical > 0.1f)
                {
                    rb2D.AddForce(new Vector2(Mathf.Clamp(transform.localScale.x, -1, 1) * -walljumpbounce, moveVertical * walljumpforce), ForceMode2D.Impulse);
                    rb2D.AddForce(new Vector2(0f, moveVertical * walljumpforce * walljump2ndJumpmodifier), ForceMode2D.Impulse);
                    //transform.localScale = new Vector3(0 - transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    i_walljumps--;
                }
            }    
        }/*
        else
        {
            moveVertical = Input.GetAxisRaw("Vertical");
        }*/

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, groundMask);
        if(colliders.Length > 0)
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }

        Collider2D[] colliders2 = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, wallMask);
        if (colliders2.Length > 0)
        {
            // canWallJump = true;
            i_walljumps = walljumps;
            
        }
        else
        {
            i_walljumps = 0;
        }

    }

    //Fixed update
    void FixedUpdate()
    {

        //Walk if allowed
        if (AllowWalk)
        {
            //Move character horizontaly (Apply moveSpeed when inputs are given for horizontal movement)
            if (moveHorizontal > 0.1f || moveHorizontal < -0.1f)
            {
                rb2D.AddForce(new Vector2(moveHorizontal * movespeed, 0f), ForceMode2D.Impulse);
            }                                                                                                          
        }



        //GetSneakKey
        if (Input.GetKey(KeyCode.LeftShift) | Input.GetKey(KeyCode.RightShift))
        {
            sneakKey = true;
        }
        else
        {
            sneakKey = false;
        }

        //Sneak: Disable stadingCollider and crouchingCollider if sneaking
        if (AllowSneak)
        {
            //Check for sneak key.
            if (!isSneaking && sneakKey)
            {
                movespeed = (movespeed/sneakmodifier);
                standingCollider.enabled = false;
                crouchingCollider.enabled = false;
                animator.SetBool("animIsSneaking",true);
                isSneaking = true;
            }
            else if (isSneaking && !sneakKey)
            {
                movespeed = (movespeed*sneakmodifier);
                standingCollider.enabled = true;
                crouchingCollider.enabled = false;
                animator.SetBool("animIsSneaking",false);
                isSneaking = false;
            }
        }

        //GetCrouchKey
        if (Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.S))
        {
            crouchKey = true;
        } else {
            crouchKey = false;
        }

        //Crouch: Disable stadingCollider and sneakingCollider if crouching
        if (AllowCrouch)
        {
            //Check for crouch key.
            if (!isCrouching && crouchKey)
            {
                movespeed = (movespeed/crouchmodifier);
                standingCollider.enabled = false;
                sneakingCollider.enabled = false;
                crouchingCollider.enabled = true;
                animator.SetBool("animIsCrawling",true);
                isCrouching = true;
            }
            else if (isCrouching && !crouchKey)
            {
                movespeed = (movespeed*crouchmodifier);
                standingCollider.enabled = true;
                sneakingCollider.enabled = true;
                crouchingCollider.enabled = false;
                animator.SetBool("animIsCrawling",false);
                isCrouching = false;
            }
        }

        //GetSprintKey
        if (Input.GetKey(KeyCode.LeftControl) | Input.GetKey(KeyCode.RightControl))
        {
            sprintKey = true;
        } else {
            sprintKey = false;
        }

        //Sprint: Change movespeed
        if (AllowSprint)
        {
            //Check for sprint key.
            if (!isSprinting && sprintKey)
            {
                movespeed = (movespeed*sprintmodifier);
                animator.SetBool("animIsRunning",true);
                isSprinting = true;
            }
            else if (isSprinting && !sprintKey)
            {
                movespeed = (movespeed/sprintmodifier);
                animator.SetBool("animIsRunning",false);
                isSprinting = false;
            }
        }

        //Rotate Sprite Right
        if (moveHorizontal > 0)
        {
            gameObject.transform.localScale = new Vector3(4, 4, 1);
        }

        //Rotate Sprite Left
        if (moveHorizontal < 0)
        {
            gameObject.transform.localScale = new Vector3(-4, 4, 1);
        }
    }

    //Only allow jumping on a collider/object tagged "platform"
    /*void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.gameObject.tag == "Platform")
        {
            isJumping = false;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = true;
        }
    }*/
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
