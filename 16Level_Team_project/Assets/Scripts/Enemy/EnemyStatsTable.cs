using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatsTable", menuName = "GameData/Enemy Stats Table")]
public class EnemyStatsTable : ScriptableObject
{
    public string enemyName;
    public Sprite enemySprite;
    public List<EnemyStageData> stageStats;

    public EnemyStageData GetStatsForStage(int stage)
    {
        return stageStats.Find(data => data.stage == stage);
    }
}
