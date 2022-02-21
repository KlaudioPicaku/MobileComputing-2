using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulateSpaceClick : MonoBehaviour
{
    [SerializeField] GameObject fadeInAndOut;
    [SerializeField] bool faded = false;
    private void Awake()
    {
        fadeInAndOut = GameObject.FindGameObjectWithTag("Fading");
    }
    private void Start()
    {

        fadeInAndOut.SetActive(false);
    }
    public void activate()
    {
        fadeInAndOut.GetComponent<fadeInAndOut>().fadeOutSpeed = 1f;
        fadeInAndOut.GetComponent<fadeInAndOut>().speed = 0f;
        fadeInAndOut.GetComponent<fadeInAndOut>().timeUp = 0f;
        fadeInAndOut.GetComponent<fadeInAndOut>().delayedIn = 0f;
        fadeInAndOut.SetActive(true);
    }
}