using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GroundItem : MonoBehaviour,ISerializationCallbackReceiver
{
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
