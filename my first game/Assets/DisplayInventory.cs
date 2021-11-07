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

    int capacity = 56;

    public float X_START;
    public float Y_START;
    public float X_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    public float Y_SPACE_BETWEEN_ITEMS;
    public bool IsExpanded = false;

    [SerializeField] InventoryObject inventory;
    [SerializeField] ExpandedInventoryObject expandedInventory;
    [SerializeField] GameObject inventoryPrefab;
    [SerializeField] GameObject expandButton;
    [SerializeField] GameObject floatingJoystick;
    [SerializeField] GameObject expanded;
    [SerializeField] GameObject expandedMask;
    [SerializeField] GameObject inventoryObject;
    [SerializeField] GameObject capience;
    [SerializeField] GameObject popup;
    Dictionary<GameObject, InventorySlot> itemsDisplayed=new Dictionary<GameObject, InventorySlot >();
    Dictionary<GameObject, InventorySlot> itemsDisplayedExpanded= new Dictionary<GameObject, InventorySlot>();
    // Start is called before the first frame update
    void Start()
    {
        inventoryPrefab.transform.GetChild(2).gameObject.SetActive(false);
        expanded.SetActive(false);
        capacity = inventory.isFree() + expandedInventory.isFree();
        CreateSlots();
    }

    // Update is called once per frame
    void Update()
    {
  
            UpdateSlots();
    }
     void UpdateSlots()
    {
        if (!IsExpanded)
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
        else
        {
            foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayedExpanded)
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
            capience.GetComponentInChildren<TextMeshProUGUI>().text = "Free " + capacity + "/56";
        }
    }
   
    public void CreateSlots()
    {
        if (!IsExpanded)
        {
            itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
            for (int i = 0; i < inventory.Container.Items.Length; i++)
            {
                var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.tag = "Min";
                obj.GetComponent<Button>().onClick.AddListener(() => OnClick(obj));
                  //AddEvent(obj, EventTriggerType.PointerClick, delegate { OnClick(obj); });
                //AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
                //AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
                //AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

                itemsDisplayed.Add(obj, inventory.Container.Items[i]);

            }
        }
        else
        {
            itemsDisplayedExpanded = new Dictionary<GameObject, InventorySlot>();
            for (int i = 0; i < inventory.Container.Items.Length ; i++)
            {

                var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
              

                    obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                    obj.transform.SetParent(expandedMask.transform);
                    obj.tag = "Max";

                    itemsDisplayedExpanded.Add(obj, inventory.Container.Items[i]);
            }
            for (int i = 0; i < expandedInventory.Container.Items.Length; i++)
            {
                var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);


                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.transform.SetParent(expandedMask.transform);
                obj.tag = "Max";

                itemsDisplayedExpanded.Add(obj, expandedInventory.Container.Items[i]);
            }
        }
        UpdateCapience();
    }
    private void UpdateCapience()
    {
        capacity = inventory.isFree() + expandedInventory.isFree();
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
        obj.transform.GetChild(2).gameObject.SetActive(true);
        GameObject popupWindow = Instantiate(popup,obj.transform); 
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
        DestroyMin();
        CreateSlots();
        Time.timeScale = 0f;
    }
    private void DestroyMin()
    {
        itemsDisplayed.Clear();
        foreach (Transform child in this.transform)
        {
            if (child.tag.Equals("Min"))
            {
                Destroy(child.gameObject);
            }
        }

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
        X_START = -370f;
        Y_START = 0f;
        Y_SPACE_BETWEEN_ITEMS = 55f;
        X_SPACE_BETWEEN_ITEM = 55f;
        NUMBER_OF_COLUMN = 7;
        CreateSlots();
        Time.timeScale = 1f;
    }
    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMN)), 0f);
    }

}
