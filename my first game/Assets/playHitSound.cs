using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playHitSound : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip audioclip;
    public void playHitAudio()
    {
        audio.PlayOneShot(audioclip);
    }
}
