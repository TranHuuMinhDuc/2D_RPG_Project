using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private void OnEnable()
    {
        SkillsSlot.OnSkillPointsSpent += HandleSkillPointsSpent;
    }
    private void OnDisable()
    {
        SkillsSlot.OnSkillPointsSpent -= HandleSkillPointsSpent;
    }

    private void HandleSkillPointsSpent(SkillsSlot slot)
    {
        String skillName = slot.skillDetailsSO.skillName;
        switch (skillName)
        {
            case "Max Health Boost":
                StatManager.instance.updateIncreasedMaxHealth(2);
                break;
            case "Bow Weapon":
                //TODO: Implement bow weapon logic
                break;
            default:
                Debug.LogWarning("Unknown skill name: " + skillName);
                break;
        }
    }
}
