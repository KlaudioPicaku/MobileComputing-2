using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeMusic : MonoBehaviour
{
    [SerializeField] AudioSource music;
    [SerializeField] int currentIndex=0;
    [SerializeField] List<AudioClip> trackList;
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0 && !music.isPlaying )
        {
            currentIndex = (currentIndex + 1) % trackList.Count;
            music.clip=trackList[currentIndex];
            music.PlayDelayed(0.5f);
        }
    }
}
