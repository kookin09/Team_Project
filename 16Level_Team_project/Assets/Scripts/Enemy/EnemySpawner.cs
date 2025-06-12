using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    public void SpawnEnemies(int stage)
    {
        int enemyCount = stage * 2; // 스테이지 수에 비례해 증가

        for (int i = 0; i < enemyCount; i++)
        {
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            Instantiate(enemyPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
        }
    }
}
