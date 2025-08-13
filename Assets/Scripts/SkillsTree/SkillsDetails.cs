using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snorx.SkillsTree
{
    [CreateAssetMenu(fileName = "SkillsDetails", menuName = "ScriptableObjects/SkillsTree/SkillsDetails")]
    public class SkillsDetails : ScriptableObject
    {
        public string skillName;
        public string skillDescription;
        public int skillLevel;
        public int maxSkillLevel;
        public Sprite skillIcon;
    }
}

