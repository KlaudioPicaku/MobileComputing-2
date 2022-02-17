using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialElement : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;
    // Update is called once per frame
    void Update()
    {
        Collider2D[] collision = Physics2D.OverlapCircleAll(transform.position, 0.3f, playerLayer);
        if (collision.Length > 0)
        {
            GameObject.FindWithTag("Tutorial").GetComponent<Tutorial>().itemPicked = true;
            Destroy(this.gameObject);
        }
    }
}
