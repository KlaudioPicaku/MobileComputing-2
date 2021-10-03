using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2Dcontroller : MonoBehaviour
{
    public float MovementSpeed = 1;
    Rigidbody2D rb;
    public Joystick joystick;
    float horizontalValue;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        horizontalValue = joystick.Horizontal;
    }
    void FixedUpdate()
    {
        Move(horizontalValue);
    }
    void Move(float dir)
    {
        float xVal = dir * MovementSpeed * Time.deltaTime;
        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
        rb.velocity = targetVelocity;

    }
}
