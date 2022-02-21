using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateCompanion : MonoBehaviour
{
    [SerializeField] GameObject companion;
    [SerializeField] float timeToActivateScript = 0f;
    [SerializeField] float timeToSpeech = 0f;
    [SerializeField] bool activated = false;
    [SerializeField] bool scriptIsOn = false;
    //[SerializeField] GameObject[] companionSpeeches;
    [SerializeField] GameObject blueFeather;
    [SerializeField] GameObject speech1;
    [SerializeField] GameObject speech2;
    [SerializeField] GameObject invisibleWall;

    private void Awake()
    {
        companion.gameObject.SetActive(false);
        companion.GetComponent<Companion>().enabled = false;
        speech1.SetActive(false);
        speech2.SetActive(false);
        //foreach (GameObject item in companionSpeeches)
        //{
        //    item.SetActive(false);
        //}
        blueFeather.SetActive(false);
    }
    private void Update()
    {
        if (activated)
        {
            timeToActivateScript += Time.deltaTime;
            timeToSpeech += Time.deltaTime;
            if (timeToActivateScript >= 9 && timeToActivateScript<10)
            {

                companion.SetActive(true);
                blueFeather.SetActive(true);

            }
            if(timeToActivateScript>=12 && timeToActivateScript < 13)
            {
                companion.GetComponent<Companion>().enabled=true;
                activateSpeech1();
            }
            if(timeToActivateScript>=14 && timeToActivateScript < 15)
            {
                activateSpeech2();
            }
            if (timeToActivateScript >= 60)
            {
                activated = false;
                companion.GetComponent<Companion>().deactivate = true;
            }
            if (timeToSpeech >= 60)
            {
                invisibleWall.SetActive(false);
            }

        }
    }
    private void activateSpeech1()
    {
        speech1.SetActive(true);
    }
    private void activateSpeech2()
    {
        speech2.SetActive(true);
    }
    public void ActivateCompanion()
        {
        companion.SetActive(true);
        activated = true;
        } 
    public void DeactivateCompanion()
    {
        companion.SetActive(false);
    }
}
