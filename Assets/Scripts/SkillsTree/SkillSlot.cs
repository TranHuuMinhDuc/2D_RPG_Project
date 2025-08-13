using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Snorx.SkillsTree;
using UnityEngine.UI;
using TMPro;
using System;

public class SkillsSlot : MonoBehaviour
{
    [Header("Skill Details SO")]
    public SkillsDetails skillDetailsSO;
    [Header("Skill List")]
    public List<SkillsSlot> prerequisiteSkillSlot;
    [Header("Skill State")]
    public int currentSkillLevel;
    public bool isSkillUnlocked;
    [Header("UI Elements")]
    public Image skillIcon;
    public TMP_Text skillLevelText;
    public Button skillButton;
    public static event Action<SkillsSlot> OnSkillPointsSpent;
    public static event Action<SkillsSlot> OnSkillMaxed;

    private void Start()
    {
        currentSkillLevel = skillDetailsSO.skillLevel;
    }
    public void OnValidate()
    {
        if (skillDetailsSO != null && skillLevelText != null && skillButton != null)
        {
            updateUI();
        }
    }

    #region Update Methods
    public void updateSkills()
    {
        if(isSkillUnlocked && currentSkillLevel < skillDetailsSO.maxSkillLevel)
        {
            currentSkillLevel++;
            OnSkillPointsSpent?.Invoke(this);
            if(currentSkillLevel >= skillDetailsSO.maxSkillLevel)
            {
                OnSkillMaxed?.Invoke(this);
            }
            updateUI();
        }
    }
    private void updateUI()
    {
        skillIcon.sprite = skillDetailsSO.skillIcon;
        if (isSkillUnlocked)
        {
            skillButton.interactable = true;
            skillLevelText.text = currentSkillLevel.ToString() + "/" + skillDetailsSO.maxSkillLevel.ToString(); 
            skillIcon.color = Color.white;
        }
        else
        {
            skillButton.interactable = false;
            skillLevelText.text = "Locked";
            skillIcon.color = Color.grey; 
        }
    }
    #endregion

    public void Unlock()
    {
        isSkillUnlocked = true;
        updateUI();
    }
    public bool CanUnlockSkill()
    {
        foreach (SkillsSlot slot in prerequisiteSkillSlot)
        {
            if (!slot.isSkillUnlocked || slot.currentSkillLevel < slot.skillDetailsSO.maxSkillLevel)
            {
                return false;
            }        
        }
        return true;
    }
}
