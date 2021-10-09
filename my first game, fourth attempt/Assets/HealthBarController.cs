using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] Slider slider;
    private void Start()
    {
        slider = GetComponent<Slider>();
    }
    public void setMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetHealth(float health)
    {
        slider.value =slider.value - health;
    }
    public void SetDamage(float damage)
    {
        SetHealth(damage);
    }
    public float GetHealth()
    {
        return slider.value;
    }
}
