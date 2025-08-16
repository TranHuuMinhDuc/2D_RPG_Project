using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class StatsUI : MonoBehaviour
{
    public GameObject[] statsSlot;
    public GameObject statsPanel;
    private PlayerInputSystem inputSystem;

    
    private void Awake()
    {
        inputSystem = new PlayerInputSystem();
    }
    #region Input System
    private void OnEnable()
    {
        inputSystem.UI.Enable();
        inputSystem.UI.StatsMenu.performed += OnToggleStats;
    }
    private void OnDisable()
    {
        inputSystem.UI.Disable();
        inputSystem.UI.StatsMenu.performed -= OnToggleStats;
    }
    private void OnToggleStats(InputAction.CallbackContext context)
    {
        bool isActive = !statsPanel.activeSelf;
        statsPanel.SetActive(isActive);
        pauseGame(isActive);
        if (isActive)
        {
            inputSystem.Player.Disable();
        }
        else
        {
            inputSystem.Player.Enable();
        }
    }
    #endregion

    public void Start()
    {
        statsUpdate();
    }

    public void statsUpdate()
    {
        statsSlot[0].GetComponentInChildren<TMP_Text>().text = "Damage: " + StatManager.instance.currentPlayerDamage;
        statsSlot[1].GetComponentInChildren<TMP_Text>().text = "Speed: " + StatManager.instance.currentPlayerSpeed;
    }
    public void pauseGame(bool isToggleStatsOn)
    {
        if(isToggleStatsOn)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
