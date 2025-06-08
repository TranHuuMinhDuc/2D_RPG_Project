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

        [Header("Enemy Combat")]
        public int enemyDamage;
        public int enemyMaxHealth = 10;
        public float attackRange;
        public float attackCooldown;

        [Header("Enemy Movement")]
        public float enemySpeed;
        public float chaseRange;
        public bool isChasing;
        public float timeOutOfRange;
        public int stopChaseTime;
        public bool isTouchingPlayer;
       
    }
}

