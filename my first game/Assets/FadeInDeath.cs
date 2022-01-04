using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInDeath : MonoBehaviour
{
    [SerializeField] Image mainImage;
    [SerializeField] Image ribbon;
    [SerializeField] Text textBox;
    [SerializeField] Text agree;
    [SerializeField] Text leave;
    [SerializeField] float speed;
    [SerializeField] float fadeOutSpeed = 1f;
    [SerializeField] bool faded = false;
    [SerializeField] float timeUp = 0f;
    [SerializeField] PlayerScript player;
    Color color1;
    Color color2;
    Color color3;
    Color color4;
    Color color5;
    // Start is called before the first frame update
    public void Awake()
    {
        player = FindObjectOfType<PlayerScript>().gameObject.GetComponent<PlayerScript>();
        mainImage = GetComponent<Image>();
        textBox = GetComponentInChildren<Text>();
        color1 = mainImage.color;
        color2 = textBox.color;
        color3 = ribbon.color;
        color4 = agree.color;
        color5 = leave.color;
        mainImage.color = new Color(1f, 1f, 1f, 0f);
        textBox.color = new Color(1f, 1f, 1f, 0f);
        ribbon.color = new Color(1f,1f,0f);
        agree.color = new Color(1f,1f,0f);
        leave.color = new Color(1f,1f,0f);

    }
    private void FixedUpdate()
    {
        if (fadeOutSpeed >= 0)
        {
            timeUp += Time.deltaTime;
            if (speed <= 1f)
            {
                colorfade();
                speed += (Time.deltaTime * 4);
            }
            //else if (timeUp >= 3f && fadeOutSpeed >= 0f)
            //{
            //    fadeOut();
            //    fadeOutSpeed -= (Time.deltaTime * 4);
            //}
        }
        else
        {
            mainImage.color = new Color(1f, 1f, 1f, 0f);
            textBox.color = new Color(1f, 1f, 1f, 0f);
            ribbon.color = new Color(1f, 1f, 1f, 0f);
            agree.color = new Color(1f, 1f, 0f);
            leave.color = new Color(1f, 1f, 0f);
            //Destroy(this.gameObject);
        }
    }
    private void colorfade()
    {
        mainImage.color = new Color(color1.r, color1.g, color1.b, speed);
        textBox.color = new Color(color2.r, color2.g, color2.b, speed);
        ribbon.color = new Color(color3.r, color3.g, color3.b, speed);
        agree.color = new Color(color4.r, color4.g, color4.b, speed);
        leave.color = new Color(color5.r, color5.g, color5.b, speed);
    }
    public void fadeOut()
    {
        mainImage.color = new Color(color1.r, color1.g, color1.b, fadeOutSpeed);
        textBox.color = new Color(color2.r, color2.g, color2.b, fadeOutSpeed);
        ribbon.color = new Color(color3.r, color3.g, color3.b, fadeOutSpeed);
        agree.color = new Color(color4.r, color4.g, color4.b, fadeOutSpeed);
        leave.color = new Color(color5.r, color5.g, color5.b, fadeOutSpeed);
    }
}
