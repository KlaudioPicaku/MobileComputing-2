using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Key",menuName ="Inventory System/Items/Keys")]

public class Keys : ItemObject
{
    public void awake()
    {
        type = ItemType.Keys;
    }
}
