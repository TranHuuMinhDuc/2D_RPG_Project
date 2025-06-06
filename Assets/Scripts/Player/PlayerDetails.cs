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
        public GameObject playerPrefab; 
        public float ImmuneTime = 1f;
        public bool isImmuneAfterHit;
    }
}

