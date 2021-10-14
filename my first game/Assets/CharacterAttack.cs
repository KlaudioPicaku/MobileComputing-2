using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] LayerMask enemyLayer;
    private EnemyHealthController enemyHealth;
    private bool attacker = false;
    public void Start()
    {
        animator = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        Attack(attacker);
    }
    void NotAlerted()
    {
        animator.SetBool("IsAlerted", false);
    }
    void Alerted()
    {
        animator.SetBool("IsAlerted", true);
    }
    void Attack(bool attack)
    {
        if (attack)
        {
            animator.SetBool("IsAttacking", true);
            //animator.SetBool("IsAlerted",true);

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("We hit" + enemy.name);
                enemyHealth = enemy.GetComponent<EnemyHealthController>();
                break;
            }
        }
    }
    public void setEnemyHealth()
    {
        if (enemyHealth != null)
        {
            enemyHealth.setHealth(Random.Range(5f, 20f));
        }
        return;
    }
    public void startAttack()
    {
        attacker = true;
        animator.SetBool("IsAttacking", true);

    }
    public void stopAttacking()
    {
        attacker = false;
        animator.SetBool("IsAttacking", false);
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
