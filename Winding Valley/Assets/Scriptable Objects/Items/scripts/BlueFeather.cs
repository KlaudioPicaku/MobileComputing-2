using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Blue Feather", menuName = "Inventory System/Items/BlueFeather")]

public class BlueFeather : ItemObject
{

    public void Awake()
    {
        type = ItemType.BlueFeather;
    }
}
