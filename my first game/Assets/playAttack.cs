using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playAttack : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip audioclip;
    public void playAttackAudio()
    {
        audio.PlayOneShot(audioclip);
    }
}
