using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableScript : MonoBehaviour
{
    [SerializeField] LayerMask playerMask;
    [SerializeField] GameObject topNotif;
    [SerializeField] DialogueManager dialogBox;
    [SerializeField] Button interactButton;
    [SerializeField] bool playerOutOfRange = true;
    [SerializeField] bool playerInRange = false;
    [SerializeField] bool activates = false;
    private void Awake()
    {
        dialogBox = GameObject.FindGameObjectWithTag("Dialog").GetComponent<DialogueManager>();
        interactButton = GameObject.Find("X").GetComponent<Button>();
        topNotif.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f, playerMask);
        if(colliders.Length>0 && !playerInRange)
        {
            playerInRange = true;
            playerOutOfRange = false;
        }
        if (playerInRange && !activates)
        {
            topNotif.SetActive(true);
            interactButton.interactable = true;
            activates =true;
        }
        if(colliders.Length==0 && !playerOutOfRange)
        {
            topNotif.SetActive(false);
            interactButton.interactable = false;
            playerOutOfRange = true;
            playerInRange = false;
            activates = false;
        }
    }
}
