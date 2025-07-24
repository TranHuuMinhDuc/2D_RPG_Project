using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public int spawnInterval = 5;
    private GameObject[] spawnedEnemy;
    
    private void Start()
    {
        spawnedEnemy = new GameObject[spawnPoints.Length];
        StartCoroutine(SpawnEnemyLoop());
    }
    IEnumerator SpawnEnemyLoop()
    {
        while (true)
        {
            checkAndSpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
            
    }
    private void checkAndSpawnEnemy()
    {
        for(int i = 0; i <spawnPoints.Length; i++)
        {
            if(spawnedEnemy[i] == null)
            {
                GameObject enemy = Instantiate(enemyPrefab, spawnPoints[i].position, Quaternion.identity);
                spawnedEnemy[i] = enemy;
            }
        }
    }
}
