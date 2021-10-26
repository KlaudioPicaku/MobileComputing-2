using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Note", menuName = "Inventory System/Items/Note")]

public class Note : ItemObject
{
    public string note;
    public void Awake()
    {
        type = ItemType.Note;
    }
}
