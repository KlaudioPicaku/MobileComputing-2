using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class fadeInAndOut : MonoBehaviour
{
    [SerializeField] Image mainImage;
    public float speed;
    public float fadeOutSpeed=1f;
    public float delayedIn = 0f;
    [SerializeField] bool faded = false;
    public float timeUp = 0f;
    [SerializeField] GameObject loadingScreen;

    Color color1;
    // Start is called before the first frame update
    public void Awake()
    {
        color1 = mainImage.color;
        mainImage.color = new Color(1f,1f,1f,0f);

    }
    private void FixedUpdate()
    {
        delayedIn += Time.deltaTime;
        if (delayedIn >= 1.5f)
        {
            if (fadeOutSpeed >= 0)
            {
                timeUp += Time.deltaTime;

                if (speed <= 1f)
                {
                    colorfade();
                    speed += (Time.deltaTime * 4);
                }
                else if (timeUp >= 4f && fadeOutSpeed >= 0f)
                {
                    fadeOut();
                    fadeOutSpeed -= (Time.deltaTime * 4);

                }
            }
            else
            {
                mainImage.color = new Color(1f, 1f, 1f, 0f);
                GameObject.FindGameObjectWithTag("Dialog").GetComponent<DialogueManager>().closeDialog();
                this.gameObject.SetActive(false);
            }
        }
    }
    private void colorfade()
    {
        mainImage.color = new Color(color1.r, color1.g, color1.b, speed);
    }
    public void fadeOut()
    {
        mainImage.color = new Color(color1.r, color1.g, color1.b, fadeOutSpeed);
    }
}
