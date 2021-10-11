using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Animator animator;
    [SerializeField] Transform attackPoint;
    [SerializeField] Transform Goal;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] bool isAttacking = false;
    bool playerInRange = false;
    [SerializeField] HealthBarController playerHealth;
    public float cooldown = 1.08f;
    private float nextFireTime = 0;
    Vector2 locate;
    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent<Animator>();

        Vector2 position = GameObject.FindWithTag("Player").transform.position;
        locate = position;
    }

    // Update is called once per frame
    void Update()
    {
        CheckRange();
        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            if (isAttacking && playerInRange)
            {
                if (Time.time > nextFireTime)
                {
                    animator.SetBool("IsAttacking", true);
                    Attack();
                }
            }
        }
    }
    void Attack()
    {
            Collider2D[] player = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

            foreach (Collider2D avatar in player)
            {
            if (!(animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) && Time.time> nextFireTime) {
                Debug.Log("We hit" + avatar.name);
                playerHealth.SetDamage(Random.Range(1f, 5f));
                animator.SetBool("IsAttacking", false);
                nextFireTime = Time.time + cooldown;
            }
            break;
           }
    }
    void CheckRange()
    {

        Vector2 skeleton = new Vector2(transform.position.x, transform.position.y);
        float distance = Vector2.Distance(skeleton, Goal.position);
        /*player is within attack distance, enemy can attack*/

        /*check if player is within twice the attack distance and react to it*/
        if (distance <= attackRange)
        {
            playerInRange = true;
            isAttacking = true;
            animator.SetBool("IsMoving",false);
            animator.SetBool("IsAttacking", true);
        }
        if (distance <= (3f * attackRange) && distance > attackRange)
        {
            animator.SetBool("IsMoving", false);
            playerInRange = true;
            transform.localScale = new Vector3(-1, 1, 1);
            animator.SetBool("Alerted", true);

        }
        /*player is nowhere near to be a threat continue patrolling the spots*/
        else
        {
            animator.SetBool("IsMoving",true);
            playerInRange = false;
            animator.SetBool("Alerted", false);
            animator.SetBool("IsAttacking", false);
        }

    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
