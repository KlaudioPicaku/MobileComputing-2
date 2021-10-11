using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] LayerMask enemyLayer;
    private bool attacker = false;
    public void Start()
    {
        animator = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        Attack(attacker);
    }
    void Attack(bool attack)
    {
        if (attack)
        {
            animator.SetBool("IsAttacking", true);

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
            
            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("We hit" + enemy.name);
                break;
            }
        }
    }
    public void startAttack()
    {
        attacker = true;
        animator.SetBool("IsAttacking", true);
    }
    public void stopAttacking()
    {
        attacker = false;
        animator.SetBool("IsAttacking",false);
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
