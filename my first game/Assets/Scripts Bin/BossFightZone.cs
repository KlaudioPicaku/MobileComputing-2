using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightZone : MonoBehaviour
{
    [SerializeField] GameObject wall1;
    [SerializeField] GameObject wall2;
    [SerializeField] GameObject dialogfeed;
    [SerializeField] GameObject dialogToFeed;
    [SerializeField] GameObject finalMessage;
    [SerializeField] GameObject boss;
    [SerializeField] GameObject lights1;
    [SerializeField] GameObject dramaticLighting;
    [SerializeField] AudioClip bossFightSong;
    [SerializeField] AudioSource music;
    [SerializeField] bool activated = false;
    [SerializeField] bool activated2 = false;
    [SerializeField] bool activated3 = false;
    // Start is called before the first frame update
    void Start()
    {
        finalMessage.SetActive(false);
        dialogToFeed.SetActive(false);
        wall1.SetActive(false);
        wall2.SetActive(false);
        boss.GetComponent<bossAI>().enabled = false;
        lights1.SetActive(true);
        dramaticLighting.SetActive(false);
        music = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
     
    }

    // Update is called once per frame
    void Update()
    {
        if (!activated && dialogfeed.GetComponent<DialogFeed>().linesFed)
        {
            activated = true;
            wall1.SetActive(true);
            wall2.SetActive(true);
            boss.GetComponent<bossAI>().enabled = true;
            lights1.SetActive(false);
            dramaticLighting.SetActive(true);
            music.gameObject.transform.parent.gameObject.GetComponent<fadeMusic>().enabled = false;
            music.clip = bossFightSong;
            music.Play();
        }
        if (!activated2 && boss.GetComponent<bossHealth>().getCurrentHealth() <= 0)
        {
            activated2 = true;
            ResetScene();
        }
        
    }
    public void ResetScene()
    {

        wall1.SetActive(false);
        wall2.SetActive(false);
        lights1.SetActive(true);
        dramaticLighting.SetActive(false);
        music.gameObject.transform.parent.gameObject.GetComponent<fadeMusic>().enabled = true;
        music.Pause();
        dialogfeed.SetActive(true);
        dialogfeed.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
        finalMessage.SetActive(true);
    }
    public void loadLeaderBoard()
    {
        GameObject.FindGameObjectWithTag("LeaderBoard").GetComponent<LevelComplete>().sendData();
        GameObject.FindGameObjectWithTag("LeaderBoard").GetComponent<LevelComplete>().activateLeader();
    }
}
