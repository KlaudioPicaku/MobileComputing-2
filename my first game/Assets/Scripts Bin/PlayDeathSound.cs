using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDeathSound : MonoBehaviour
{
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioSource music;
    void Start()
    {
        music = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        music.gameObject.transform.parent.gameObject.GetComponent<fadeMusic>().enabled = false;
        music.clip=deathSound;
        music.Play();
    }
}
