using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayWooshSound : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audio;
    public AudioClip audioclip;
    public void playWoosh()
    {
        audio.PlayOneShot(audioclip);
    }
}
