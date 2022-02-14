using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playAnotherSound : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip audioclip;
    public void playAlertAudio()
    {
        audio.PlayOneShot(audioclip);
    }
}
