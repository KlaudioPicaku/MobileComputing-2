using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAttack : MonoBehaviour
{
    [SerializeField] Transform goal;
    [SerializeField] Transform attackPoint;
    //[SerializeField] Transform player;
    [SerializeField] Transform goalPost;
    [SerializeField] Animator animator;
    [SerializeField] float coolDown;
    [SerializeField] float timeSinceLast=0;
    [SerializeField] float timeSinceLastSpell = 0;
    [SerializeField] float nextFireTime = 0;
    [SerializeField] float nextSpellTime = 30;
    [SerializeField] float attackRange;
    [SerializeField] HealthBarController playerHealth;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] GameObject special;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] int currentIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        goal = GameObject.FindGameObjectWithTag("Player").transform;
        animator = this.gameObject.GetComponent<Animator>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthBarController>() ;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLast = timeSinceLast + Time.deltaTime;
        timeSinceLastSpell = timeSinceLastSpell + Time.deltaTime;
        goalPost = goal;
        if (timeSinceLast > nextFireTime)
        {
            if (checkRange())
            {
                Debug.Log("attack true?");
                animator.SetTrigger("attack");
            }
        }
        // if (timeSinceLast > nextSpellTime)
        //{
        //    animator.SetTrigger("spell");
        //}

    }
    void Attack()
    {
        Collider2D[] player = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D avatar in player)
        {
            if (Time.time > nextFireTime)
            {
                Debug.Log("We hit" + avatar.name);
                playerHealth.SetDamage(Random.Range(35f,40f));
                nextFireTime = timeSinceLast + coolDown;
                break;
            }

        }
    }


        private bool checkRange()
    {
        if (Vector2.Distance(transform.position, goalPost.position) <= 2.2f) return true;
        return false;
    }
    public void specialAttack()
    {
        nextSpellTime = nextSpellTime + timeSinceLastSpell;
        currentIndex = (currentIndex + 1) % spawnPoints.Length;
        Instantiate(special, spawnPoints[currentIndex]);
    }
}
