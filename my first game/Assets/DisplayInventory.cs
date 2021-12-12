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

    [SerializeField]bool idSet = false; // check if id is correct on empty slots
    [SerializeField]int IDtoBeSwapped;

    //bools for interactivity with inventory
    [SerializeField] bool created = false;
    [SerializeField] bool isPopped= false;
    [SerializeField] bool isOnSwap = false;
    [SerializeField] bool swapped = false;
    [SerializeField] float X_START;
    [SerializeField] float Y_START;
    [SerializeField] float X_SPACE_BETWEEN_ITEM;
    [SerializeField] int NUMBER_OF_COLUMN;
    [SerializeField] float Y_SPACE_BETWEEN_ITEMS;
    [SerializeField] bool IsExpanded = false;
    [SerializeField] int idClicked = 0 ;
    [SerializeField] bool isOnInfo = false;
    [SerializeField] bool discarded=false;
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
    [SerializeField] GameObject contentInfoParent;
    [SerializeField] GameObject contentInfo;

    Dictionary<GameObject, InventorySlot> itemsDisplayed=new Dictionary<GameObject, InventorySlot >();
    Dictionary<GameObject, InventorySlot> itemsDisplayedExpanded= new Dictionary<GameObject, InventorySlot>();
    Dictionary<int, int> availableIDs = new Dictionary<int, int>();
    // Start is called before the first frame update
    void Start()
    {
        inventoryPrefab.transform.GetChild(2).gameObject.SetActive(false);
        expanded.SetActive(false);
        capacity = inventory.isFree() + expandedInventory.isFree();
        //CreateIDs();
        SerializeSlots();
        CreateSlots();
    }

    // Update is called once per frame
    void Update()
    {
         UpdateSlots();

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
    private void CreateIDs()
    {
        availableIDs = new Dictionary<int, int>();
        for (int i = -253; i <56*253 ; i++)
        {
            availableIDs.Add(i,0);
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

                 obj.GetComponent<Button>().onClick.AddListener(() => OnClick(obj, itemId));

                itemsDisplayedExpanded.Add(obj, inventory.Container.Items[i]);
            }
            for (int i = 0; i < expandedInventory.Container.Items.Length; i++)
            {
                int itemId = expandedInventory.Container.Items[i].ID;
                var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);


                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.transform.SetParent(expandedMask.transform);
                obj.tag = "Max";
               
                obj.GetComponent<Button>().onClick.AddListener(() => OnClick(obj, itemId));
               
                itemsDisplayedExpanded.Add(obj, expandedInventory.Container.Items[i]);
            }
        }
    }
    private void SerializeSlots()
    {
        int j=1;
        int l = 1;
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            if (inventory.Container.Items[i].ID < 0)
            {
                inventory.Container.Items[i].ID = l * (-253);
                inventory.Container.Items[i].item.Id = l * (-253);
                l++;
                j++;
            }
        }
        for(int k =0; k < expandedInventory.Container.Items.Length;k++)
        {
            if (expandedInventory.Container.Items[k].ID < 0)
            {
                expandedInventory.Container.Items[k].ID = (j + 1) * (-253);
                expandedInventory.Container.Items[k].item.Id = (j + 1) * (-253);
                j++;
            }
        }
        idSet = true;

    }
    private void OnSwapping(GameObject swapping, int itemId)
    {

        idClicked = itemId;
        Debug.Log(itemId);
        InventorySlot temp;
        bool flag = false;
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            if (inventory.Container.Items[i].ID == IDtoBeSwapped)
            {
                temp = inventory.Container.Items[i];
                for (int j = 0; j < inventory.Container.Items.Length; j++)
                {
                    int test = inventory.Container.Items[j].ID;
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
                        int test = expandedInventory.Container.Items[j].ID;
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

                if (expandedInventory.Container.Items[i].ID == IDtoBeSwapped)
                {
                    temp = expandedInventory.Container.Items[i];
                    for (int j = 0; j < inventory.Container.Items.Length; j++)
                    {

                        int test = inventory.Container.Items[j].ID;
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
                            int test = expandedInventory.Container.Items[j].ID;
                            if (expandedInventory.Container.Items[j].ID == itemId)
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
            swapped = true;

        }
        for (int i = 0; i < expandedMask.transform.childCount; i++)
        {
            expandedMask.transform.GetChild(i).transform.GetChild(2).gameObject.SetActive(false);
        }
        setMinimized();
        setExpanded();
        
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

        if (!isPopped && !isOnSwap &&  !isOnInfo && itemId >=0 )
        {
            previousSelected = obj;
            obj.transform.GetChild(2).gameObject.SetActive(true);
            expandButton.SetActive(true);
            popupParent.transform.position = obj.transform.position;
            GameObject popupWindow = Instantiate(popup, popupParent.transform);

            popupWindow.GetComponentInChildren<ContentSizeFitter>().transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => swapper(obj, popupWindow, itemId));
            popupWindow.GetComponentInChildren<ContentSizeFitter>().transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => Info(obj, popupWindow, itemId));
            popupWindow.GetComponentInChildren<ContentSizeFitter>().transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => Discard(obj, popupWindow, itemId));
        }
        else if (isOnSwap && IsExpanded)
        { 
            OnSwapping(obj, itemId);
            isOnSwap = false;
        }
        else if (isOnInfo)
        {
            resetInfo();
        }
        else
        {
            if (itemId <= 0)
            {
                return;
            }
            onClickDestroy(previousSelected, popupParent.transform.GetChild(0).gameObject, itemId);
        }
    }
    private void Discard(GameObject selected, GameObject popupWindow, int itemId)
    {
        Destroy(popupWindow);
        bool flag = false;
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            if (inventory.Container.Items[i].item.Id == itemId)
            {
                inventory.Container.Items[i].item.Name = "";
                inventory.Container.Items[i].item.Id = -1;
                inventory.Container.Items[i].amount = 0;
                inventory.Container.Items[i].ID = -1;
                flag = true;
                created = false;
                discarded = true;
                break;
            }
        }
        if (!flag)
        {
            for (int i = 0; i < expandedInventory.Container.Items.Length; i++)
            {
                if (expandedInventory.Container.Items[i].item.Id == itemId)
                {
                    expandedInventory.Container.Items[i].item.Name = "";
                    expandedInventory.Container.Items[i].item.Id = -1;
                    expandedInventory.Container.Items[i].amount = 0;
                    expandedInventory.Container.Items[i].ID = -1;
                    flag = true;
                    created = false;
                    discarded = true;
                    break;
                }
            }
        }
        if (flag)
        {
            SerializeSlots(); 
        }
        if (discarded)
        {
            setMinimized();
            setExpanded();
        }
    }
    private void resetInfo()
    {
        setMinimized();
        setExpanded();
        contentInfoParent.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 0f);
        contentInfo.GetComponentInChildren<TextMeshProUGUI>().text = "";
        isOnInfo = false;
    }
    private void removelisteners()
    {
        foreach  (KeyValuePair <GameObject , InventorySlot> item in itemsDisplayedExpanded)
        {
            item.Key.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }
    private void Highlight(int itemid)
    {
        foreach (var item in itemsDisplayedExpanded)
        {
            if (item.Value.item.Id == itemid)
            {
                item.Key.transform.GetChild(2).gameObject.SetActive(true);
                break;
            }
        }
    }
    private void Info(GameObject selected,GameObject popupWindow,int itemId)
    {
        isOnInfo = true;
        Destroy(popupWindow);
        if (!IsExpanded)
        {
            setExpanded();
        }
        foreach (var item in itemsDisplayedExpanded)
        {
            if (item.Value.ID >=0 && item.Value.item.Id == itemId)
            {
                    item.Key.transform.GetChild(2).gameObject.SetActive(true);
                    contentInfoParent.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1f,1f,1f,1f);
                    contentInfoParent.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[item.Value.item.Id].uiDisplay;
                    contentInfo.GetComponentInChildren<TextMeshProUGUI>().text = inventory.database.GetItem[item.Value.item.Id].description;
                    break;
            }
        }

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

    private void swapper(GameObject obj, GameObject popupWindow, int itemId)
    {
        isPopped = false;
        Destroy(popupWindow);
        IDtoBeSwapped = itemId;
        isOnSwap = true;

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
        itemsDisplayedExpanded.Clear();
        foreach (Transform child in expandedMask.transform)
        {
            if (child.tag.Equals("Max"))
            {
                Destroy(child.gameObject);
            }
        }

        isOnSwap = false;
        idSet = false;
    }
    public void setMinimized()
    {
        DestroyMax();
        expandButton.SetActive(true);
        IsExpanded = false;
        isOnInfo = false;
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
