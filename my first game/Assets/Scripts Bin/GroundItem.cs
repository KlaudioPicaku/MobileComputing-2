using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class GroundItem : MonoBehaviour,ISerializationCallbackReceiver
{
    //static string path;
    //private voidAwake()
    //{
    //    path = Application.persistentDataPath + "/TEMP";
    //    if (!Directory.Exists(path))
    //    {
    //        Directory.CreateDirectory(path);
    //    }
    //}
    public ItemObject item;
    string path;
    //private void Awake()
    //{
    //    Destroy(this.gameObject, 35f);
    //}
    private void Awake()
    {
        path = Application.persistentDataPath + "/TEMP";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        string path2 = Application.persistentDataPath + "/TEMP" + "/" + this.gameObject.name + ".item";
        if (File.Exists(path2))
        {
            this.gameObject.transform.gameObject.SetActive(false);
        }
    }
    public void OnAfterDeserialize()
    {
    }

    public void OnBeforeSerialize()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = item.uiDisplay;
        //EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
    }
    
}
