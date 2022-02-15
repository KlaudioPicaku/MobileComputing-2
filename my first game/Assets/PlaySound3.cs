using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound3 : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip[] audioclip;
    public int currentIndex = 0;
    private static int MAX_INDEX = 3;
    public void playHurtAudio()
    {
        currentIndex = (currentIndex + 1) % MAX_INDEX;
        audio.PlayOneShot(audioclip[currentIndex]);
    }
}
