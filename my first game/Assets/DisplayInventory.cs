using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DisplayInventory : MonoBehaviour
{
    [SerializeField] GameObject inventoryPrefab;
   [SerializeField] InventoryObject inventory;
    [SerializeField] InventoryObject expandedInventory;

    public float X_START;
    public float Y_START;
    public float X_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    public float Y_SPACE_BETWEEN_ITEMS;
    public bool IsExpanded = false;
    bool flag = false;
    bool flag2 = false;
    [SerializeField] GameObject expandButton;
    [SerializeField] GameObject floatingJoystick;
    [SerializeField] GameObject expanded;
    [SerializeField] GameObject expandedMask;
    [SerializeField] GameObject inventoryObject;
    Dictionary<GameObject, InventorySlot> itemsDisplayed=new Dictionary<GameObject, InventorySlot >();
    Dictionary<GameObject, InventorySlot> itemsDisplayedExpanded= new Dictionary<GameObject, InventorySlot>();

    // Start is called before the first frame update
    void Start()
    { 
        //inventory.Container.Items = new InventorySlot[7];

        expanded.SetActive(false);
        CreateSlots();
    }

    // Update is called once per frame
    void Update()
    {
            UpdateSlots();
    }
     void UpdateSlots()
    {
        foreach ( KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed)
        {
            if (_slot.Value.ID >= 0)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[_slot.Value.item.Id].uiDisplay;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0"); 
            }
            else
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }
    void UpdateExpanded()
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed)
        {
            if (_slot.Value.ID >= 0)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[_slot.Value.item.Id].uiDisplay;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
            }
            else
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }

    //public void UpdateDisplayMinimized()
    //{
    //    if (inventory.Container.Items.Count >= 7)
    //    {
    //        for (int i = 0; i <= 6; i++)
    //        {
    //            InventorySlot slot = inventory.Container.Items[i];
    //            if (itemsDisplayed.ContainsKey(slot))
    //            {
    //                itemsDisplayed[slot].GetComponentInChildren<TextMeshProUGUI>().text =slot.amount.ToString("n0");

    //            }
    //            else
    //            {
    //                var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
    //                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
    //                obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
    //                itemsDisplayed.Add(slot, obj);
    //            }
    //        }
    //    }
    //    else
    //    {
    //        UpdateDisplay();
    //    }
    //}
    /*public void CreateDisplayMax()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
            itemsDisplayed.Add(inventory.Container[i], obj);
        }
    }*/
    public void CreateSlots()
    {
        itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.Container.Items.Length ; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.tag = "Min";

            //AddEvent(obj, EventTriggerType.PointerClick, delegate { OnClick(obj); });
            //AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            //AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            //AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

            itemsDisplayed.Add(obj, inventory.Container.Items[i]);
            
        }
    }
    private void DestroyMinimized()
    {
        itemsDisplayed.Clear();
        foreach (Transform child in transform)
        {
            if (child.tag.Equals("Min"))
            {
                Destroy(child.gameObject);
            }
        }

    }
    private void createExpanded()
    {

        /* for (int i = 0; i < inventory.Container.Items.Count; i++)
         {
             InventorySlot slot = inventory.Container.Items[i];
             var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
             obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[slot.item.Id].uiDisplay;
             obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
             obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
             obj.transform.SetParent(expandedMask.transform);
             obj.tag = "Max";
             itemsDisplayed.Add(slot, obj);
         }*/
        for (int i = 0; i < expandedInventory.Container.Items.Length  ; i++)
        {
            itemsDisplayedExpanded = new Dictionary<GameObject, InventorySlot>();
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            if (i < 7)
            {
                
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.transform.SetParent(expandedMask.transform);
                obj.tag = "Max";

                itemsDisplayedExpanded.Add(obj, inventory.Container.Items[i]);
            }
            else
            {
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.transform.SetParent(expandedMask.transform);
                obj.tag = "Max";

                itemsDisplayedExpanded.Add(obj, expandedInventory.Container.Items[i]);
            }
        }
        
        
    }
    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }
    public void OnClick(GameObject obj)
    {
        
    }
    public void OnDragStart(GameObject obj)
    {
        var mouseObject = new GameObject();
        var rt = mouseObject.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(10,10);
        mouseObject.transform.SetParent(transform.parent);
        if (itemsDisplayed[obj].ID >= 0)
        {
            var img = mouseObject.AddComponent<Image>();
            img.sprite = inventory.database.GetItem[itemsDisplayed[obj].ID].uiDisplay;
            img.raycastTarget = false;
        }
    }
    public void OnDragEnd(GameObject obj)
    {

    }
    public void OnDrag(GameObject obj)
    {

    }
    public void setExpanded()
    {
        expandButton.SetActive(false);
        floatingJoystick.SetActive(false);
        //X_START = -465f;
        //Y_START = 340f; 
        // X_SPACE_BETWEEN_ITEM = 80f;
        //Y_SPACE_BETWEEN_ITEMS = 75f;
        //NUMBER_OF_COLUMN = 8;
        IsExpanded = true;
        expanded.SetActive(true);
        createExpanded();
        Time.timeScale = 0f;
    }
    private void DestroyMax()
    {
        itemsDisplayed.Clear();
        foreach (Transform child in expandedMask.transform)
        {
            if (child.tag.Equals("Max"))
            {
                Destroy(child.gameObject);
            }
        }

    }
    public void setMinimized()
    {
        DestroyMax();
        //DestroyMinimized();
        expandButton.SetActive(true);
        IsExpanded = false;
        floatingJoystick.SetActive(true);
        expanded.SetActive(false);
        flag2 = true;
        X_START = -306.5f;
        Y_START = 0.8f;
        Y_SPACE_BETWEEN_ITEMS = 40f;
        X_SPACE_BETWEEN_ITEM = 40f;
        NUMBER_OF_COLUMN = 8;
        Time.timeScale = 1f;
    }
    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMN)), 0f);
    }
}
