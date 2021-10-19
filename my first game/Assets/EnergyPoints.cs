using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPoints : MonoBehaviour
{
    
    EnergyBarController energy;
    [SerializeField] Transform Goal;
    [SerializeField] LayerMask playerMask;
    [SerializeField] float triggerRange = 2f;
    [SerializeField] float smoothSpeed = 0.125f;
    [SerializeField] Vector3 offset;
    bool energyIsSpent = false;
    public float energyGain = 10f;
    // Start is called before the first frame update
    private void Start()
    {
        Vector3 throwVal;
        //vector to move energy point dropped by enemy
        if (transform.localScale.x < 0)
        {
            throwVal = new Vector3(transform.position.x - 3, transform.position.y + 2, transform.position.z);
        }
        else
        {
           throwVal = new Vector3(transform.position.x + 3, transform.position.y + 2, transform.position.z);
        }

        Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, 20 * triggerRange, playerMask);
        foreach (Collider2D player in players)
        {
            Goal = player.GetComponent<Transform>();
            energy = player.GetComponent<EnergyBarController>();
            transform.position = Vector3.Lerp(transform.position, throwVal, 5 * smoothSpeed* Time.deltaTime);
        }
    }
    void Update()
    {
        if (Goal != null)
        {
            float distance = Vector3.Distance(Goal.position, transform.position);
            //verify target position out of bounds or not
            if (distance <= 2 * triggerRange)
            {
                Vector3 offsetPosition = Goal.position + offset;

                Vector3 smoothPosition = Vector3.Lerp(transform.position, offsetPosition, 5 * smoothSpeed * Time.deltaTime);
                transform.position = smoothPosition;
                transform.position = Vector3.Lerp(transform.position, Goal.position, 5 * smoothSpeed * Time.deltaTime);
            }
            if (!energyIsSpent && distance <= 0.025 * triggerRange)
            {
                LockOn();
            }
        }
        else
        {
            Destroy(this.gameObject,5f);
        }
    }
    //locks on player and gives him 10 energy points
    void LockOn()
    {

        if (!energyIsSpent)
        {


            if (transform.position.x >= Goal.position.x + 0.003 || 
                transform.position.x <= Goal.position.x - 0.003 )
            {

                Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, triggerRange, playerMask);
                foreach (Collider2D player in players)
                {
                    player.GetComponent<EnergyBarController>().SetGain(10f);
                    energyIsSpent = true;
                    GetComponent<Animator>().Play("energyCollected");
                    break;
                }
            }
        }
        else
        {
            return;
        }
    }
    public void destroyEnergy()
    {
        Destroy(this.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        if (transform.position == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(transform.position, 2*triggerRange);
    }
}
