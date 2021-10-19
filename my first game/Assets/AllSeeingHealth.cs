using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllSeeingHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 10f;
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

        if (currentHealth <= 0 && !isDead)
        {
            
            DieEye();
            isDead = true; //avoid animation loop
        }
    }
    void hideElement()
    {
        this.gameObject.GetComponent<Renderer>().enabled = false;
    }
    private void DieEye()
    {
        if (!isDead)
        {
            GameObject a = Instantiate(energyPointsPrefab, GetComponent<Transform>()) as GameObject;
            //Destroy object
            animator.Play("AllSeeing_die");
            animator.SetBool("IsDead", true);
            GetComponent<AllSeeingAI>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            this.enabled = false;
            Destroy(GameObject.FindWithTag("AllSeeingRoot"),10f);
            isDead = true;
        }

    }
    public void setHealth(float health)
    {
        currentHealth = currentHealth - health;
    }
}
