using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayClimb2 : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip audioclip;
    public void playClimbAudio2()
    {
        audio.PlayOneShot(audioclip);
    }
}
