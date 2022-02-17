using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWave : MonoBehaviour
{
    [SerializeField] GameObject enemyToSpawn;
    [SerializeField] float timeStarted;
    [SerializeField] int timeSpawn;
    [SerializeField] int nextTime;
    [SerializeField] LayerMask playerMask;
    [SerializeField] bool activated=false;
    [SerializeField] int spawned = 0;
    [SerializeField] Transform[] pointsToSpawn;
    [SerializeField] int currentIndex = 0;

    private void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {

        Collider2D[] item = Physics2D.OverlapCircleAll(this.transform.position, 4f, playerMask);
        if (item.Length > 0 && !activated)
        {
            activated = true;
            GameObject.FindGameObjectWithTag("Tutorial").GetComponent<Tutorial>().nearBush=true;
        }
        if (activated)
        {
            Debug.Log("Wave 1");
            timeStarted += Time.deltaTime;
            timeSpawn = Mathf.RoundToInt(timeStarted);
            if (timeSpawn>nextTime && spawned < 10)
            {
                nextTime = timeSpawn+2;
                GameObject enemy = Instantiate(enemyToSpawn, pointsToSpawn[currentIndex]) as GameObject;
                enemy.transform.position = pointsToSpawn[currentIndex].position;
                enemy.transform.localScale = new Vector3(-1.5f,1.5f);
                currentIndex = (currentIndex + 1) % pointsToSpawn.Length;
                spawned++;
            }
            if (spawned == 10)
            {
                GameObject.FindGameObjectWithTag("Tutorial").GetComponent<Tutorial>().waveComplete = true;
                activated = false;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, 4f);
    }
}
