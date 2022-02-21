using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossHealth : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;
    [SerializeField] GameObject energyPointsPrefab;
    [SerializeField] float previousHealth;
    [SerializeField] Transform playerLock;
    [SerializeField] string deathAnimation;
    private Animator animator;
    private bool isDead = false;
    [SerializeField] float timeDead = 0f;
    PlayerScript player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        previousHealth = currentHealth;
    }
    private void Update()
    {
        if (!isDead)
        {
            //check if health has changed or not
            if (currentHealth < previousHealth)
            {
                animator.SetTrigger("hurt");
                previousHealth = currentHealth;
                Vector3 positionSpawn = this.gameObject.transform.position;
                Instantiate(energyPointsPrefab, positionSpawn, Quaternion.identity);

            }
            else if (currentHealth <= 0 && !isDead)
            {
                Vector3 positionSpawn = this.gameObject.transform.position;
                Instantiate(energyPointsPrefab, positionSpawn, Quaternion.identity);
                Die();
                isDead = true;
            }
        }
        else
        {
            timeDead += Time.deltaTime;
            if (timeDead >= 10f)
            {
                this.gameObject.transform.gameObject.SetActive(false);
            }

        }
    }
    private void Die()
    {
        if (!isDead)
        {
            animator.Play(deathAnimation);
            animator.SetBool("IsDead", true);
            GetComponent<bossAI>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<bossAttack>().enabled = false;
            player.enemiesKilled.Add(this.gameObject.name);
            //this.enabled = false;
            //Destroy(GameObject.FindWithTag("SkeletonRoot"),10f);
            isDead = true;
        }

    }
    public void setHealth(float health)
    {
        currentHealth = currentHealth - health;
    }
    public float getCurrentHealth()
    {
        return currentHealth;
    }
}
