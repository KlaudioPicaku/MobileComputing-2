using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    public bool isAttacking = false;
    bool attacker = false;
    public void Start()
    {
        animator = GetComponent<Animator>();
        if (isAttacking)
        {
            attacker = true;
        }
        else
        {
            attacker = false;
        }
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

}
