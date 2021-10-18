using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBarController : MonoBehaviour
{
    [SerializeField] Slider slider;
    private void Start()
    {
        slider = GetComponent<Slider>();
    }
    public void setMaxEnergy(float energy)
    {
        slider.maxValue = energy;
        slider.value = energy;
    }
    public void SetEnergy(float energy)
    {
        if (slider.value < 100)
        {
            slider.value = slider.value + energy;
        }
        else
        {
            return;
        }
    }
    public void SetGain(float energy)
    {
        SetEnergy(energy);
    }
    public float GetEnergy()
    {
        return slider.value;
    }
}
