using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject joystick;
    [SerializeField] GameObject textBoxForTutorial;

    [SerializeField] Button aButton;
    [SerializeField] Button bButton;

    [SerializeField] bool movementComplete=false;
    [SerializeField] bool attackComplete=false;
    [SerializeField] bool jumpComplete = false;
    [SerializeField] bool interactComplete = false;
    [SerializeField] bool specialComplete = false;
    public bool tutorialCompleteLocal = false;
    public bool waveComplete = false;
    public bool nearBush = false;
    public bool itemPicked=false;
    public bool interacted = false;

    private void Awake()
    {
        this.tutorialCompleteLocal = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().tutorialComplete;
        aButton.enabled = false;
        bButton.enabled = false;
        joystick.SetActive(false);

    }
    private void Start()
    {
        if (!tutorialCompleteLocal)
        {
            textBoxForTutorial.SetActive(true);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (textBoxForTutorial.activeInHierarchy)
        {
            joystick.SetActive(true);
            if(joystick.activeInHierarchy && !movementComplete)
            {
                textBoxForTutorial.GetComponent<Text>().text = "Use the joystick to move";
                if (joystick.GetComponent<FloatingJoystick>().Horizontal > 0)
                {
                    movementComplete = true;
                    textBoxForTutorial.GetComponent<Text>().text = "Move to the nearest bush!";
                }
            }
            if(movementComplete && !attackComplete && nearBush && !waveComplete)
            {
                aButton.enabled = true;
                textBoxForTutorial.GetComponent<Text>().text = "Fight the wave using the ''A'' button !";

            }
            if (waveComplete && !itemPicked)
            {
                textBoxForTutorial.GetComponent<Text>().text = "Jump using the ''B'' button,\n and pick up items by walking over them!";
            }
            if(itemPicked && !interacted)
            {
                textBoxForTutorial.GetComponent<Text>().text = "Catch some sleep, go near the tent, \n it's interactable";
            }

        }
        else
        {
            textBoxForTutorial.SetActive(false);
        }
        
    }
}
