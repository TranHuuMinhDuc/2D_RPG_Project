using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Snorx.Data;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public Slider healthSlider;
    public PlayerDetails PlayerDetailsSO;

    private void Start()
    {
        StatManager.instance.currentPlayerHealthSM = PlayerDetailsSO.playerMaxHealth;
    }
    private void Update()
    {
        updateHealthUI();
    }
    public void changeHealth(int amount)
    {
        StatManager.instance.currentPlayerHealthSM += amount;
        StatManager.instance.currentPlayerHealthSM = Mathf.Clamp(StatManager.instance.currentPlayerHealthSM, 0,
            StatManager.instance.currentPlayerMaxHealth);
    }
    private void updateHealthUI()
    {
        healthSlider.maxValue = PlayerDetailsSO.playerMaxHealth;
        healthSlider.value = StatManager.instance.currentPlayerHealthSM;
    }
}
