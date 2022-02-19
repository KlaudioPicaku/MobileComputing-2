using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateCompanion : MonoBehaviour
{
    [SerializeField] GameObject companion;
    [SerializeField] float timeToActivateScript = 0f;
    [SerializeField] bool activated = false;
    [SerializeField] bool scriptIsOn = false;
    [SerializeField] GameObject[] companionSpeeches;
    [SerializeField] GameObject blueFeather;

    private void Awake()
    {
        companion.GetComponent<Companion>().enabled = false;
        foreach (GameObject item in companionSpeeches)
        {
            item.SetActive(false);
        }
        blueFeather.SetActive(false);
    }
    private void Update()
    {
        if (activated)
        {
            timeToActivateScript += Time.deltaTime;
            if (timeToActivateScript >= 9 && timeToActivateScript<10)
            {
                
                companion.GetComponent<Companion>().enabled = true;
                activateSpeech();
                blueFeather.SetActive(true);

            }
            if (timeToActivateScript >= 60)
            {
                activated = false;
                companion.GetComponent<Companion>().deactivate = true;
            }

        }
    }
    private void activateSpeech()
    {
        foreach (GameObject item in companionSpeeches)
        {
            item.SetActive(true);
        }
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
