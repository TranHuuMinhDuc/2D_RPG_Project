using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillsTreeManager : MonoBehaviour
{
    [Header("Skills Tree Settings")]
    public GameObject skillsTreePanel;
    public int avaliableSkillPoints;
    public TMP_Text skillPointText;
    public StatsUI statsUI;
    [Header("Skills Slots")]
    public SkillsSlot[] skillsSlots;
    private PlayerInputSystem inputSystem;

    private void Awake()
    {
        inputSystem = new PlayerInputSystem();
    }
    private void Start()
    {
        foreach (SkillsSlot slot in skillsSlots)
        {
            slot.skillButton.onClick.AddListener(() => CheckAvaliableSkillPoints(slot));
        }
        updateSkillPoints(0);
    }
    private void CheckAvaliableSkillPoints(SkillsSlot slot)
    {
        if(avaliableSkillPoints > 0)
        {
            slot.updateSkills();
        }
    }
    public void updateSkillPoints(int amount)
    {
        avaliableSkillPoints += amount;
        skillPointText.text = "Skill Points: " + avaliableSkillPoints;
    }


    #region OnEnable/Disable Methods
    private void OnEnable()
    {
        inputSystem.UI.Enable();
        inputSystem.UI.SkillsTree.performed += OnToggleSKillsTree;
        SkillsSlot.OnSkillPointsSpent += HandleSkillPoints;
        SkillsSlot.OnSkillMaxed += HandleSkillMaxed;
        XPManager.OnLevelUp += updateSkillPoints;
    }
    private void OnDisable()
    {
        
        inputSystem.UI.SkillsTree.performed -= OnToggleSKillsTree;
        SkillsSlot.OnSkillPointsSpent -= HandleSkillPoints;
        SkillsSlot.OnSkillMaxed -= HandleSkillMaxed;
        XPManager.OnLevelUp -= updateSkillPoints;
        inputSystem.UI.Disable();
    }
    private void OnToggleSKillsTree(InputAction.CallbackContext context)
    {
        bool isActive = !skillsTreePanel.activeSelf;
        skillsTreePanel.SetActive(isActive);
        statsUI.pauseGame(isActive);
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

    #region HandleSkillsMethods
    private void HandleSkillPoints(SkillsSlot skillSlot)
    {
        if(avaliableSkillPoints > 0)
        {
            updateSkillPoints(-1);  
        }
    }
    private void HandleSkillMaxed(SkillsSlot skillSlot)
    {
        foreach (SkillsSlot slot in skillsSlots)
        {
            if(!slot.isSkillUnlocked && slot.CanUnlockSkill())
            {
                slot.Unlock();
            }
        }
    }
    #endregion
    
}
