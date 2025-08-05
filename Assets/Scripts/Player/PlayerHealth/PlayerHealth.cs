using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Snorx.Data;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public Slider healthSlider;
    
    private void Update()
    {
        updateHealthUI();
    }
    public void changeHealth(int amount)    
    {
        StatManager.instance.currentPlayerHealth += amount;
        StatManager.instance.currentPlayerHealth = Mathf.Clamp(StatManager.instance.currentPlayerHealth, 0,
            StatManager.instance.currentPlayerMaxHealth);
    }
    private void updateHealthUI()
    {
        healthSlider.maxValue = StatManager.instance.currentPlayerMaxHealth;
        healthSlider.value = StatManager.instance.currentPlayerHealth;
    }
}
