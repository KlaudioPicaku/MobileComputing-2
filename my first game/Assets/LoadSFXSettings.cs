using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LoadSFXSettings : MonoBehaviour
{
    [SerializeField] string savePath;
    [SerializeField] SettingsData localSettings;
    [SerializeField] GameObject soundEffects;
    private void Awake()
    {
        savePath = Application.persistentDataPath + "/SETTINGS";
        if (File.Exists(savePath + "/Settings.data"))
        {
            FileStream dataStream = new FileStream(savePath + "/Settings.data", FileMode.Open);
            BinaryFormatter converter = new BinaryFormatter();
            localSettings = converter.Deserialize(dataStream) as SettingsData;
            dataStream.Close();
            soundEffects.GetComponent<AudioSource>().volume = localSettings.soundFXvolume;
        }
    }
}
