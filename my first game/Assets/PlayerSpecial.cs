using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpecial : MonoBehaviour
{
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] LayerMask snakeLayer;
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange = 2f;
    [SerializeField] Animator animator;
    [SerializeField] EnergyBarController energy;
    [SerializeField] Button button;
    [SerializeField] GameObject specialEffect;
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
    public void specialAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, 2*attackRange, enemyLayer);
        Collider2D[] hitSnakes = Physics2D.OverlapCircleAll(attackPoint.position, 2 * attackRange, snakeLayer);
        if (energy.GetEnergy() == 100)
        {
            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.tag.Equals("Eye"))
                {
                    enemy.GetComponent<AllSeeingHealth>().setHealth(10f);
                }
                else if (enemy.tag.Equals("Enemy"))
                {
                    enemy.GetComponent<EnemyHealthController>().setHealth(30f);
                }

            }
            foreach (Collider2D enemy in hitSnakes)
            {
                if (enemy.tag.Equals("Snake"))
                {
                    enemy.GetComponentInChildren<snakeHealth>().setHealth(10f);
                }
            }

           energy.SetEnergy(-100);
            GameObject a = Instantiate(specialEffect, GetComponent<Transform>()) as GameObject;
        }

    }
    public void startSpecialAttack()
    {
        if (button.interactable)
        {
            animator.Play("specialAttack");
            //specialAttack();
        }
        else
        {
            return;
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
