using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    private static int ATTACK_MAX_INDEX = 3;
    [SerializeField] Animator animator;
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] LayerMask snakesLayer;
    [SerializeField] LayerMask SpecialLayer;
    [SerializeField] LayerMask bossLayer;
    [SerializeField] int attackIndex = 0;

    private EnemyHealthController enemyHealth;
    private snakeHealth snakeHealth;
    private specialEnemyHealth specialHealth;
    [SerializeField] bossHealth bossHealth;
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
            animator.SetTrigger("attack");
            animator.SetInteger("attackIndex",attackIndex);
            attackIndex = (attackIndex + 1) % ATTACK_MAX_INDEX;
            //animator.SetBool("IsAlerted",true);
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
            foreach (Collider2D enemy in hitEnemies)
            {
                enemyHealth = enemy.GetComponent<EnemyHealthController>();
                break;
            }
            Collider2D[] hitSnakes= Physics2D.OverlapCircleAll(attackPoint.position, attackRange, snakesLayer);
            foreach(Collider2D snake in hitSnakes)
            {
                snakeHealth = snake.GetComponent<snakeHealth>();
                break;
            }
            Collider2D[] hitSpecial = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, SpecialLayer);
            foreach (Collider2D special in hitSpecial)
            {
                specialHealth = special.GetComponent<specialEnemyHealth>();
                break;
            }
            Collider2D[] hitBoss = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, bossLayer);
            foreach (Collider2D boss in hitBoss)
            {
                bossHealth = boss.GetComponent<bossHealth>();
                break;
            }
        }
    }
    public void setEnemyHealth()
    {
        if (enemyHealth != null)
        {
            enemyHealth.setHealth(Random.Range(5f, 20f));
            enemyHealth = null;
        }
        if (snakeHealth != null)
        {
            snakeHealth.setHealth(10f);
        }
        if (enemyHealth != null)
        {
            enemyHealth.setHealth(Random.Range(5f, 25f));
        }
        if (bossHealth != null)
        {
            bossHealth.setHealth(Random.Range(20f, 25f));
            bossHealth = null;
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
