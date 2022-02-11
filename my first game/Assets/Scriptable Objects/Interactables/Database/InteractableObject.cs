using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractableType
{
    Tent,
    GenericSign,
    Companion,
}

public abstract class InteractableObject : ScriptableObject
{
    public Sprite uiDisplay;
    public InteractableType type;
    public int ineractableId;
    public AudioClip dialogVoice;
    public List<string> DialogLines;

}
[System.Serializable]
public class Interactable
{
    public string Name;
    public int ineractableId;
    public InteractableType interactableType;
    public AudioClip dialogVoice;
    public Interactable(InteractableObject _interactable)
    {
        Name=_interactable.name;
        interactableType = _interactable.type;
        ineractableId=_interactable.ineractableId;
        dialogVoice = _interactable.dialogVoice;
}

}

