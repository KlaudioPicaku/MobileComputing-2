using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayClimb : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip audioclip;
    public void playClimbAudio()
    {
        audio.PlayOneShot(audioclip);
    }
}
