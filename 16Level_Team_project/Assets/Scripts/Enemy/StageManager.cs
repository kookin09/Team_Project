using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    public int currentStage = 1;
    public int enemiesKilled = 0;
    public int enemiesToKill = 5;

    public EnemySpawner enemySpawner;
    public StageUIManager stageUiManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        stageUiManager.UpdateStageUI(currentStage);
        stageUiManager.UpdateKillCountUI(enemiesKilled, enemiesToKill);
        enemySpawner.SpawnEnemy();
    }
    public void OnEnemyKilled()
    {
        enemiesKilled++;
        stageUiManager.UpdateKillCountUI(enemiesKilled,enemiesToKill);

        if(enemiesKilled >= enemiesToKill)
        {
            currentStage++;
            enemiesKilled = 0;

            stageUiManager.UpdateStageUI(currentStage);
            stageUiManager.UpdateKillCountUI(enemiesKilled, enemiesToKill);
        }
        enemySpawner.SpawnEnemy();
    }
}
