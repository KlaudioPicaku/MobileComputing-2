using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound4 : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audio;
    public AudioClip[] audioclip;
    public int currentIndex = 0;
    private static int MAX_INDEX = 3;
    public void playSwishAudio()
    {
        currentIndex = (currentIndex + 1) % MAX_INDEX;
        audio.PlayOneShot(audioclip[currentIndex]);
    }
}
