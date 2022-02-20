using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightZone : MonoBehaviour
{
    [SerializeField] GameObject wall1;
    [SerializeField] GameObject wall2;
    [SerializeField] GameObject dialogfeed;
    [SerializeField] GameObject boss;
    [SerializeField] GameObject lights1;
    [SerializeField] GameObject dramaticLighting;
    // Start is called before the first frame update
    void Start()
    {
        wall1.SetActive(false);
        wall2.SetActive(false);
        boss.GetComponent<bossAI>().enabled = false;
        lights1.SetActive(true);
        dramaticLighting.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogfeed.GetComponent<DialogFeed>().linesFed)
        {
            wall1.SetActive(true);
            wall2.SetActive(true);
            boss.GetComponent<bossAI>().enabled = true;
            lights1.SetActive(false);
            dramaticLighting.SetActive(true);
        }
        
    }
}
