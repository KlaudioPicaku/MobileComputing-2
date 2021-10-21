using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class locatePlayer : MonoBehaviour
{
    [SerializeField] Transform Goal;
    [SerializeField] float triggerRange = 10f;
    [SerializeField] LayerMask playerMask;
    // Start is called before the first frame update
    void Start()
    {

        Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, 20 * triggerRange, playerMask);
        foreach (Collider2D player in players)
        {
            Goal = player.GetComponent<Transform>();
            break;
        }
    }
        // Update is called once per frame
        void Update()
        {
            transform.position = Goal.position;

        }
    public void destroyObject()
    {
        Destroy(this.gameObject);
    }
}
