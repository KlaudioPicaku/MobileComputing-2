using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waitIntro : MonoBehaviour
{
    public float waitTime = 5f;
    [SerializeField] LevelManager levelManager;
    //[SerializeField] GameObject loadingScreen;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForIntro());
        //loadingScreen.SetActive(false);
    }

 IEnumerator WaitForIntro()
    {
        yield return new WaitForSeconds(waitTime);

        levelManager.LoadLevel("MainMenu");

    }
}
