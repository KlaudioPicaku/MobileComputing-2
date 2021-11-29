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
    //bools for interactivity with inventory
    bool isPopped = false;
    bool isOnSwap = false;
    bool itemWaiting = false;

    int IDtoBeSwapped;

    [SerializeField] float X_START;
    [SerializeField] float Y_START;
    [SerializeField] float X_SPACE_BETWEEN_ITEM;
    [SerializeField] int NUMBER_OF_COLUMN;
    [SerializeField] float Y_SPACE_BETWEEN_ITEMS;
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
    [SerializeField] GameObject popupParent;
    [SerializeField] GameObject previousSelected;
    [SerializeField] GameObject minimiseButton;

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
        //Debug.Log(isOnSwap);

    }
    //updates slot image sprite and quantity
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
        if (popupParent.transform.childCount>0)
        {
            expandButton.SetActive(false);
            if (IsExpanded)
            {
                Transform shiftedPosition = popupParent.transform;
                popupParent.transform.GetChild(0).position = new Vector3(popupParent.transform.GetChild(0).position.x, popupParent.transform.position.y-24.5f, popupParent.transform.position.z);
                minimiseButton.SetActive(false);
            }
        }
        else
        {
            expandButton.SetActive(true);
            if (IsExpanded)
            {
                minimiseButton.SetActive(true);
            }

        }
        
    }
   //creates slots for inventory, both expanded and not depending on bool IsExpanded
    public void CreateSlots()
    {
        if (!IsExpanded)
        {
            itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
            for (int i = 0; i < inventory.Container.Items.Length; i++)
            {
                int itemId = inventory.Container.Items[i].ID;
                var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.tag = "Min";
                obj.GetComponent<Button>().onClick.AddListener(() => OnClick(obj, itemId));

                itemsDisplayed.Add(obj, inventory.Container.Items[i]);
            }
        }

        else
        {
            itemsDisplayedExpanded = new Dictionary<GameObject, InventorySlot>();
            for (int i = 0; i < inventory.Container.Items.Length; i++)
            {
                int itemId = inventory.Container.Items[i].ID;
                var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);


                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.transform.SetParent(expandedMask.transform);
                obj.tag = "Max";
                if (isOnSwap)
                {
                    obj.GetComponent<Button>().onClick.AddListener(() => OnSwapping(obj, itemId));
                }
                else
                {
                    obj.GetComponent<Button>().onClick.AddListener(() => OnClick(obj, itemId));
                }
                itemsDisplayedExpanded.Add(obj, inventory.Container.Items[i]);
            }
            for (int i = 0; i < expandedInventory.Container.Items.Length; i++)
            {
                int itemId = expandedInventory.Container.Items[i].ID;
                var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);


                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.transform.SetParent(expandedMask.transform);
                obj.tag = "Max";
                if (isOnSwap)
                {
                    obj.GetComponent<Button>().onClick.AddListener(() => OnSwapping(obj, itemId));
                }
                else
                {
                    obj.GetComponent<Button>().onClick.AddListener(() => OnClick(obj, itemId));
                }
                itemsDisplayedExpanded.Add(obj, expandedInventory.Container.Items[i]);
            }
        }
        UpdateCapience();
    }
    private void OnSwapping(GameObject swapping, int itemId)
    {
        InventorySlot temp;
        bool flag = false;
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            if (inventory.Container.Items[i].ID == IDtoBeSwapped)
            {
                temp = inventory.Container.Items[i];
                for (int j = 0; j < inventory.Container.Items.Length; j++)
                {
                    if (inventory.Container.Items[j].ID == itemId)
                    { 
                        inventory.Container.Items[i] = inventory.Container.Items[j];
                        inventory.Container.Items[j] = temp;
                        flag = true;
                        isOnSwap = false;
                        break;
                    }
                }
                if (!flag)
                {
                    for (int j = 0; j < expandedInventory.Container.Items.Length; j++)
                    {
                        if (expandedInventory.Container.Items[j].ID == itemId)
                        {
                            inventory.Container.Items[i] = expandedInventory.Container.Items[j];
                            expandedInventory.Container.Items[j] = temp;
                            flag = true;
                            isOnSwap = false;
                            break;
                        }
                    }
                }
            }
            if (flag)
            {
                break;
            }
        }
        if (!flag)
        {
            for (int i = 0; i < expandedInventory.Container.Items.Length; i++)
            {
                if (expandedInventory.Container.Items[i].ID==IDtoBeSwapped)
                {
                    temp = expandedInventory.Container.Items[i];
                    for (int j=0; j<inventory.Container.Items.Length;j++)
                    {
                        if (inventory.Container.Items[j].ID == itemId)
                        {
                            expandedInventory.Container.Items[i] = inventory.Container.Items[j];
                            inventory.Container.Items[j] = temp;
                            flag = true;
                            isOnSwap = false;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        for (int j = 0; j < expandedInventory.Container.Items.Length; j++)
                        {
                            if (expandedInventory.Container.Items[j].ID==itemId)
                            {
                                expandedInventory.Container.Items[i] = expandedInventory.Container.Items[j];
                                expandedInventory.Container.Items[j] = temp;
                                flag = true;
                                isOnSwap = false;
                                break;
                            }
                        }
                    }
                   
                }
                if (flag)
                {
                    break;
                    
                }
            }
        }
        for (int i = 0; i < expandedMask.transform.childCount; i++)
        {
            expandedMask.transform.GetChild(i).transform.GetChild(2).gameObject.SetActive(false);
        }
        if (!isOnSwap)
        {
            DestroyMax();
            setExpanded();
        }
    }
    private void UpdateCapience()
    {
        capacity = inventory.isFree() + expandedInventory.isFree();
    }
    private void OnClickOpen(GameObject obj,GameObject popupLocal,int itemId)
    {
        obj.GetComponent<Button>().onClick.RemoveAllListeners();
        obj.GetComponent<Button>().onClick.AddListener(() => onClickDestroy(obj,popupLocal,itemId));
    }
    private void onClickDestroy(GameObject obj, GameObject popupLocal,int itemId)
    {
        Destroy(popupLocal);

        obj.transform.GetChild(2).gameObject.SetActive(false);
        obj.GetComponent<Button>().onClick.RemoveAllListeners();
        obj.GetComponent<Button>().onClick.AddListener(() => OnClick(obj,itemId));

    }   
    private void OnClick(GameObject obj, int itemId)
    {
        isWindowPopped(obj);
        if (!isPopped && !isOnSwap)
        {
            previousSelected = obj;
            obj.transform.GetChild(2).gameObject.SetActive(true);
            expandButton.SetActive(true);
            popupParent.transform.position = obj.transform.position;
            GameObject popupWindow = Instantiate(popup, popupParent.transform);
            popupWindow.GetComponentInChildren<ContentSizeFitter>().transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => OnSwap(obj, popupWindow, itemId));
            OnClickOpen(obj, popupWindow, itemId);
        }
        else if (isOnSwap && IsExpanded )
        {
            OnSwapping(obj, itemId);
            //DestroyMax();
            //setExpanded();
        }
        /*else if(IsExpanded && isOnSwap)
        {
            foreach (var item in itemsDisplayedExpanded)
            {
                item.Key.transform.GetComponent<Button>().onClick.AddListener(() => OnSwapping(obj, itemId));
            }
        }*/
        else
            onClickDestroy(previousSelected, popupParent.transform.GetChild(0).gameObject, itemId);
    }
    private void isWindowPopped(GameObject obj)
    {
        if (popupParent.transform.childCount > 0)
        {
            Transform popupWindow = popupParent.transform.GetChild(0);
            if (popupWindow)
            {
                isPopped = true;
            }
        }
        else
            isPopped = false;
    }
    private bool readyEnvironment(GameObject selected,GameObject popUpWindow,int itemId)
    {
        isPopped = false;
        Destroy(popUpWindow);
        if (!IsExpanded)
        {
            setExpanded();
        }
        bool flag = false;// true if object is highlighted
        for (int i = 0; i < expandedMask.transform.childCount; i++)
        {
            GameObject Go = expandedMask.transform.GetChild(i).gameObject;
            foreach (var item in itemsDisplayedExpanded) 
            {
                if (item.Value.ID == itemId)
                {
                    item.Key.transform.GetChild(2).gameObject.SetActive(true);
                    break;
                }
            }
        }
        return flag;
    }
    private void OnSwap(GameObject selected,GameObject popUpWindow,int itemId)
    {
        IDtoBeSwapped = itemId;
        isOnSwap = true;
        readyEnvironment(selected,popUpWindow,itemId);
        prepareSlots();
    }
    private void prepareSlots()
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

    }
    
    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
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

        isOnSwap = false;
    }
    public void setMinimized()
    {
        DestroyMax();
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
