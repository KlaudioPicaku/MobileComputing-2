using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalScript : MonoBehaviour
{
    [SerializeField] GameObject expanded;
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
    // Start is called before the first frame update
    void Start()
    {
        expanded.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
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
        }
        if (previousLength < currentLength)
        {
            toArrayList();
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
        
    }
    void toArrayList()
    {
       arrayOfStrings = journal.ToArray();
    }
    public void DisplayCurrent()
    {
        textBox.text = arrayOfStrings[currentIndex];
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
