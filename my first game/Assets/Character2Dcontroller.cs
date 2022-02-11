using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Character2Dcontroller : MonoBehaviour
{
    public Joystick joystick;
    public float MovementSpeed = 1;
    Rigidbody2D rb;
    Animator animator;
    [SerializeField] Transform groundCheckCollider;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask ladderLayer;
    [SerializeField] float jmpPow = 500;
    [SerializeField] float distance;
    float horizontalValue;
    float verticalValue;
    float runSpeedModifier = 2f;
    bool isRunning = false;
    bool facingRight = true;
    bool isGrounded = false;
    bool jumping = false;
    bool jumper = false;
    [SerializeField] bool isClimbing = false;
    [SerializeField] bool isClimbingDown = false;
    private void Awake()
    {
        this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
    }
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        if (!loadingScreen.activeInHierarchy){
            //Store horizontal and vertical value
            horizontalValue = joystick.Horizontal;
            verticalValue = joystick.Vertical;
            if (joystick.isActiveAndEnabled)
            {
                // if joystick is pushed beyond a certaing point(player is running)
                if (Mathf.Abs(horizontalValue) >= 0.2 && !animator.GetCurrentAnimatorStateInfo(0).IsName("player_hurt"))
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
                }
                else
                {
                    jumping = false;
                }
                Collider2D[] hitinfo = Physics2D.OverlapCircleAll(transform.position, 0.2f, ladderLayer);
                if (hitinfo.Length > 0)
                {
                    if (verticalValue > 0)
                    {
                        isClimbing = true;
                        isClimbingDown = false;
                    }
                    else if (verticalValue < 0)
                    {
                        isClimbing = false;
                        isClimbingDown = true;
                    }
                }
                else if (hitinfo.Length == 0)
                {
                    isClimbing = false;
                    isClimbingDown = false;
                    rb.gravityScale = 1f;
                    animator.ResetTrigger("isClimbing");
                    animator.SetFloat("yVelocity", 0f);
                }
                if (isClimbing)
                {
                    rb.velocity = new Vector2(rb.velocity.x, verticalValue * MovementSpeed * 200 * Time.fixedDeltaTime);
                    rb.gravityScale = 0f;
                    animator.SetTrigger("isClimbing");
                    animator.SetFloat("yVelocity", verticalValue);
                }
                else if (isClimbingDown)
                {
                    rb.velocity = new Vector2(rb.velocity.x, verticalValue * MovementSpeed * 200 * Time.fixedDeltaTime);
                    rb.gravityScale = 0f;
                    animator.SetTrigger("isClimbing");
                    animator.SetFloat("yVelocity", verticalValue);
                }
                GroundCheck();
                Move(horizontalValue, jumping);
            }
            else
            {
                animator.SetFloat("xVelocity", 0f);
            }
        }
    }
    void GroundCheck()
    {
        // set isGrounded to  false to avoid endless jumping
        isGrounded = false;
        //checking if Grouncheck obj is colliding with platform
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, 0.2f, groundLayer);
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
        float xVal = dir * MovementSpeed * 100 * Time.fixedDeltaTime;
        if (isRunning)
        {
            xVal *= runSpeedModifier;
        }
        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
        rb.velocity = targetVelocity;
        if (facingRight && dir < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
        }
        else if (!facingRight && dir > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        }
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        #endregion
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, Vector2.up);
    }
}
