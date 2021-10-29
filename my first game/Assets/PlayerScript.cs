using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public InventoryObject inventory;
  public void OnTriggerEnter2D(Collider2D other)
    {
        var item = other.GetComponent<GroundItem>();
        if (item)
        {
            print("Item in now ?!");
            inventory.AddItem(new Item (item.item), 1);
            Destroy(other.gameObject);
        }
    }
    /*private void Update()
    {
        
    }*/
}
