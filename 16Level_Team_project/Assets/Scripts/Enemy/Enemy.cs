using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyStatsTable statsTable;
    public int currentStage;

    private int monsterHP;
    private EnemySpawner spawner;

    public void Initialize(EnemySpawner enemySpawner, int stage)
    {
        spawner = enemySpawner;
        currentStage = stage;
    }
    void Start()
    {
        if (statsTable == null)
        {
            Debug.LogError("statsTable이 설정되지 않았습니다!");
            return;
        }
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null && statsTable.enemySprite != null)
        {
            sr.sprite = statsTable.enemySprite;
        }
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
    private void OnMouseDown()
    {
        Debug.Log("Clicked!");
        TakeDamage(1);
    }

    public void TakeDamage(int damage)
    {
        monsterHP -= damage;

        if (monsterHP <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log($"[Stage {currentStage}] {statsTable.enemyName} 처치됨");
        spawner.OnEnemyDefeated();
        Destroy(gameObject);
    }
}
