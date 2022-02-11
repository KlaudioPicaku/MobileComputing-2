using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogFeed : MonoBehaviour
{
    [SerializeField] List<string> linesToFeed;
    [SerializeField] LayerMask playerMask;
    [SerializeField] AudioClip mainCharacterVoice;
    [SerializeField] bool linesFed = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collider2D[] other = Physics2D.OverlapCircleAll(transform.position, 3f, playerMask);
        if (other.Length > 0)
        {
            foreach (Collider2D item in other)
            {
                item.gameObject.GetComponent<PlayerScript>().dialogToPassOver = linesToFeed;
                item.gameObject.GetComponent<PlayerScript>().dialog.typingClip = mainCharacterVoice;
                item.gameObject.GetComponent<PlayerScript>().playThoughts();
                item.gameObject.GetComponent<PlayerScript>().dialog.PlayDialogue1();
                linesFed = true;
                break;
            }
        }
        if (linesFed)
        {
            this.gameObject.SetActive(false);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 3f);
    }
}
