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

    //Shoot
    public float projectilespeed = 7;
    public Transform spawnPointRight;
    public Transform spawnPointLeft;

    public Projectile projectilePrefab;

    Coroutine JumpForceChange;

    public int maxLives = 5;
    private int _lives = 3;

    public int lives
    {
        get { return _lives; }
        set
        {
            //if (_lives > value)
            // we lost a life - we need to respawn
            // 

            _lives = value;

            if (lives > maxLives)
                _lives = maxLives;

            //(_lives < 0)
            //gameover

            Debug.Log("lives has been set to" + _lives.ToString()); 

        }
    }

    public int _score = 0;

    public int Score
    {
        get { return _score; } 
        set
        {
            _score = value;

            Debug.Log("Score: " + _score.ToString());
        }
    }

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

        //projectile

        if(projectilespeed <= 0)
        {
            projectilespeed = 7;
        }

        if (!spawnPointLeft || !spawnPointRight || !projectilePrefab)
        {
            Debug.Log("Please setup default values on" + gameObject.name);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] curPlayingClip = anim.GetCurrentAnimatorClipInfo(0);

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
        //attack
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Attack");
        }
        if (curPlayingClip.Length > 0)
        {
            if (Input.GetButtonDown("Fire1") && curPlayingClip[0].clip.name != "Attack")
            {
                anim.SetTrigger("Attack");
            }
            else if (curPlayingClip[0].clip.name == "Attack")
            {
                rb.velocity = Vector2.zero;
            }
            else
            {
                /*
                    Vector2 moveDirection = new Vector2(hInput * speed, rb.velocity.y);
                rb.velocity = moveDirection;
                */
            }
        }


        //check for flipped and create an algorithm to flip your character
        if (hInput != 0)
        {
            sr.flipX = (hInput < 0);
        }

        if (isGrounded) 
        {
            rb.gravityScale = 1;

        }
        //shoot
        if (Input.GetButtonDown("Fire2"))
        {
            anim.SetTrigger("Shoot");
        }
            
    }
    public void IncreaseGravity()
    {
        rb.gravityScale = 6;

    }

    public void Fire()
    {
        if (!sr.flipX)
        {
            Projectile curProjectile = Instantiate(projectilePrefab, spawnPointRight.position, spawnPointRight.rotation);
            curProjectile.speed = projectilespeed;
        }
        else
        {
            Projectile curProjectile = Instantiate(projectilePrefab, spawnPointLeft.position, spawnPointLeft.rotation);
            curProjectile.speed = -projectilespeed;
        }
        
    }

     public void StartJumpForceChange()
    {
        if(JumpForceChange == null)
        {
            JumpForceChange = StartCoroutine(jumpForceChange());
        }
    }
   IEnumerator jumpForceChange()
    {
        jumpForce *= 2;
         yield return new WaitForSeconds(5.0f);

        jumpForce /= 2;
        JumpForceChange= null;
    }
}
