using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Money", menuName = "Inventory System/Items/Money")]
public class Money :ItemObject
{
    public int value;
    public void awake()
    {
        type = ItemType.Money;
    }
}
