using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Red Feather", menuName = "Inventory System/Items/RedFeather")]

public class RedFeather : ItemObject
{

    public void Awake()
    {
        type = ItemType.BlueFeather;
    }
}
