using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snorx.Data
{
    [CreateAssetMenu(fileName = "PlayerDetails", menuName = "ScriptableObjects/Player/PlayerDetails")]
    public class PlayerDetails : ScriptableObject
    {
        [Header("Player Details")]
        public string playerName;
        public float playerSpeed = 5f;
        public int playerMaxHealth = 100;
        public int playerDamage = 1;
        public float attackCoolDown;

        public bool isKnockedBack;
        public bool isAttacking;
    }
}

