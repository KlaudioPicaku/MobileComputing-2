using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnloadPreviousLvl : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;
    [SerializeField] bool interacted = false;
    [SerializeField] float leftBound;
    [SerializeField] Camera camera;
    // Update is called once per frame
    private void Awake()
    {
        camera = Camera.main;
    }
    void Update()
    {
        if (!interacted)
        {
            Collider2D[] player = Physics2D.OverlapCircleAll(transform.position, 3f, playerLayer);
            if (player.Length > 0)
            {
                SceneManager.UnloadSceneAsync(this.gameObject.scene.buildIndex -1);
                interacted = true;
                camera.gameObject.GetComponent<BindCamera>().bindThisCamera(leftBound);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, 3f);
    }
}
