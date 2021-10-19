using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] float maxHealth = 60f;
    [SerializeField] float currentHealth;
    [SerializeField] GameObject energyPointsPrefab;
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
            Instantiate(energyPointsPrefab, GetComponent<Transform>());
            
        }
        else if (currentHealth <= 0 && !isDead)
        {
            GameObject b = Instantiate(energyPointsPrefab, GetComponent<Transform>()) as GameObject;
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
            Destroy(GameObject.FindWithTag("SkeletonRoot"),10f);
            isDead = true;
        }

    }
    public void setHealth(float health)
    {
        currentHealth = currentHealth - health;
    }
}
