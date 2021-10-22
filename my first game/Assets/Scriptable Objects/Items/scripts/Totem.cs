using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Totem", menuName = "Inventory System/Items/Totem")]
public class Totem : ItemObject
{
    public string effects;
    public void Awake()
    {
        type = ItemType.Totem;
    }
}
