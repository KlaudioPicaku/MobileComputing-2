using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    [SerializeField] PlayerScript _player;
    [SerializeField] PauseMenu pauseMenu;
    [SerializeField] GameObject notificationParent;
    [SerializeField] GameObject notification;

    SaveData localSave;
    public void SaveGame()
    {
        if (_player.isNearCheckPoint)
        {
            FileStream dataStream = new FileStream(Application.persistentDataPath + "/save.data", FileMode.Create);
            BinaryFormatter converter = new BinaryFormatter();
            _player.setToSave();
            converter.Serialize(dataStream, _player.toBeSaved);
            _player.saveInventory();
            dataStream.Close();
            GameObject temp = Instantiate(notification, notificationParent.transform);
            temp.GetComponentInChildren<Text>().text = "Saving...";
            pauseMenu.Resume();
        }
        else
        {
            GameObject temp = Instantiate(notification, notificationParent.transform);
            temp.GetComponentInChildren<Text>().text ="You have to be near a checkpoint to save the game";
            pauseMenu.Resume();
        }
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/save.data"))
        {
            // File exists 
            FileStream dataStream = new FileStream(Application.persistentDataPath + "/save.data", FileMode.Open);

            BinaryFormatter converter = new BinaryFormatter();
            localSave = converter.Deserialize(dataStream) as SaveData;
            //LoadScene();
            _player = GameObject.Find("player").GetComponent<PlayerScript>();
            _player.toBeSaved = localSave;
            _player.resetSave();
            SceneManager.LoadScene(_player.toBeSaved.sceneName);
            //SceneManager.LoadScene("Persistent");
            _player.loadInventory();
            dataStream.Close();

            if (pauseMenu.isActiveAndEnabled)
            {
                pauseMenu.Resume();
            }
            
        }
        else
        {
            // File does not exist
            Debug.Log("Save file not found in " + Application.persistentDataPath + "/save.data");
        }
    }
    private IEnumerator LoadScene()
    {
        // Start loading the scene
        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync("Presistent", LoadSceneMode.Single);
        // Wait until the level finish loading
        while (!asyncLoadLevel.isDone)
            yield return null;
        // Wait a frame so every Awake and Start method is called
        yield return new WaitForEndOfFrame();
    }
}
[System.Serializable]
public struct SerializableVector3
{
    public float x;
    public float y;
    public float z;
}
[System.Serializable]
public class SaveData
{
    public string sceneName;
    public float health;
    public float energy;

    public SerializableVector3 spawnPosition;
}