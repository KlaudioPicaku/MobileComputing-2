using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] bool isDead=false;
    [SerializeField] GameObject DeathScreen;
    [SerializeField] Canvas canvas;
    private void Start() 
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        slider = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Slider>() ;
    }
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
