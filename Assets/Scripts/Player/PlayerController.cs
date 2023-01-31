using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    //Components
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;

    //movement var

    public float speed;
    public float jumpForce;

    //groundcheck stuff
    public bool isGrounded;
    public Transform GroundCheck;
    public LayerMask isGroundLayer;
    public float GroundCheckRadius;
   

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        if (speed <= 0)
        {
            speed = 6.0f;
            Debug.Log("Speed was set incorrect, deafaulting to " + speed.ToString());
        }
        if (jumpForce <= 0)
        {
            jumpForce = 300;
            Debug.Log("jumpForce was set incorrect, deafaulting to " + jumpForce.ToString());
        }
        if (GroundCheckRadius <= 0)
        {
            GroundCheckRadius = 0.2f;
            Debug.Log("groundCheckRadius was set incorrect, deafaulting to " + GroundCheckRadius.ToString());
        }
        if (!isGrounded)
        {
            GroundCheck = GameObject.FindGameObjectWithTag("GroundCheck").transform;
            Debug.Log("Ground Check not set, finding it manually");
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        //groundedchecking
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, isGroundLayer);

        //horizontal movement
        float hInput = Input.GetAxisRaw("Horizontal");

        Vector2 moveDirection = new Vector2(hInput * speed, rb.velocity.y);
        rb.velocity = moveDirection;
        Debug.Log(hInput);

        //set grounded
        anim.SetBool("isGrounded", isGrounded);

        //animate walking back and forth
        anim.SetFloat("hInput", Mathf.Abs(hInput));

        //jump movement
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Attack");
        }

    }

}
