using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] InventoryObject inventory;
    [SerializeField] InventoryObject expandedInventory;
  public void OnTriggerEnter2D(Collider2D other)
    {
        var item = other.GetComponent<GroundItem>();
        if (item )
        {
            inventory.AddItem(new Item (item.item), 1);
            Destroy(other.gameObject);
        }
        else
        {
            expandedInventory.AddItem(new Item(item.item), 1);
            Destroy(other.gameObject);
        }
    }
    /*private void Update()
    {
        
    }*/
}
