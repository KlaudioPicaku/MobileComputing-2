using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class bossAI : MonoBehaviour
{

    public Transform player;
    public float moveSpeed;
    private Rigidbody2D rb;
    private Vector2 movement;
    [SerializeField] Transform attackPoint;
    [SerializeField] Animator animator;
    float distanceBetween;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = this.GetComponent<Rigidbody2D>();
        distanceBetween = Vector2.Distance(transform.position, attackPoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(player.transform.position.x, transform.position.y);

        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //rb.rotation = angle;
        if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1.6f, 1.6f, 1);
        }
        else
        {
            transform.localScale = new Vector3(1.6f, 1.6f, 1);
        }
        movement = direction;
    }
    private void FixedUpdate()
    {
        if (playerNotInRange())
        {
            moveCharacter(movement);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
    void moveCharacter(Vector2 direction)
    {
        if (playerNotInRange())
        {
            animator.SetBool("isWalking",true);
            transform.position = Vector3.Lerp(transform.position, direction, moveSpeed * Time.deltaTime);
        }
       
    }
    private bool playerNotInRange()
    {
        if (Vector2.Distance(attackPoint.position, player.position) > 2.5f) return true;
        return false;
    }
}
