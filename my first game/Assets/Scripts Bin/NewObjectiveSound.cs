using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewObjectiveSound : MonoBehaviour
{
    [SerializeField] GameObject textBox;
    [SerializeField] string currentString;
    [SerializeField] string newString;
    [SerializeField] AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        currentString = textBox.GetComponent<Text>().text;
        
    }

    // Update is called once per frame
    void Update()
    {
        newString = textBox.GetComponent<Text>().text;
        if (!newString.Equals(currentString))
        {
            currentString = newString;
            audio.Play();
        }
    }
}
