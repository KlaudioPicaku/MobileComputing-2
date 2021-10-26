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
    [SerializeField] LayerMask enemy;
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
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 0.55f,enemy);
            knockBackLocation = transform.position;
            foreach(Collider2D enem in enemies){
                if (transform.position.x < enem.transform.position.x)
                {
                 knockBack = knockBackLocation + knockBackValue;
                }
                else
                {

                  knockBack = knockBackLocation - knockBackValue;
                   
                }
            }
            previousHealth = currentHealth;
            animator.SetTrigger("KnockBack");
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