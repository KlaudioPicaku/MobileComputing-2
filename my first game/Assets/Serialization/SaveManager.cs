using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;


public class SaveManager : MonoBehaviour
{
    public SaveData activeSave;
    public void Save()
    {
        string dataPath = Application.persistentDataPath;

        var serializer = new XmlSerializer(typeof(SaveData));

        var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".save", FileMode.Create);
        serializer.Serialize(stream, activeSave);
        stream.Close();
        Debug.Log("Save");

    }
    public void Load()
    {
        string dataPath = Application.persistentDataPath;
        if(System.IO.File.Exists(dataPath + "/" + activeSave.saveName + ".save"))
        {
            var serializer = new XmlSerializer(typeof(SaveData));
            var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".save", FileMode.Open);
            activeSave = serializer.Deserialize(stream)as SaveData;
            stream.Close();
            Debug.Log("Load");
        }
    }
}
[System.Serializable]
public class SaveData
{
    public string saveName;

    public Vector3 spawnPosition;
    


}
