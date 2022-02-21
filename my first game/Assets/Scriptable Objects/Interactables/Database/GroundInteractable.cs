using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundInteractable : MonoBehaviour, ISerializationCallbackReceiver
{
    public InteractableObject item;
    public void OnAfterDeserialize()
    {
    }

    public void OnBeforeSerialize()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = item.uiDisplay;
        //EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
    }
}
