using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class specialItemsController : MonoBehaviour
{
    [SerializeField] InventoryObject inventory;
    [SerializeField] ExpandedInventoryObject expandedInventory;
    [SerializeField] SpecialSlots specialInventory;

    [SerializeField] GameObject energyBarSlider;
    // Update is called once per frame
    private void Update()
    {
        if (specialInventory.isPresent(8))
        {
            energyBarSlider.gameObject.SetActive(true);
        }
        else
        {

            energyBarSlider.gameObject.SetActive(false);
        }
    }
}
