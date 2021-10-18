using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPoints : MonoBehaviour
{
    [SerializeField] EnergyBarController energy;
    [SerializeField] LayerMask player;
    [SerializeField] Transform Goal;
    [SerializeField] Transform triggerPoint;
    [SerializeField] float triggerRange = 0.25f;
    [SerializeField] float automaticLockOn = 10f;
    [SerializeField] float smoothSpeed = 0.0125f;
    [SerializeField] Vector3 offset;
    bool energyIsSpent = false;
    public float energyGain = 10f;
    // Start is called before the first frame update
    void Update()
    {
        if (!energyIsSpent)
        {
            checkRange();
        }
    }
    void checkRange()
    {
        print("should be testing range");
        float distance = Vector2.Distance(triggerPoint.position, Goal.position);
        if(distance<= triggerRange)
        {
            Vector3 offsetPosition = Goal.position + offset;

            //verify target position out of bounds or not
            Vector3 smoothPosition = Vector3.Lerp(transform.position, offsetPosition, 2 * smoothSpeed);
            transform.position = smoothPosition;
            transform.position = Vector3.Lerp(transform.position, Goal.position, 2 * smoothSpeed);
            LockOn(!energyIsSpent);
        }
    }
    void LockOn(bool IsSpent)
    {
        if (IsSpent)
        {


            if (transform.position.x <= Goal.position.x + 0.0003 || 
                transform.position.x >= Goal.position.x - 0.0003 )
            {
                print("Should be getting energy");
                Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, triggerRange, player);
                foreach (Collider2D player in players)
                {
                    energy.GetComponent<EnergyBarController>().SetGain(10f);
                    break;
                }
                energyIsSpent = true;
            }
        }
        else
        {
            return;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (triggerPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(transform.position, triggerRange);
    }
}
