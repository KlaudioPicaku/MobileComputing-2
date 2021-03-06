using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public string savePath;
    public itemsDatabaseObject database;
    public Inventory Container;
    int freeSlots=0;


    public void AddItem(Item _item, int _amount,bool _isSpecial)
    {
        /*if (_item.buffs.Length > 0)
        {
            SetEmptySlot(_item, _amount);
            return;
        }*/

        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].ID == _item.Id)
            {
                Container.Items[i].AddAmount(_amount);
                Container.Items[i].isSpecial = _isSpecial;
                return;
            }
        }
        SetEmptySlot(_item, _amount,_isSpecial);

    }
    public bool isPresent(int itemId)
    {
        bool flag = false;
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].ID == itemId)
            {
                flag = true;
                break;
            }
        }
        return flag;
    }
    public InventorySlot SetEmptySlot(Item _item, int _amount,bool _isSpecial)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].ID <= -1)
            {
                Container.Items[i].UpdateSlot(_item.Id, _item, _amount,_isSpecial);
                return Container.Items[i];
            }
        }
        //set up functionality for full inventory
        return null;
    }

    public void MoveItem(InventorySlot item1, InventorySlot item2)
    {
        InventorySlot temp = new InventorySlot(item2.ID, item2.item, item2.amount,item2.isSpecial);
        item2.UpdateSlot(item1.ID, item1.item, item1.amount,item1.isSpecial);
        item1.UpdateSlot(temp.ID, temp.item, temp.amount, temp.isSpecial);
    }


    public void RemoveItem(Item _item)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].item == _item)
            {
                Container.Items[i].UpdateSlot(-1, null, 0,false);
            }
        }
    }

    [ContextMenu("Save")]
    public void Save()
    {
        //string saveData = JsonUtility.ToJson(this, true);
        //BinaryFormatter bf = new BinaryFormatter();
        //FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        //bf.Serialize(file, saveData);
        //file.Close();

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        stream.Close();
    }
    [ContextMenu("Load")]
    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            //BinaryFormatter bf = new BinaryFormatter();
            //FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            //JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            //file.Close();

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            Inventory newContainer = (Inventory)formatter.Deserialize(stream);
            for (int i = 0; i < Container.Items.Length; i++)
            {
                Container.Items[i].UpdateSlot(newContainer.Items[i].ID, newContainer.Items[i].item, newContainer.Items[i].amount, newContainer.Items[i].isSpecial);
            }
            stream.Close();

        }
        else
        {
            Clear();
        }
    }
    [ContextMenu("Clear")]
    public void Clear()
    {
        Container = new Inventory();
    }
    public int isFree()
    {
        freeSlots = 0;
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].ID < 0)
            {
                freeSlots++;
            }
        }
        return freeSlots;
    }

}
[System.Serializable]
public class Inventory
{
    public InventorySlot[] Items = new InventorySlot[7];
}
[System.Serializable]
public class InventorySlot
{
    public int ID = -1;
    public Item item;
    public int amount;
    public bool isSpecial;
    public InventorySlot()
    {
        ID = -1;
        item = null;
        amount = 0;
        isSpecial = false;
    }
    public InventorySlot(int _id, Item _item, int _amount,bool _isSpecial)
    {
        ID = _id;
        item = _item;
        amount = _amount;
        isSpecial = _isSpecial;
    }
    public void UpdateSlot(int _id, Item _item, int _amount, bool _isSpecial)
    {
        ID = _id;
        item = _item;
        amount = _amount;
        isSpecial = _isSpecial;
    }
    public void AddAmount(int value)
    {
        amount += value;
    }
}