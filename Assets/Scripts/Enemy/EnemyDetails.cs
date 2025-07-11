using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snorx.EnemyData
{
    [CreateAssetMenu(fileName = "EnemyDetails", menuName = "ScriptableObjects/Enemy/EnemyDetails")]
    public class EnemyDetails : ScriptableObject
    {
        [Header("Enemy Details")]
        public string enemyName;

        [Header("Enemy Movement")]
        public float enemySpeed;
        public float enemyDetectionRange;

        [Header("Enemy Combat")]
        public int enemyDamage;
        public int enemyMaxHealth = 10;
        public float attackRange;
        public float attackCooldown;
        public float attackCooldownTimer;
        public float weaponRange;
        public float knockBackForce;
        public float playerStunTime;
    }
}

