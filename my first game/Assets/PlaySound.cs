using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip audioclip;
    public GameObject lightning;
    public void playAudio()
    {
        audio.PlayOneShot(audioclip);
    }
    public void Deactivate()
    {
        if (!audio.isPlaying)
        {
            Destroy(lightning);
        }
    }
}
