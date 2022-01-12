using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalScript : MonoBehaviour
{
    [SerializeField] GameObject expanded;
    [SerializeField] GameObject expandedButton;
    public List<string> journal= new List<string>();
    [SerializeField] string[] arrayOfStrings;
    [SerializeField] Text textBox;
    [SerializeField] Sprite[] images;
    [SerializeField] Image image;
    [SerializeField] Button nextButton;
    [SerializeField] Button previousButton;
    [SerializeField] int currentIndex = 0;
    [SerializeField] int currentLength = 0;
    [SerializeField] int previousLength;
    [SerializeField] DisplayInventory expandCheck;
    [SerializeField] GameObject notifParent;
    [SerializeField] GameObject notifUpdate;
    // Start is called before the first frame update
    void Start()
    {
        expanded.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (expandCheck.IsExpanded)
        {
            expandedButton.SetActive(false);
        }
        else
        {
            expandedButton.SetActive(true);
        }
        if (journal.Count==0)
        {
            nextButton.enabled = false;
            previousButton.enabled = false;
            textBox.text = "Your story is yet to be written...";
            image.color = new Vector4(1f,1f,1f,0f);
            
        }
        if (journal.Count > 0)
        {
            currentLength = journal.Count;
            nextButton.enabled = true;
            previousButton.enabled = true;
        }
        if (previousLength < currentLength)
        {
            toArrayList();
            Instantiate(notifUpdate, notifParent.transform);
            previousLength = currentLength;
        }
        if (currentIndex == currentLength - 1)
        {
            nextButton.enabled = false;
        }
        if (currentIndex==0)
        {
            previousButton.enabled = false;
        }
        if (arrayOfStrings.Length > 0)
        {
            image.color = new Vector4(1f, 1f, 1f, 1f);
            textBox.text = arrayOfStrings[currentIndex];
            image.sprite = images[currentIndex];
        }
        if (Time.timeScale == 0f)
        {
            expandedButton.SetActive(false);
        }
        if (Time.timeScale == 1f)
        {
            expandedButton.SetActive(true);
        }
        
    }
    void toArrayList()
    {
       arrayOfStrings = journal.ToArray();
    }
    public void DisplayCurrent()
    {
        if (arrayOfStrings.Length != 0)
        {
            textBox.text = arrayOfStrings[currentIndex];
        }
    }
    public void nextClick()
    {
        currentIndex++;
        for (int i = 0; i < arrayOfStrings.Length; i++)
        {
            if (i == currentIndex)
            {
                textBox.text = arrayOfStrings[i];
                break;
            }
        }
    }
    public void previousClick()
    {
        currentIndex--;
        for (int i = 0; i < arrayOfStrings.Length; i++)
        {
            if (i == currentIndex)
            {
                textBox.text = arrayOfStrings[i];
                break;
            }

        }
    }
}
