using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Script Setup
    public Rigidbody2D rb2D;
    public Collider2D standingCollider;
    public Collider2D sneakingCollider;
    public Collider2D crouchingCollider;
    public Collider2D footTriggerCollider;

    public float movespeed = 3f;
    public float jumpforce = 40f;
    public bool isJumping = false;
    public bool isSneaking = false;
    public bool isCrouching = false;

    public bool AllowWalk = true;
    public bool AllowJump = true;
    public bool AllowSneak = true;
    public bool AllowCrouch = true;

    // Define moves
    private float moveHorizontal;
    private float moveVertical;

    //Misc
    private bool sneakKey;
    private bool crouchKey;


    // Start is called before the first frame update
    void Start()
    {
        //Auto Disable crouch collider
        crouchingCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Get Horizontal movement
        moveHorizontal = Input.GetAxisRaw("Horizontal");
       
        //Jumping
        if (Input.GetKey(KeyCode.Space))
        {
            moveVertical = 1;
        }
        else
        {
            moveVertical = Input.GetAxisRaw("Vertical");
        }

        //FootIsColliding check
        if (footTriggerCollider.IsTouchingLayers(-1))
        {
            isJumping = false;
        } else
        {
            isJumping = true;
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

        //Jump if allowed
        if (AllowJump)
        {
            //Move character vericaly (Jump if character is not already jumping and a veritcal input is given)
            if (!isJumping && moveVertical > 0.1f)
            {
                rb2D.AddForce(new Vector2(0f, moveVertical * jumpforce), ForceMode2D.Impulse);
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
                standingCollider.enabled = false;
                crouchingCollider.enabled = false;
                isSneaking = true;
            }
            else if (isSneaking && !sneakKey)
            {
                standingCollider.enabled = true;
                crouchingCollider.enabled = true;
                isSneaking = false;
            }
        }

        //GetCrouchKey
        if (Input.GetKey(KeyCode.LeftControl) | Input.GetKey(KeyCode.RightControl))
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
                standingCollider.enabled = false;
                sneakingCollider.enabled = false;
                crouchingCollider.enabled = true;
                isCrouching = true;
            }
            else if (isCrouching && !crouchKey)
            {
                standingCollider.enabled = true;
                sneakingCollider.enabled = true;
                crouchingCollider.enabled = false;
                isCrouching = false;
            }
        }

        //Rotate Sprite Right
        if (moveHorizontal > 0)
        {
            gameObject.transform.localScale = new Vector3(3, 4, 1);
        }

        //Rotate Sprite Left
        if (moveHorizontal < 0)
        {
            gameObject.transform.localScale = new Vector3(-3, 4, 1);
        }
    }
}
