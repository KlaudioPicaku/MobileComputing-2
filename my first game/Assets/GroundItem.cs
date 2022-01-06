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

    //private void Awake()
    //{
    //    Destroy(this.gameObject, 35f);
    //}

    public void OnAfterDeserialize()
    {
    }

    public void OnBeforeSerialize()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = item.uiDisplay;
        EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
    }
    
}
