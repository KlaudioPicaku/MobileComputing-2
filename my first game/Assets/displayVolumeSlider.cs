using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayVolumeSlider : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Text text;

    // Start is called before the first frame update
    void Start()
    {
        int number = Mathf.RoundToInt(slider.value * 100);
        text.text = number.ToString() + "%";
    }

    // Update is called once per frame
    void Update()
    {
        int number = Mathf.RoundToInt(slider.value * 100);
        text.text = number.ToString() + "%";
    }
}
