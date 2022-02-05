using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Collectable", menuName = "Inventory System/Items/Collectable")]

public class Collectables : ItemObject
{

    public void Awake()
    {
        type = ItemType.Collectables;
    }
}
