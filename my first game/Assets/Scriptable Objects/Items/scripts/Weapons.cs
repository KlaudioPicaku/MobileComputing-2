using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory System/Items/Weapons")]
public class Weapons : ItemObject
{
    public int attack;
    public int defense;
    public void Awake()
    {
        type = ItemType.Weapons;
    }
}
