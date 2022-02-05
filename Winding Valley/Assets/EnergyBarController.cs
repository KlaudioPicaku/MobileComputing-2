using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBarController : MonoBehaviour
{
    [SerializeField] Slider slider;
    public void setMaxEnergy(float energy)
    {
        slider.maxValue = energy;
        slider.value = energy;
    }
    public void SetEnergy(float energy)
    {
        if (slider.IsActive() &&  slider.value < 100)
        {
            slider.value = slider.value + energy;
        }
        else if (slider.IsActive() && slider.value==100 && energy < 0) 
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
        if (slider.IsActive())
        {
            return slider.value;
        }
        return 0f;
    }
}
