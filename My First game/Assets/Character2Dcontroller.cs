using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2Dcontroller : MonoBehaviour
{
    public float MovementSpeed = 1;
    public float JumpForce = 1;
    RigidBody2d rb;
    void Start()
    {
        rb = GetComponent<RigidBody2d>();
    }
    void Update()
    {
        
    }
    void FixedUpdate()
    {
       
    }
}
