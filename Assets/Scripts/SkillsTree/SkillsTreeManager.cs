using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillsTreeManager : MonoBehaviour
{
    [Header("Skills Tree Settings")]
    public int avaliableSkillPoints;
    public TMP_Text skillPointText;
    [Header("Skills Slots")]
    public SkillsSlot[] skillsSlots;
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
        SkillsSlot.OnSkillPointsSpent += HandleSkillPoints;
        SkillsSlot.OnSkillMaxed += HandleSkillMaxed;
    }
    private void OnDisable()
    {
        SkillsSlot.OnSkillPointsSpent -= HandleSkillPoints;
        SkillsSlot.OnSkillMaxed -= HandleSkillMaxed;
    }
    #endregion

    #region Handle Skills Methods
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
