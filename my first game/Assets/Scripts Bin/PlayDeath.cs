using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDeath : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip audioclip;
    public void playDeathAudio()
    {
        audio.PlayOneShot(audioclip);
    }
}
