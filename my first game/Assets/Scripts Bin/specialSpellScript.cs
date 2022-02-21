using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class specialSpellScript : MonoBehaviour
{
    [SerializeField] LayerMask playerMask;
    [SerializeField] Transform attackPoint;
    public void special()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPoint.position,0.3f,playerMask);
        if (colliders.Length > 0)
        {
            foreach (var item in colliders)
            {
                item.gameObject.GetComponent<HealthBarController>().SetDamage(30f);
                break;
            }
        }
        Destroy(this.gameObject.transform.parent.gameObject,0.8f);
    }
}
