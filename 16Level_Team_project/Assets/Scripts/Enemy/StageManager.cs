using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    public int currentStage = 1;
    public int enemiesKilled = 0;
    public int enemiesToKill = 5;
    public int maxClearedStage = 1;

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
        stageUiManager.UpdateKillCountUI(enemiesKilled, enemiesToKill);

        if (enemiesKilled >= enemiesToKill)
        {
            if (maxClearedStage < currentStage + 1)
            {
                maxClearedStage = currentStage + 1;
            }

            currentStage++;
            enemiesKilled = 0;

            stageUiManager.UpdateStageUI(currentStage);
            stageUiManager.UpdateKillCountUI(enemiesKilled, enemiesToKill);
        }

        enemySpawner.SpawnEnemy();
    }
    public void GoToPreviousStage()
    {
        if (currentStage > 1)
        {
            currentStage--;
            enemiesKilled = 0;
            stageUiManager.UpdateStageUI(currentStage);
            stageUiManager.UpdateKillCountUI(enemiesKilled, enemiesToKill);
            enemySpawner.ForceRespawnEnemy();
        }
    }

    public void GoToNextStage()
    {
        if (currentStage + 1 <= maxClearedStage && currentStage < 5)
        {
            currentStage++;
            enemiesKilled = 0;

            stageUiManager.UpdateStageUI(currentStage);
            stageUiManager.UpdateKillCountUI(enemiesKilled, enemiesToKill);
            enemySpawner.ForceRespawnEnemy();
        }
    }
}
