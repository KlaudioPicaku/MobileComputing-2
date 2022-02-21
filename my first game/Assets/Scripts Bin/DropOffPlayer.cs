using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOffPlayer : MonoBehaviour
{
    [SerializeField] GameObject Camera;
    [SerializeField] CameraFollow script;
    [SerializeField] LayerMask playerMask;
    private void Awake()
    {
        Camera = GameObject.Find("Main Camera");
        script=Camera.GetComponent<CameraFollow>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 3f, playerMask);
        if (colliders.Length > 0)
        {
            script.enabled = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 3f);
    }
}
