using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public int currentStage = 1;
    public EnemySpawner enemySpawner;
    public StageUIManager stageUiManager;

    void Start()
    {
        StartStage(currentStage);
    }

    public void StartStage(int stage)
    {
        currentStage = stage;
        enemySpawner.SpawnEnemies(stage);
        stageUiManager.UpdateStageUI(stage);
    }
}
