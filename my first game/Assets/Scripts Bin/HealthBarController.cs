using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Slider slider;
    [SerializeField] bool isDead=false;
    [SerializeField] GameObject DeathScreen;
    [SerializeField] Canvas canvas;
   
    public void FixedUpdate()
    {
        if (slider.value <= 0 && !isDead)
        {
            Instantiate(DeathScreen, canvas.transform);
            isDead = true;
        }
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
