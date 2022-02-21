using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopOnPause : MonoBehaviour
{
    [SerializeField] AudioSource sfx;
    void Update()
    {
        if (Time.timeScale == 0)
        {
            sfx.Pause();
        }
    }
}
