using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    [SerializeField] HealthBarController health;
    [SerializeField] float currentHealth;
    [SerializeField] float previousHealth;
    [SerializeField] Animator animator;
    [SerializeField] float knockBackSpeed = 0.05f;
    [SerializeField] Vector2 knockBackLocation;
    [SerializeField] Vector2 knockBackValue;
    [SerializeField] Vector2 knockBack;
    void Start()
    {
        //health = GetComponent<HealthBarController>();
        currentHealth = health.GetHealth();
        previousHealth = currentHealth;
        animator = GetComponent<Animator>();
        knockBackLocation = new Vector2(transform.position.x, transform.position.y);
        knockBackValue = new Vector2((float)-0.35, (float)-0.35);
      
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = health.GetHealth();
        if (currentHealth < previousHealth)
        {
            knockBackLocation = transform.position;
            knockBack = knockBackLocation + knockBackValue;
            previousHealth = currentHealth;
            animator.Play("player_hurt");
            //KnockBackPlayer();
        }
    }
    void StopKnockBack()
    {
        // animation handler
       animator.SetBool("KnockBack", false);
    }
    void KnockBackPlayer()
    {
        /* change player location based on damage inflicted*/

        transform.position = Vector2.MoveTowards(transform.position, knockBack ,Time.deltaTime*knockBackSpeed);
       
    }
}
