using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravityController : MonoBehaviour
{
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Rigidbody2D gravity;
    [SerializeField]bool gravitySet = false;
    // Start is called before the first frame update
    void Start()
    {
        gravity = GetComponent<Rigidbody2D>();
        gravity.gravityScale = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        //if (!loadingScreen.activeInHierarchy && !gravitySet)
        //{
        //    gravity.gravityScale = 1f;
        //    gravitySet = true;
        //}

    }
}
