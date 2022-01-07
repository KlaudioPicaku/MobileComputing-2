using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] float maxHealth = 60f;
    [SerializeField] float currentHealth;
    [SerializeField] GameObject energyPointsPrefab;
    [SerializeField] float previousHealth;
    [SerializeField] Transform playerLock;
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
        if (!isDead) {
            //check if health has changed or not
            if (currentHealth < previousHealth)
            {
                animator.SetTrigger("Hurt");
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
                GameObject.FindWithTag("SkeletonRoot").SetActive(false);
            }

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
}
