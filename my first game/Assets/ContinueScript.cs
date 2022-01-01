using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueScript : MonoBehaviour
{
    [SerializeField] GameObject dialog;
    private void Start()
    {
        dialog.SetActive(false);
    }
    public void PlayNewGame()
    {
        if (File.Exists(Application.persistentDataPath + "/save.data"))
        {
            dialog.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("Persistent");
        }

    }
    public void confirmNewGame()
    {
        if (File.Exists(Application.persistentDataPath + "/save.data"))
        {
            File.Delete(Application.persistentDataPath + "/save.data");
            SceneManager.LoadScene("Persistent");
        }
        else
        {
            Debug.Log("Error findig save file");
        }
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/save.data"))
        {
            // File exists 
            SceneManager.LoadScene("Persistent");
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
