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
        public int playerLevel = 1;

        [Header("Player Movement Details")]
        public float playerSpeed = 5f;

        [Header("Player Health Details")]
        public int playerMaxHealth = 100;

        [Header("Player Combat Details")]
        public int playerDamage = 1;
        public float attackCoolDown = 1f;
        public float knockBackForce = 5f;
        public float enemyStunTime = 1f;
        public float weaponRange     = 1;

        [Header("Player State Details")]
        public bool isKnockedBack;
        public bool isAttacking;
    }
}

