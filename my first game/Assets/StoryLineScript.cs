using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryLineScript : MonoBehaviour
{
    [SerializeField] PlayerScript player;
    [SerializeField] GameObject feedLines;
    [SerializeField] activateCompanion companion;
    [SerializeField] AudioClip thunder;
    [SerializeField] AudioSource audio;
    [SerializeField] bool notPlayed = false;
    [SerializeField] bool notActivated = false;
    [SerializeField] float timeStarted = 0f;
    [SerializeField] float timeThunder = 0f;
    [SerializeField] float timeCompanion = 0f;
    [SerializeField] bool linesFed = false;
    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        companion = GameObject.FindGameObjectWithTag("CompanionActivator").GetComponent<activateCompanion>();
        feedLines.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.cameraFaded)
        {
            timeStarted += Time.deltaTime;
            if (timeStarted >= 2f)
            {
                timeThunder += Time.deltaTime;
                if (!notPlayed)
                {
                    audio.Play();
                    notPlayed = true;
                }

                if (timeStarted >= 5)
                {
                    timeCompanion += Time.deltaTime;
                    if (!notActivated)
                    {
                        companion.ActivateCompanion();
                        notActivated = true;
                    }
                    if (timeCompanion >=4)
                    {
                        if (!linesFed)
                        {
                            linesFed = true;
                            feedLines.SetActive(true);

                        }
                    }

                }
            }
        }
        
    }
}
