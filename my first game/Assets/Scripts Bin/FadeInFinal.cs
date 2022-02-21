using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInFinal : MonoBehaviour
{
    [SerializeField] Image mainImage;
    [SerializeField] Text textBox;
    [SerializeField] float speed;
    [SerializeField] float fadeOutSpeed = 1f;
    [SerializeField] bool faded = false;
    [SerializeField] float timeUp = 0f;
    Color color1;
    Color color2;
    // Start is called before the first frame update
    void Start()
    {
        color1 = mainImage.color;
        color2 = textBox.color;
        mainImage.color = new Color(1f, 1f, 1f, 0f);
        textBox.color = new Color(1f, 1f, 1f, 0f);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (fadeOutSpeed >= 0)
        {
            timeUp += Time.deltaTime;
            if (speed <= 1f)
            {
                colorfade();
                speed += (Time.deltaTime * 4);
            }
        }
    }
    private void colorfade()
    {
        mainImage.color = new Color(color1.r, color1.g, color1.b, speed);
        textBox.color = new Color(color2.r, color2.g, color2.b, speed);
    }
}
