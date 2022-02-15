using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] string savePath;
    [SerializeField] SettingsData localSettings;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider soundfxSlider;
    [SerializeField] float musicVolume;
    [SerializeField] GameObject[] musicPlayers;
    [SerializeField] GameObject[] soundEffects;
    [SerializeField] float soundFxVolume;
    public int currentNumberOfActiveScenes = 0;
    public bool updatedSettings=false;
    private void Awake()
    {
        savePath = Application.persistentDataPath + "/SETTINGS";


        //musicVolume = GameObject.FindGameObjectsWithTag("Music").GetComponent<AudioSource>().volume;
        //soundFxVolume = GameObject.FindGameObjectWithTag("SoundFx").GetComponent<AudioSource>().volume;
        musicSlider.value = 1f;
        soundfxSlider.value = 1f;
    }

    // Start is called before the first frame update

    void Start()
    {
        if (File.Exists(savePath + "/Settings.data"))
        {
            FileStream dataStream = new FileStream(savePath + "/Settings.data", FileMode.Open);
            BinaryFormatter converter = new BinaryFormatter();
            localSettings = converter.Deserialize(dataStream) as SettingsData;
            dataStream.Close();
            musicSlider.value = localSettings.musicVolume;
            soundfxSlider.value = localSettings.soundFXvolume;
  
        }

    }

    public void saveSettings()
    {
        if (File.Exists(savePath + "/Settings.data"))
        {
            FileStream dataStream = new FileStream(savePath + "/Settings.data", FileMode.OpenOrCreate);
            BinaryFormatter converter = new BinaryFormatter();
            localSettings.musicVolume = musicSlider.value;
            localSettings.soundFXvolume = soundfxSlider.value;
            converter.Serialize(dataStream, localSettings);
            dataStream.Close();
        }
        else
        {
            Directory.CreateDirectory(savePath);
            FileStream dataStream = new FileStream(savePath + "/Settings.data", FileMode.OpenOrCreate);
            BinaryFormatter converter = new BinaryFormatter();
            localSettings.musicVolume = musicSlider.value;
            localSettings.soundFXvolume = soundfxSlider.value;
            converter.Serialize(dataStream, localSettings);
            dataStream.Close();

        }
        Debug.Log("Settings Saved");

    }
    private void Update()
    {
        if (SceneManager.sceneCount > currentNumberOfActiveScenes)
        {
            updateListeners();
            currentNumberOfActiveScenes = SceneManager.sceneCount;
        }
        if(!updatedSettings){
            updateListeners();
            updatedSettings = true;
        }
        foreach (GameObject g in musicPlayers)
        {
            if (g != null)
            {
                g.GetComponent<AudioSource>().volume = musicSlider.value;
            }
        }
        foreach(GameObject g in soundEffects)
        {
            if (g != null)
            {
                g.GetComponent<AudioSource>().volume = soundfxSlider.value;
            }
        }
    }
    public void updateListeners()
    {
            musicPlayers = GameObject.FindGameObjectsWithTag("Music");
            soundEffects = GameObject.FindGameObjectsWithTag("SoundFx");
    }
}
[System.Serializable]
public class SettingsData
{
    public float musicVolume;
    public float soundFXvolume;
}
