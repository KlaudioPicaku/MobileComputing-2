using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Energy", menuName = "Inventory System/Items/Energy")]
public class EnergyPotion : ItemObject 
{
    public int restoreEnergyValue;
    public void Awake()
    {
        type = ItemType.Energy;
    }
}
