using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2Dcontroller : MonoBehaviour
{
    public float MovementSpeed = 1;
    Rigidbody2D rb;
    public Joystick joystick;
    float horizontalValue;
    float runSpeedModifier = 2f;
    bool isRunning = false;
    bool facingRight = true;
    Animator animator;
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
        if (Mathf.Abs(joystick.Horizontal) >= 0.2)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }
    void FixedUpdate()
    {
        Move(horizontalValue);
    }
    void Move(float dir)
    {

        float xVal = dir * MovementSpeed* 100 * Time.deltaTime;
        if (isRunning)
        {
            xVal *= runSpeedModifier;
        }
        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
        rb.velocity = targetVelocity;
        if(facingRight && dir < 0)
        {
            transform.localScale = new Vector3(-2, 2, 2);
            facingRight = false;
        }
        else if(!facingRight && dir > 0)
        {
            transform.localScale = new Vector3(2, 2, 2);
            facingRight = true;
        }
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
    }
}
