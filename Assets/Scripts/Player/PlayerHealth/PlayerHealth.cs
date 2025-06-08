using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Snorx.Data;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public Slider healthSlider;
    public PlayerDetails PlayerDetailsSO;

    private void Start()
    {
        currentHealth = PlayerDetailsSO.playerMaxHealth;
    }
    private void Update()
    {
        updateHealthUI();
    }
    public void changeHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, PlayerDetailsSO.playerMaxHealth);
    }
    private void updateHealthUI()
    {
        healthSlider.maxValue = PlayerDetailsSO.playerMaxHealth;
        healthSlider.value = currentHealth;
    }
}
