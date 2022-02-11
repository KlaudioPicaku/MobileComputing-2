using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Companion", menuName = "Interactable System/Companion")]

public class CompanionInteractable : InteractableObject
{
    private void Awake()
    {
        type = InteractableType.Companion;
    }
}
