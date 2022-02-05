using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    None,
    Food,
    Collectables,
    Keys,
    Energy,
    Weapons,
    Money,
    Totem,
    Note,
    BlueFeather,
    RedFeather,
}
public abstract class ItemObject : ScriptableObject
{
    public int Id;
    public Sprite uiDisplay;
    public ItemType type;
    [TextArea(15, 20)]
    public string description;
    public bool isSpecial;
}
[System.Serializable]
public class Item
{
    public string Name;
    public int Id;
    public bool isSpecial;
    public ItemType itemType;
    public Item(ItemObject item)
    {
        Name = item.name;
        Id = item.Id;
        isSpecial = item.isSpecial;
        itemType = item.type;
    }
}