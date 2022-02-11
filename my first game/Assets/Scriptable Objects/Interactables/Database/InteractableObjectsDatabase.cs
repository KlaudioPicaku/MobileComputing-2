using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Interactable Database", menuName = "Interactable System/Interactable/Database")]

public class InteractableObjectsDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public InteractableObject[] interactables;
    public Dictionary<int, InteractableObject> GetInteractable = new Dictionary<int, InteractableObject>();
    public void OnAfterDeserialize()
    {
        GetInteractable = new Dictionary<int, InteractableObject>();
        for (int i = 0; i < interactables.Length; i++)
        {
            interactables[i].ineractableId = i;
            GetInteractable.Add(i, interactables[i]);
        }
    }

    public void OnBeforeSerialize()
    {
    }
}
