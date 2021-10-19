using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpecial : MonoBehaviour
{
    [SerializeField] EnemyHealthController groundEnemy;
    [SerializeField] AllSeeingHealth flyingEnemy;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange = 2f;
    [SerializeField] Animator animator;
    [SerializeField] EnergyBarController energy;
    [SerializeField] Button button;
   /* private void Start()
    {
        energy = GetComponent<EnergyBarController>().GetComponent<Slider>();
    }*/
    private void Update()
    {
        if(energy.GetEnergy() < 100)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }
    void specialAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, 2*attackRange, enemyLayer);
        if (energy.GetEnergy() == 100)
        {
            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.tag.Equals("Eye"))
                {
                    flyingEnemy.setHealth(10f);
                }
                else if (enemy.tag.Equals("Ground"))
                {
                    groundEnemy.setHealth(30f);
                }

            }
        }

    }
    public void startSpecialAttack()
    {
        if (button.interactable)
        {
            animator.SetBool("IsAttacking", true);
            specialAttack();
        }

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
