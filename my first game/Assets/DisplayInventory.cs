using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory;

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
    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        print(inventory.Container.Count);
        expanded.SetActive(false);
        CreateDisplay();
    }

    // Update is called once per frame
    void Update()
    {  
        /*if (IsExpanded && !flag)
        {

            //NUMBER_OF_COLUMN = 10;
            createExpanded();
            flag = true;
        }
        else*/ if (flag2)
        {
            CreateDisplay();
            flag2 = false;
        }
       
         else
         {
            UpdateDisplay();
         }
    }
    public void UpdateDisplay()
    {
        if (inventory.Container.Count >= 7)
        {
            for (int i = 0; i < 7; i++)
            {
                if (itemsDisplayed.ContainsKey(inventory.Container[i]))
                {
                    itemsDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");

                }
                else
                {
                    var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                    obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                    obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                    obj.tag = "Min";
                    itemsDisplayed.Add(inventory.Container[i], obj);
                }
            }
        }
        else
        {
            for (int i = 0; i < inventory.Container.Count; i++)
            {
                if (itemsDisplayed.ContainsKey(inventory.Container[i]))
                {
                    itemsDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");

                }
                else
                {
                    var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                    obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                    obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                    obj.tag = "Min";
                    itemsDisplayed.Add(inventory.Container[i], obj);
                }
            }
        }
    }
    public void UpdateDisplayMinimized()
    {
        if (inventory.Container.Count >= 7)
        {
            for (int i = 0; i <= 6; i++)
            {
                if (itemsDisplayed.ContainsKey(inventory.Container[i]))
                {
                    itemsDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");

                }
                else
                {
                    var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                    obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                    obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                    itemsDisplayed.Add(inventory.Container[i], obj);
                }
            }
        }
        else
        {
            UpdateDisplay();
        }
    }
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
    public void CreateDisplay()
    {
        if (inventory.Container.Count >= 7)
        {
            for (int i = 0; i < 7; i++)
            {
                var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                obj.tag = "Min";
                itemsDisplayed.Add(inventory.Container[i], obj);
            }
        }
        else
        {
            for (int i = 0; i < inventory.Container.Count; i++)
            {
                var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                obj.tag = "Min";
                itemsDisplayed.Add(inventory.Container[i], obj);
            }
        }
    }
    private void DestroyMinimized()
    {
        itemsDisplayed.Clear();
        foreach(Transform child in transform)
        {
            if (child.tag.Equals("Min"))
            {
                Destroy(child.gameObject);
            }
        }

    }
    private void createExpanded()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, expandedMask.transform);
            //obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
            obj.tag = "Max";
           // obj.transform.localScale = obj.transform.localScale * 2;
            obj.transform.SetParent(expandedMask.transform);
            //obj.transform.localScale = obj.transform.localScale * 0.2f;
            //expandedMask.transform.localScale = expandedMask.transform.localScale * 0.2f;
            itemsDisplayed.Add(inventory.Container[i], obj);
        }
    }
    public void setExpanded()
    {
        DestroyMinimized();
        expandButton.SetActive(false);
        floatingJoystick.SetActive(false);
        /*X_START = -465f;
        Y_START = 340f;
        X_SPACE_BETWEEN_ITEM = 80f;
        Y_SPACE_BETWEEN_ITEMS = 75f;
        NUMBER_OF_COLUMN = 8;*/
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
        DestroyMinimized();
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
        return new Vector3(X_START+(X_SPACE_BETWEEN_ITEM*(i % NUMBER_OF_COLUMN)),Y_START + (-Y_SPACE_BETWEEN_ITEMS*(i/NUMBER_OF_COLUMN)),0f);
    }
}
