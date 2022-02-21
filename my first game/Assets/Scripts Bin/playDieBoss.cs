using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playDieBoss : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip audioclip;
    public void playDieBossAudio()
    {
        audio.PlayOneShot(audioclip);
    }
}
