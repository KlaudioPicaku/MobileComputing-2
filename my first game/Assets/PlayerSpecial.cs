using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecial : MonoBehaviour
{
    [SerializeField] EnemyHealthController groundEnemy;
    [SerializeField] AllSeeingHealth flyingEnemy;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] Animator animator;

    // Start is called before the first frame update
 /*   void Start()
    {
        groundEnemy = GetComponent<EnemyHealthController>();
        flyingEnemy = GetComponent<AllSeeingHealth>();
    }*/

    void specialAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, 4*attackRange, enemyLayer);
        foreach(Collider2D enemy in hitEnemies){
            if (enemy.tag.Equals("Eye"))
            {
                flyingEnemy.setHealth(10f);
            }
            else if(enemy.tag.Equals("Ground"))
            {
                groundEnemy.setHealth(30f);
            }

        }

    }
    public void startSpecialAttack()
    {
        animator.SetBool("IsAttacking", true);
        specialAttack();

    }
    public void stopSpecialAttack()
    {
        animator.SetBool("IsAttacking",false);
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, 2*attackRange);
        //Gizmos.DrawWireSphere(attackPoint.position, 3f*attackRange);
    }

}
