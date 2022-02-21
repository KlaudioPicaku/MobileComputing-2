using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killZoneScript : MonoBehaviour
{
    [SerializeField] PlayerScript player;
    [SerializeField] LayerMask playerMask;
    [SerializeField] Vector2 center;

    [SerializeField] Vector2 boxSize;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player").gameObject.GetComponent<PlayerScript>();
        center = new Vector2(transform.position.x, transform.position.y);
        boxSize = this.gameObject.GetComponent<BoxCollider2D>().size;
    }

    // Update is called once per frame
    public void OnTriggerEnter2D(Collider2D other)
    {
        Collider2D[] collider = Physics2D.OverlapBoxAll(center, boxSize,0f,playerMask.value) ;
        if (collider.Length > 0)
        {
            player.killPlayer();
        }
    }
    }
