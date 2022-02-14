using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class fadeInCamera : MonoBehaviour
{
    [SerializeField] Image mainImage;
    [SerializeField] float speed;
    [SerializeField] float fadeOutSpeed;
    [SerializeField] bool faded = false;
    [SerializeField] float timeUp = 0f;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] GameObject fadeInAndOut;
    Color color1;
    // Start is called before the first frame update
    public void Awake()
    {
   
        color1 = mainImage.color;
        mainImage.color = Color.black;

    }
    private void FixedUpdate()
    {
        if (!loadingScreen.activeInHierarchy)
        {
            if (fadeOutSpeed >= 0)
            {
                timeUp += Time.deltaTime;
                speed += (Time.deltaTime * 4);
                //if (speed <= 1f)
                //{
                //    colorfade();
                //    
                //}
                //else
                if (timeUp >= 1.5f && fadeOutSpeed >= 0f)
                {
                    fadeOut();
                    fadeOutSpeed -= (Time.deltaTime * 4);
                }
            }
            else
            {
                mainImage.color = new Color(1f, 1f, 1f, 0f);
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
