using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //static int MAX_HOTBAR = 7;
    //static int MAX_EXPANDED_CAPACITY = 49;
    [SerializeField] InventoryObject inventory;
    [SerializeField] ExpandedInventoryObject expandedInventory;
    [SerializeField] LayerMask layerMask;
    int freeSlotsHotBar = 0;
    int freeSlotsExpanded = 0;
    private void Start()
    {
        freeSlotsHotBar = inventory.isFree();
        freeSlotsExpanded = expandedInventory.isFree();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        bool isPresent = false;
        bool isPresentExp = false;
        bool flag = true;
        var item = other.GetComponent<GroundItem>();

        if (other.gameObject.layer == 8)
        {
            for (int i = 0; i < inventory.Container.Items.Length; i++)
            {
                if (inventory.Container.Items[i].ID == item.item.Id)
                {
                    isPresent = true;
                    flag = false;
                    break;
                }

            }
            if (flag && !isPresent)
            {
                for (int i = 0; i < expandedInventory.Container.Items.Length; i++)
                {
                    if (expandedInventory.Container.Items[i].ID == item.item.Id)
                    {
                        isPresentExp = true;
                        flag = false;
                        break;
                    }
                }
            }
            if (isPresent)
            {
                inventory.AddItem(new Item(item.item), 1);
                Destroy(other.gameObject);
            }
            else if (isPresentExp)
            {

                expandedInventory.AddItem(new Item(item.item), 1);
                Destroy(other.gameObject);
                print("added to expanded");
            }
            else
            {
                if (freeSlotsHotBar == 0 && freeSlotsExpanded > 0)
                {
                    expandedInventory.AddItem(new Item(item.item), 1);
                    Destroy(other.gameObject);
                    print("space in expanded");
                }
                else if (freeSlotsExpanded == 0 && freeSlotsHotBar == 0)
                {
                    print("No more space left");
                }
                else
                {
                    inventory.AddItem(new Item(item.item), 1);
                    Destroy(other.gameObject);
                }
            }
        }
        
    }
    /*private void Update()
    {
        
    }*/
}
