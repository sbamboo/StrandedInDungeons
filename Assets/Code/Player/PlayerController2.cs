using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    // Script Setup
    public Rigidbody2D rb2D;
    public float movespeed = 3f;
    public float jumpforce = 40f;
    public bool isjumping = false;

    // Define moves
    private float moveHorizontal;
    private float moveVertical;


    // Start is called before the first frame update
    void Start()
    {
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
    }

    //Fixed update
    void FixedUpdate()
    {
        //Move character horizontaly (Apply moveSpeed when inputs are given for horizontal movement)
        if (moveHorizontal > 0.1f || moveHorizontal < -0.1f)
        {
            rb2D.AddForce(new Vector2(moveHorizontal * movespeed, 0f), ForceMode2D.Impulse);
        }

        //Move character vericaly (Jump if character is not already jumping and a veritcal input is given)
        if (!isjumping && moveVertical > 0.1f)
        {
            rb2D.AddForce(new Vector2(0f, moveVertical * jumpforce), ForceMode2D.Impulse);
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

    //Only allow jumping on a collider/object tagged "platform"
    void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.gameObject.tag == "Platform")
        {
            isjumping = false;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isjumping = true;
        }
    }
}
