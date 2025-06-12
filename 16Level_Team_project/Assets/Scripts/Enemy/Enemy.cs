using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyStatsTable statsTable;
    public int currentStage;

    private int monsterHP;

    void Start()
    {
        EnemyStageData data = statsTable.GetStatsForStage(currentStage);
        if (data != null)
        {
            monsterHP = data.monsterHP;

            Debug.Log($"[Stage {currentStage}] {statsTable.enemyName} - HP: {monsterHP}");
        }
        else
        {
            Debug.LogWarning("해당 스테이지에 대한 적 데이터가 없습니다!");
        }
    }
}
