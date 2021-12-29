using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    //static int MAX_HOTBAR = 7;
    //static int MAX_EXPANDED_CAPACITY = 49;

    [SerializeField] InventoryObject inventory;
    [SerializeField] ExpandedInventoryObject expandedInventory;
    [SerializeField] SpecialSlots specialInventory;
    [SerializeField] LayerMask layerMask;
    [SerializeField] GameObject errorBox;
    [SerializeField] GameObject notificationParent;
    [SerializeField] Slider healthSlider;
    [SerializeField] Slider energySlider;
    SerializableVector3 position = new SerializableVector3();
    public SaveData toBeSaved;
    int freeSlotsHotBar = 0;
    int freeSlotsExpanded = 0;
    int freeSlotsSpecial = 0;
    bool specialItem1 = false;
    GroundItem item;
    private void Start()
    {

        freeSlotsHotBar = inventory.isFree();
        freeSlotsExpanded = expandedInventory.isFree();
        freeSlotsSpecial = specialInventory.isFree();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        bool isPresent = false;
        bool isPresentExp = false;
        bool flag = true;
        item = other.GetComponent<GroundItem>();

        if (other.gameObject.layer == 8 && !item.item.isSpecial)
        {
            for (int i = 0; i < inventory.Container.Items.Length; i++)
            {
                if (item.item.Id >= 8 && item.item.Id <= 12)
                {
                    if (inventory.Container.Items[i].ID == item.item.Id)
                    {

                        isPresent = true;
                        flag = false;
                        break;
                    }
                }
                else if (inventory.Container.Items[i].ID == item.item.Id)
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
                    if (item.item.Id >= 8 && item.item.Id <= 12)
                    {
                        if (expandedInventory.Container.Items[i].ID == item.item.Id)
                        {
                            isPresentExp = true;
                            flag = false;
                            break;
                        }
                    }
                    else if (expandedInventory.Container.Items[i].ID == item.item.Id)
                    {
                        isPresentExp = true;
                        flag = false;
                        break;
                    }
                }
            }
            if (isPresent && item.item.Id >= 8 && item.item.Id <= 12)
            {
                inventory.AddItem(new Item(item.item), 1, true);
                Destroy(other.gameObject);
            }
            else if (isPresent && !(item.item.Id >= 8 && item.item.Id <= 12))
            {
                inventory.AddItem(new Item(item.item), 1, false);
                Destroy(other.gameObject);

            }
            else if (isPresentExp && item.item.Id >= 8 && item.item.Id <= 12)
            {

                expandedInventory.AddItem(new Item(item.item), 1, true);
                Destroy(other.gameObject);
                print("added to expanded");
            }
            else if (isPresentExp && !(item.item.Id >= 8 && item.item.Id <= 12))
            {

                expandedInventory.AddItem(new Item(item.item), 1, false);
                Destroy(other.gameObject);
                print("added to expanded");
            }
            else
            {
                if (freeSlotsHotBar == 0 && freeSlotsExpanded > 0 &&
                    item.item.Id >= 8 && item.item.Id <= 12)
                {
                    expandedInventory.AddItem(new Item(item.item), 1, true);
                    Destroy(other.gameObject);
                    print("space in expanded");
                }
                else if (freeSlotsHotBar == 0 && freeSlotsExpanded > 0 &&
                   !(item.item.Id >= 8 && item.item.Id <= 12))
                {
                    expandedInventory.AddItem(new Item(item.item), 1, false);
                    Destroy(other.gameObject);
                    print("space in expanded");
                }
                else if (freeSlotsExpanded == 0 && freeSlotsHotBar == 0)
                {
                    print("No more space left");
                }
                else if (item.item.Id >= 8 && item.item.Id <= 12)
                {
                    inventory.AddItem(new Item(item.item), 1, true);
                    Destroy(other.gameObject);
                }
                else
                {
                    inventory.AddItem(new Item(item.item), 1, false);
                    Destroy(other.gameObject);
                }
            }
        }
        else if (other.gameObject.layer == 8 && item.item.isSpecial)
        {
            if (specialInventory.isPresent(item.item.Id) ||
                inventory.isPresent(item.item.Id) ||
                expandedInventory.isPresent(item.item.Id))
            {
                GameObject errorNotif = Instantiate(errorBox, notificationParent.transform) as GameObject;
                errorNotif.transform.GetComponentInChildren<TextMeshProUGUI>().text = "Warning: Max amount of item " + item.item.name + " reached!";
            }
            else
            {
                for (int i = 0; i < inventory.Container.Items.Length; i++)
                {
                    if (item.item.Id >= 8 && item.item.Id <= 12)
                    {
                        if (inventory.Container.Items[i].ID == item.item.Id)
                        {

                            isPresent = true;
                            flag = false;
                            break;
                        }
                    }
                    else if (inventory.Container.Items[i].ID == item.item.Id)
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
                        if (item.item.Id >= 8 && item.item.Id <= 12)
                        {
                            if (expandedInventory.Container.Items[i].ID == item.item.Id)
                            {
                                isPresentExp = true;
                                flag = false;
                                break;
                            }
                        }
                        else if (expandedInventory.Container.Items[i].ID == item.item.Id)
                        {
                            isPresentExp = true;
                            flag = false;
                            break;
                        }
                    }
                }
                if (isPresent && item.item.Id >= 8 && item.item.Id <= 12)
                {
                    inventory.AddItem(new Item(item.item), 1, true);
                    Destroy(other.gameObject);
                }
                else if (isPresent && !(item.item.Id >= 8 && item.item.Id <= 12))
                {
                    inventory.AddItem(new Item(item.item), 1, false);
                    Destroy(other.gameObject);

                }
                else if (isPresentExp && item.item.Id >= 8 && item.item.Id <= 12)
                {

                    expandedInventory.AddItem(new Item(item.item), 1, true);
                    Destroy(other.gameObject);
                    print("added to expanded");
                }
                else if (isPresentExp && !(item.item.Id >= 8 && item.item.Id <= 12))
                {

                    expandedInventory.AddItem(new Item(item.item), 1, false);
                    Destroy(other.gameObject);
                    print("added to expanded");
                }
                else
                {
                    if (freeSlotsHotBar == 0 && freeSlotsExpanded > 0 &&
                        item.item.Id >= 8 && item.item.Id <= 12)
                    {
                        expandedInventory.AddItem(new Item(item.item), 1, true);
                        Destroy(other.gameObject);
                        print("space in expanded");
                    }
                    else if (freeSlotsHotBar == 0 && freeSlotsExpanded > 0 &&
                       !(item.item.Id >= 8 && item.item.Id <= 12))
                    {
                        expandedInventory.AddItem(new Item(item.item), 1, false);
                        Destroy(other.gameObject);
                        print("space in expanded");
                    }
                    else if (freeSlotsExpanded == 0 && freeSlotsHotBar == 0)
                    {
                        print("No more space left");
                    }
                    else if (item.item.Id >= 8 && item.item.Id <= 12)
                    {
                        inventory.AddItem(new Item(item.item), 1, true);
                        Destroy(other.gameObject);
                    }
                    else
                    {
                        inventory.AddItem(new Item(item.item), 1, false);
                        Destroy(other.gameObject);
                    }
                }

            }
        }

    }
    /*sets data to be saved */
    public void setToSave()
    {
        position.x = transform.position.x;
        position.y = transform.position.y;
        position.z = transform.position.z;
        toBeSaved.health = healthSlider.value;
        toBeSaved.energy = energySlider.value;
        toBeSaved.spawnPosition = position;

    }
    /*Resets player variables from Load() function in  SaveManager Class */
    public void resetSave()
    {
        Vector3 oldPosition = new Vector3(toBeSaved.spawnPosition.x, toBeSaved.spawnPosition.y, toBeSaved.spawnPosition.z);
        transform.position = oldPosition;
        healthSlider.value = toBeSaved.health;
        energySlider.value = toBeSaved.energy;

    }
    public void saveInventory()
    {
        inventory.Save();
        expandedInventory.Save();
        specialInventory.Save();
    }
    public void loadInventory()
    {
        inventory.Load();
        expandedInventory.Load();
        specialInventory.Load();
    }
    //private void Update()
    //{
    //    if (specialInventory.isPresent(item.item.Id))
    //    {

    //        energyBarSlider.gameObject.SetActive(true);
    //    }
    //    else
    //    {

    //        energyBarSlider.gameObject.SetActive(false);
    //    }
    //}
}
