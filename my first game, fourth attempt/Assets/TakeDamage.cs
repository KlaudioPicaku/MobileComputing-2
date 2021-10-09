using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    [SerializeField] HealthBarController health;
    [SerializeField] float currentHealth;
    [SerializeField] float previousHealth;
    [SerializeField] Animator animator;
    [SerializeField] float knockBackSpeed = 2f;
    void Start()
    {
        //health = GetComponent<HealthBarController>();
        currentHealth = health.GetHealth();
        previousHealth = currentHealth;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = health.GetHealth();
        if (currentHealth < previousHealth)
        {
            previousHealth = currentHealth;
            animator.SetBool("KnockBack", true);
            KnockBackPlayer();
        }
    }
    void KnockBackPlayer()
    {
        Vector2 knockBackLocation = new Vector2(transform.position.x , transform.position.y);
        Vector2 knockBackValue = new Vector2((float)-0.35, (float)-0.35);
        Vector2 knockBack = knockBackLocation + knockBackValue;
        transform.position = Vector2.MoveTowards(transform.position, knockBack ,knockBackSpeed);
        if(transform.position.x == knockBack.x && transform.position.y == knockBack.y)
        {
            animator.SetBool("KnockBack", false);
        }
    }
}
