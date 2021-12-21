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

    [SerializeField] bool idSet = false; // check if id is correct on empty slots
    [SerializeField] int IDtoBeSwapped;

    //bools for interactivity with inventory
    [SerializeField] bool created = false;
    [SerializeField] bool isPopped = false;
    [SerializeField] bool swapped = false;
    bool isOnSwap = false;
    bool invalidFlag = false;

    [SerializeField] float X_START;
    [SerializeField] float Y_START;
    [SerializeField] float X_SPACE_BETWEEN_ITEM;
    [SerializeField] int NUMBER_OF_COLUMN;
    [SerializeField] float Y_SPACE_BETWEEN_ITEMS;

    [SerializeField] float X_START_SPECIAL;
    [SerializeField] float Y_START_SPECIAL;
    [SerializeField] float X_SPACE_BETWEEN_ITEM_SPECIAL;
    [SerializeField] int NUMBER_OF_COLUMN_SPECIAL;
    [SerializeField] float Y_SPACE_BETWEEN_ITEMS_SPECIAL;

    [SerializeField] bool IsExpanded = false;
    [SerializeField] int idClicked = 0;
    [SerializeField] bool isOnInfo = false;
    [SerializeField] bool discarded = false;
    [SerializeField] InventoryObject inventory;
    [SerializeField] ExpandedInventoryObject expandedInventory;
    [SerializeField] SpecialSlots specialInventory;
    [SerializeField] GameObject inventoryPrefab;
    [SerializeField] GameObject SpecialInventoryPrefab;
    [SerializeField] GameObject expandButton;
    [SerializeField] GameObject floatingJoystick;

    [SerializeField] GameObject expanded;
    [SerializeField] GameObject expandedMask;
    [SerializeField] GameObject specialSlotsParent;

    [SerializeField] HealthBarController healthGainer;
    //[SerializeField] EnergyBarController energyGainer;

    [SerializeField] GameObject inventoryObject;
    [SerializeField] GameObject capience;
    [SerializeField] GameObject popup;
    [SerializeField] GameObject popupParent;
    [SerializeField] GameObject previousSelected;
    [SerializeField] GameObject minimiseButton;
    [SerializeField] GameObject contentInfoParent;
    [SerializeField] GameObject contentInfo;
    [SerializeField] GameObject warning;
    [SerializeField] GameObject healingPrefab;
    [SerializeField] GameObject energyGainingPrefab;
    [SerializeField] GameObject playerOverHeadIconsParent;

    [SerializeField] Slider energySlider;

    [SerializeField] Sprite defaultSprite;
    [SerializeField] Color specialColor;


    Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
    Dictionary<GameObject, InventorySlot> itemsDisplayedExpanded = new Dictionary<GameObject, InventorySlot>();
    Dictionary<GameObject, InventorySlot> specialItemsPresent = new Dictionary<GameObject, InventorySlot>();
    Dictionary<int, int> availableIDs = new Dictionary<int, int>();
    // Start is called before the first frame update
    void Start()
    {
        inventoryPrefab.transform.GetChild(2).gameObject.SetActive(false);
        expanded.SetActive(false);
        capacity = inventory.isFree() + expandedInventory.isFree();
        Color temporarycolor = SpecialInventoryPrefab.GetComponent<Image>().color;
        specialColor = new Color(temporarycolor.r, temporarycolor.g, temporarycolor.b, temporarycolor.a);
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
                    if (!(_slot.Value.ID >= 8 && _slot.Value.ID <= 8))
                    {
                        _slot.Key.transform.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                        
                    }
                    else
                    {
                        _slot.Key.transform.GetComponentInChildren<Image>().color = specialColor;
                    }
                    _slot.Key.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");

                }
                else
                {
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = defaultSprite;
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0.5f);
                    _slot.Key.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
                }
            }
        }
        else
        {

            foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayedExpanded)
            {
                Debug.Log(_slot.Value.ID);
                if (_slot.Value.ID >= 0)
                {
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[_slot.Value.item.Id].uiDisplay;
                    if (!(_slot.Value.ID >= 8 && _slot.Value.ID <= 8))
                    {
                        _slot.Key.transform.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    }
                    else
                    {
                        _slot.Key.transform.GetComponent<Image>().color = specialColor;
                    }
                    _slot.Key.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
                }
                else
                {
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = defaultSprite;
                    _slot.Key.transform.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0.5f);
                    _slot.Key.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
                }
            }

            foreach (KeyValuePair<GameObject, InventorySlot> _slot in specialItemsPresent)
            {
                if (_slot.Value.ID >= 0)
                {
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[_slot.Value.item.Id].uiDisplay;
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                    _slot.Key.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
                }
                else
                {
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = defaultSprite;
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 0.5f);
                    _slot.Key.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
                }
            }
            capience.GetComponentInChildren<TextMeshProUGUI>().text = "Free " + capacity + "/56";

        }
        if (popupParent.transform.childCount > 0)
        {
            expandButton.SetActive(false);
            if (IsExpanded)
            {
                Transform shiftedPosition = popupParent.transform;
                popupParent.transform.GetChild(0).position = new Vector3(popupParent.transform.GetChild(0).position.x, popupParent.transform.position.y - 24.5f, popupParent.transform.position.z);
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
        for (int i = -253; i < 56 * 253; i++)
        {
            availableIDs.Add(i, 0);
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
            specialItemsPresent = new Dictionary<GameObject, InventorySlot>();
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
            for (int i = 0; i < specialInventory.Container.Items.Length; i++)
            {
                int itemId = specialInventory.Container.Items[i].ID;
                var obj = Instantiate(SpecialInventoryPrefab, Vector3.zero, Quaternion.identity, transform);


                obj.GetComponent<RectTransform>().localPosition = GetPositionSpecial(i);
                obj.transform.SetParent(specialSlotsParent.transform);
                obj.tag = "Spec";
                specialInventory.Container.Items[i].isSpecial = true;
                obj.GetComponent<Button>().onClick.AddListener(() => OnClick(obj, itemId));

                specialItemsPresent.Add(obj, specialInventory.Container.Items[i]);
            }
        }
    }
    private void SerializeSlots()
    {
        int j = 1;
        int l = 1;
        int m = 1;
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            if (inventory.Container.Items[i].ID < 0)
            {
                inventory.Container.Items[i].ID = l * (-253);
                inventory.Container.Items[i].item.Id = l * (-253);
                l++;
                j++;
                m++;
            }
        }
        for (int k = 0; k < expandedInventory.Container.Items.Length; k++)
        {
            if (expandedInventory.Container.Items[k].ID < 0)
            {
                expandedInventory.Container.Items[k].ID = (j + 1) * (-253);
                expandedInventory.Container.Items[k].item.Id = (j + 1) * (-253);
                j++;
                m++;
            }
        }
        for (int i = 0; i < specialInventory.Container.Items.Length; i++)
        {
            if (specialInventory.Container.Items[i].ID < 0)
            {
                specialInventory.Container.Items[i].ID = (m + 1) * (-253);
                specialInventory.Container.Items[i].item.Id = (m + 1) * (-253);
                m++;
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
        invalidFlag = false;

        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            if (inventory.Container.Items[i].ID == IDtoBeSwapped)
            {
                temp = inventory.Container.Items[i];
                for (int j = 0; j < inventory.Container.Items.Length; j++)
                {
                    //int test = inventory.Container.Items[j].ID;
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
                    if (!flag)
                    {
                        for (int k = 0; k < specialInventory.Container.Items.Length; i++)
                        {
                            if (specialInventory.Container.Items[k].ID == itemId)
                            {
                                if (temp.isSpecial)
                                {
                                    inventory.Container.Items[i] = specialInventory.Container.Items[k];
                                    specialInventory.Container.Items[k] = temp;
                                    flag = true;
                                    isOnSwap = false;
                                    break;
                                }
                                else
                                {
                                    GameObject warningWindow = Instantiate(warning, this.transform.parent) as GameObject;
                                    warningWindow.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Attention: You can't put non special items in a special slot!";
                                    flag = true;
                                    invalidFlag = true;
                                    break;
                                }
                            }
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
                        if (!flag)
                        {
                            for (int k = 0; k < specialInventory.Container.Items.Length; i++)
                            {
                                if (specialInventory.Container.Items[k].ID == itemId)
                                {
                                    if (temp.item.isSpecial)
                                    {
                                        expandedInventory.Container.Items[i] = specialInventory.Container.Items[k];
                                        specialInventory.Container.Items[k] = temp;
                                        flag = true;
                                        isOnSwap = false;
                                        break;
                                    }
                                    else
                                    {
                                        GameObject warningWindow = Instantiate(warning, this.transform.parent) as GameObject;
                                        warningWindow.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Attention: You can't put non special items in a special slot!";
                                        flag = true;
                                        invalidFlag = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                if (flag)
                {
                    break;
                }
            }
            //swapped = true;
        }
        if (!flag)
        {
            for (int i = 0; i < specialInventory.Container.Items.Length; i++)
            {
                if (specialInventory.Container.Items[i].ID == IDtoBeSwapped)
                {
                    temp = specialInventory.Container.Items[i];
                    for (int j = 0; j < inventory.Container.Items.Length; j++)
                    {
                        if (inventory.Container.Items[j].ID == itemId)
                        {
                            if (inventory.Container.Items[j].item.isSpecial || itemId < 0)
                            {
                                specialInventory.Container.Items[i] = inventory.Container.Items[j];
                                inventory.Container.Items[j] = temp;
                                flag = true;
                                isOnSwap = false;
                                break;
                            }
                            else
                            {
                                GameObject warningWindow = Instantiate(warning, this.transform.parent) as GameObject;
                                warningWindow.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Attention: You can't put non special items in a special slot!";
                                flag = true;
                                invalidFlag = true;
                                break;
                            }
                        }
                    }
                    if (!flag)
                    {
                        for (int j = 0; j < expandedInventory.Container.Items.Length; j++)
                        {

                            if (expandedInventory.Container.Items[j].ID == itemId)
                            {
                                if (expandedInventory.Container.Items[j].item.isSpecial || itemId < 0)
                                {
                                    specialInventory.Container.Items[i] = expandedInventory.Container.Items[j];
                                    expandedInventory.Container.Items[j] = temp;
                                    flag = true;
                                    isOnSwap = false;
                                    break;
                                }
                                else
                                {
                                    GameObject warningWindow = Instantiate(warning, this.transform.parent) as GameObject;
                                    warningWindow.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Attention: You can't put non special items in a special slot!";
                                    flag = true;
                                    invalidFlag = true;
                                    break;
                                }
                            }
                        }
                        if (!flag)
                        {
                            for (int k = 0; k < specialInventory.Container.Items.Length; k++)
                            {
                                bool test = temp.item.isSpecial;
                                if (specialInventory.Container.Items[k].ID == itemId)
                                {
                                    if (temp.item.isSpecial)
                                    {
                                        specialInventory.Container.Items[i] = specialInventory.Container.Items[k];
                                        specialInventory.Container.Items[k] = temp;
                                        flag = true;
                                        isOnSwap = false;
                                        break;
                                    }
                                    else
                                    {
                                        GameObject warningWindow = Instantiate(warning, this.transform.parent) as GameObject;
                                        warningWindow.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Attention: You can't put non special items in a special slot!";
                                        flag = true;
                                        invalidFlag = true;
                                        break;
                                    }
                                }

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
        setMinimized();
        setExpanded();
    }
    private void UpdateCapience()
    {
        capacity = inventory.isFree() + expandedInventory.isFree();
    }
    private void OnClickOpen(GameObject obj, GameObject popupLocal, int itemId)
    {
        obj.GetComponent<Button>().onClick.RemoveAllListeners();
        obj.GetComponent<Button>().onClick.AddListener(() => onClickDestroy(obj, popupLocal, itemId));
    }
    private void onClickDestroy(GameObject obj, GameObject popupLocal, int itemId)
    {
        Destroy(popupLocal);
        obj.transform.GetChild(2).gameObject.SetActive(false);
        obj.GetComponent<Button>().onClick.RemoveAllListeners();
        obj.GetComponent<Button>().onClick.AddListener(() => OnClick(obj, itemId));
        setMinimized();
        setExpanded();

    }
    private void OnClick(GameObject obj, int itemId)
    {
        isWindowPopped(obj);

        if (!isPopped && !isOnSwap && !isOnInfo && itemId >= 0)
        {
            previousSelected = obj;
            obj.transform.GetChild(2).gameObject.SetActive(true);
            expandButton.SetActive(true);
            popupParent.transform.position = obj.transform.position;
            GameObject popupWindow = Instantiate(popup, popupParent.transform);

            popupWindow.GetComponentInChildren<ContentSizeFitter>().transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => swapper(obj, popupWindow, itemId));
            popupWindow.GetComponentInChildren<ContentSizeFitter>().transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => Info(obj, popupWindow, itemId));
            if (!(itemId >= 8 && itemId <= 12))
            {
                popupWindow.GetComponentInChildren<ContentSizeFitter>().transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => Use(obj, popupWindow, itemId));
                popupWindow.GetComponentInChildren<ContentSizeFitter>().transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => Discard(obj, popupWindow, itemId));
            }
            else
            {
                popupWindow.GetComponentInChildren<ContentSizeFitter>().transform.GetChild(3).GetComponent<Button>().enabled = false;
            }
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
    private void Use(GameObject obj, GameObject popupWindow, int itemId)
    {
        Destroy(popupWindow);
        bool flag = true;
        if (!IsExpanded)
        {
            for (int i = 0; i < inventory.Container.Items.Length; i++)
            {
                if (inventory.Container.Items[i].ID == itemId)
                {
                    string test = inventory.Container.Items[i].item.itemType.ToString();
                    if (inventory.Container.Items[i].item.itemType.ToString().Equals("Food"))
                    {
                        healthGainer.SetHealth(-30);
                        Instantiate(healingPrefab, playerOverHeadIconsParent.transform);
                        if (inventory.Container.Items[i].amount == 1)
                        {
                            inventory.Container.Items[i].ID = -1;
                            inventory.Container.Items[i].item.Id = -1;
                            inventory.Container.Items[i].item.Name = "";
                            inventory.Container.Items[i].amount = 0;
                            flag = true;
                            break;
                        }
                        else
                        {
                            inventory.Container.Items[i].amount = inventory.Container.Items[i].amount - 1;
                        }
                    }
                    else if (inventory.Container.Items[i].item.itemType.ToString().Equals("Energy"))
                    {
                        energySlider.value = 100f;
                        Instantiate(energyGainingPrefab, playerOverHeadIconsParent.transform);
                        if (inventory.Container.Items[i].amount == 1)
                        {
                            inventory.Container.Items[i].ID = -1;
                            inventory.Container.Items[i].item.Id = -1;
                            inventory.Container.Items[i].item.Name = "";
                            inventory.Container.Items[i].amount = 0;
                            flag = true;
                            break;
                        }
                        else
                        {
                            inventory.Container.Items[i].amount = inventory.Container.Items[i].amount - 1;
                        }
                    }
                }
            }
            if (flag)
            {
                SerializeSlots();
                setExpanded();
                setMinimized();
            }
        }
        else
        {
            for (int i = 0; i < inventory.Container.Items.Length; i++)
            {
                if (inventory.Container.Items[i].ID == itemId)
                {
                    if (inventory.Container.Items[i].item.itemType.Equals("Food"))
                    {
                        healthGainer.SetHealth(-30);
                        Instantiate(healingPrefab, playerOverHeadIconsParent.transform);
                        if (inventory.Container.Items[i].amount == 1)
                        {
                            inventory.Container.Items[i].ID = -1;
                            inventory.Container.Items[i].item.Id = -1;
                            inventory.Container.Items[i].item.Name = "";
                            inventory.Container.Items[i].amount = 0;
                            flag = true;
                            break;
                        }
                    }
                }
                else if (inventory.Container.Items[i].item.itemType.ToString().Equals("Energy"))
                {
                    energySlider.value = 100f;
                    Instantiate(energyGainingPrefab, playerOverHeadIconsParent.transform);
                    if (inventory.Container.Items[i].amount == 1)
                    {
                        inventory.Container.Items[i].ID = -1;
                        inventory.Container.Items[i].item.Id = -1;
                        inventory.Container.Items[i].item.Name = "";
                        inventory.Container.Items[i].amount = 0;
                        flag = true;
                        break;
                    }
                    else
                    {
                        inventory.Container.Items[i].amount = inventory.Container.Items[i].amount - 1;
                    }
                }
            }
            if (!flag)
            {
                for (int i = 0; i < expandedInventory.Container.Items.Length; i++)
                {
                    if (expandedInventory.Container.Items[i].ID == itemId)
                    {
                        if (expandedInventory.Container.Items[i].item.itemType.Equals("Food"))
                        {
                            healthGainer.SetHealth(-30);
                            Instantiate(healingPrefab, playerOverHeadIconsParent.transform);
                            if (expandedInventory.Container.Items[i].amount == 1)
                            {
                                expandedInventory.Container.Items[i].ID = -1;
                                expandedInventory.Container.Items[i].item.Id = -1;
                                expandedInventory.Container.Items[i].item.Name = "";
                                expandedInventory.Container.Items[i].amount = 0;
                                flag = true;
                                break;
                            }
                        }
                    }
                    else if (expandedInventory.Container.Items[i].item.itemType.ToString().Equals("Energy"))
                    {
                        energySlider.value = 100f;
                        Instantiate(energyGainingPrefab, playerOverHeadIconsParent.transform);
                        if (inventory.Container.Items[i].amount == 1)
                        {
                            expandedInventory.Container.Items[i].ID = -1;
                            expandedInventory.Container.Items[i].item.Id = -1;
                            expandedInventory.Container.Items[i].item.Name = "";
                            expandedInventory.Container.Items[i].amount = 0;
                            flag = true;
                            break;
                        }
                        else
                        {
                            expandedInventory.Container.Items[i].amount = inventory.Container.Items[i].amount - 1;
                        }
                    }
                }
            }
            if (flag)
            {
                SerializeSlots();
                setMinimized();
                setExpanded();
            }

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
        foreach (KeyValuePair<GameObject, InventorySlot> item in itemsDisplayedExpanded)
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
    private void Info(GameObject selected, GameObject popupWindow, int itemId)
    {
        isOnInfo = true;
        Destroy(popupWindow);
        if (!IsExpanded)
        {
            setExpanded();
        }
        foreach (var item in itemsDisplayedExpanded)
        {
            if (item.Value.ID >= 0 && item.Value.item.Id == itemId)
            {
                item.Key.transform.GetChild(2).gameObject.SetActive(true);
                contentInfoParent.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 1f);
                contentInfoParent.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[item.Value.item.Id].uiDisplay;
                contentInfo.GetComponentInChildren<TextMeshProUGUI>().text = inventory.database.GetItem[item.Value.item.Id].description;
                break;
            }
        }
        foreach (var item in specialItemsPresent)
        {
            if (item.Value.ID >= 0 && item.Value.item.Id == itemId)
            {
                item.Key.transform.GetChild(2).gameObject.SetActive(true);
                contentInfoParent.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 1f);
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
        //Time.timeScale = 0f;
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
        foreach (Transform child in specialSlotsParent.transform)
        {
            if (child.tag.Equals("Spec"))
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
    public Vector3 GetPositionSpecial(int i)
    {
        return new Vector3(X_START_SPECIAL + (X_SPACE_BETWEEN_ITEM_SPECIAL * (i % NUMBER_OF_COLUMN_SPECIAL)), Y_START_SPECIAL + (-Y_SPACE_BETWEEN_ITEMS_SPECIAL * (i / NUMBER_OF_COLUMN_SPECIAL)), 0f);
    }
}
