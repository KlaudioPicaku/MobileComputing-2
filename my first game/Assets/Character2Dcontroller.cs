using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2Dcontroller : MonoBehaviour
{
    public Joystick joystick;
    public float MovementSpeed = 1;
    Rigidbody2D rb;
    Animator animator;
    [SerializeField] Transform groundCheckCollider;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float jmpPow = 500;
    float horizontalValue;
    float runSpeedModifier = 2f;
    bool isRunning = false;
    bool facingRight = true;
    bool isGrounded = false;
    bool jumping=false;
    bool jumper=false;

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
            //Store horizontal value
            horizontalValue = joystick.Horizontal;
            // if joystick is pushed beyond a certaing point(player is running)
            if (Mathf.Abs(joystick.Horizontal) >= 0.2 && !animator.GetCurrentAnimatorStateInfo(0).IsName("player_hurt"))
            {
                isRunning = true;
            }
            else
            {
                isRunning = false;
            }
            if (jumper)
            {
                jumping = true;
            } else {
                jumping = false;
            }
    }
    void FixedUpdate()
    {
        GroundCheck();
        Move(horizontalValue,jumping);
    }
    void GroundCheck()
    {
        // set isGrounded to  false to avoid endless jumping
        isGrounded = false;
        //checking if Grouncheck obj is colliding with platform
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, 0.2f,groundLayer);
        if (colliders.Length > 0)
        {
            isGrounded = true;
        }
    }
  
    public void Jump()
    {
        jumper = true;
        animator.SetBool("IsJumping", true);
    }
    public void stopJump()
    {
        jumper = false;
        animator.SetBool("IsJumping", false);
    }
    void Move(float dir, bool airFlag)
    {
        //if player is on the ground he can jump
        if (isGrounded && airFlag)
        {
            isGrounded = false;
            airFlag = true;
            rb.AddForce(new Vector2(0f, jmpPow));
        }
        #region Movement
        float xVal = dir * MovementSpeed* 100 * Time.fixedDeltaTime;
        if (isRunning)
        {
            xVal *= runSpeedModifier;
        }
        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
        rb.velocity = targetVelocity;
        if(facingRight && dir < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
        }
        else if(!facingRight && dir > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        }
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        #endregion
    }
}
