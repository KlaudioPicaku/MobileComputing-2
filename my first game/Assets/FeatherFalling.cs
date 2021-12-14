using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherFalling : MonoBehaviour
{
    float beginning = 0f;
    float everySecond = 0.3f;
    int counter = 0;
    [SerializeField]Transform groundCheckCollider;
    [SerializeField] LayerMask groundlayer;
    bool isGrounded = false;
    private void Start()
    {
        groundCheckCollider = transform.GetChild(1).transform;

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        groundCheck();
        if (!isGrounded)
        {
            if (Time.time > beginning)
            {
                counter++;
                direction();
                beginning += Mathf.RoundToInt(Time.time) + everySecond;

            }
        }
        else{
            transform.GetComponent<ConstantForce2D>().force = new Vector2(0f, 0f);
            transform.GetComponent<Rigidbody2D>().AddTorque(transform.up.y * 0f);
        }
    }
    private void direction()
    {
  
        if (counter % 2 == 0)
        {
            transform.GetComponent<ConstantForce2D>().force = new Vector2(0.008f, 0.001f);
            transform.localScale = new Vector3(1f, 1f, 1f);
            transform.GetComponent<Rigidbody2D>().AddTorque(transform.up.y * 0.005f);
        }
        else
        {
            transform.GetComponent<ConstantForce2D>().force = new Vector2(-0.008f, 0.001f);
            transform.localScale = new Vector3(-1f,1f,1f);
            transform.GetComponent<Rigidbody2D>().AddTorque(transform.up.y * -0.005f);
        }
    }
    private void groundCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, 0.2f, groundlayer);
        if (colliders.Length > 0)
        {
            isGrounded = true;
        }
    }
    private void OnDrawGizmosSelected()
    {
        
        Gizmos.DrawWireSphere(transform.position,0.02f);

    }
}
