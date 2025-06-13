using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<EnemyStatsTable> enemyPools;
    public GameObject enemyPrefab;
    public Transform spawnPoints;

    private GameObject currentEnemy;

    public void SpawnEnemy()
    {
        if(currentEnemy == null)
        {
            var randomStatsTable = enemyPools[Random.Range(0, enemyPools.Count)];

            currentEnemy = Instantiate(enemyPrefab, spawnPoints.position, Quaternion.identity);
            Enemy enemy = currentEnemy.GetComponent<Enemy>();
            enemy.statsTable = randomStatsTable;
            enemy.Initialize(this, StageManager.Instance.currentStage);
        }
    }
    public void OnEnemyDefeated()
    {
        currentEnemy = null;
        StageManager.Instance.OnEnemyKilled();
    }
}
