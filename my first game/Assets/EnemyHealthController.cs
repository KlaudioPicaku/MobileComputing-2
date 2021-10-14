using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] float maxHealth = 60f;
    [SerializeField] float currentHealth;
    [SerializeField] float previousHealth;
    private Animator animator;
    private bool isDead = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        previousHealth = currentHealth;
    }
    private void Update()
    {
        //check if health has changed or not
        if (currentHealth < previousHealth)
        {
            animator.SetTrigger("Hurt");
            previousHealth = currentHealth;
        }
        else if (currentHealth <= 0 && !isDead)
        {
            Die();
            isDead = true;
        }
    }
    private void Die()
    {
        if (!isDead)
        {
            animator.Play("basic_skeleton_dead");
            animator.SetBool("IsDead",true);
            GetComponent<EnemyAI>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<EnemyAttack>().enabled = false;
            this.enabled = false;
            Destroy(GameObject.Find("Waypoints"),10);
            Destroy(gameObject, 10);
            isDead = true;
        }

    }
    public void setHealth(float health)
    {
        currentHealth = currentHealth - health;
    }
}
