using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip audioclip;
    public void playAudio()
    {
        audio.PlayOneShot(audioclip);
    }
}
