using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Tent", menuName = "Interactable System/Items/Tent")]

public class Tent : InteractableObject
{
    private void Awake()
    {
        type = InteractableType.Tent;
    }
}
