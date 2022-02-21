using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class fadeInLevelName : MonoBehaviour
{
    [SerializeField] Text textBox;
    [SerializeField] float speed;
    [SerializeField] float fadeOutSpeed = 1f;
    [SerializeField] bool nameSet= false;
    [SerializeField] float timeUp = 0f;
    [SerializeField] GameObject loadingScreen;
    Color color1;
    Color color2;
    private void OnEnable()
    {
        fadeOutSpeed = 2f;
        speed = 0f;
        timeUp = 0f;
        nameSet = false;
    }
    // Start is called before the first frame update
    public void Awake()
    {
        textBox = GetComponentInChildren<Text>();
        color2 = textBox.color;
        textBox.color = new Color(1f, 1f, 1f, 0f);
    }
    private void FixedUpdate()
    {
        if (!loadingScreen.activeInHierarchy)
        {
            if (!nameSet)
            {
                textBox.text = SceneManager.GetSceneAt(1).name;
                nameSet = true;
            }
            if (fadeOutSpeed >= 0)
            {
                timeUp += Time.deltaTime;
                if (speed <= 1f)
                {
                    colorfade();
                    speed += (Time.deltaTime * 4);
                }
                else if (timeUp >= 3f && fadeOutSpeed >= 0f)
                {
                    fadeOut();
                    fadeOutSpeed -= (Time.deltaTime * 4);
                }
            }
            else
            {
                //textBox.color = new Color(1f, 1f, 1f, 0f);
                //exclamationPoint.color = new Color(1f, 1f, 1f, 0f);
                textBox.text = "";
                this.gameObject.SetActive(false);
            }
        }
    }
    private void colorfade()
    {

        textBox.color = new Color(color2.r, color2.g, color2.b, speed);
        //exclamationPoint.color = new Color(color3.r, color3.g, color3.b, speed);
    }
    public void fadeOut()
    {
        textBox.color = new Color(color2.r, color2.g, color2.b, fadeOutSpeed);
        //exclamationPoint.color = new Color(color3.r, color3.g, color3.b, fadeOutSpeed);
    }
}
