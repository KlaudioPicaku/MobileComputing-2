using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;
    [SerializeField] bool interacted = false;
    // Update is called once per frame
    void Update()
    {
        if (!interacted)
        {
            Collider2D[] player = Physics2D.OverlapCircleAll(transform.position, 3f, playerLayer);
            if (player.Length > 0)
            {
                SceneManager.LoadSceneAsync(this.gameObject.scene.buildIndex + 1, LoadSceneMode.Additive);
                interacted = true;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, 3f);
    }
}
