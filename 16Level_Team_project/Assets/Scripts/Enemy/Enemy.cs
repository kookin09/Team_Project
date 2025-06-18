using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyStatsTable statsTable;
    public int currentStage;

    private int monsterHP;
    private EnemySpawner spawner;

    public UnityEngine.UI.Image healthBarFill; 

    private int maxHp;
    private int currentHp;
    private Player player;
    public void SetPlayer(Player playerRef)
    {
        player = playerRef;
    }

    public void Initialize(EnemySpawner enemySpawner, int stage, Player playerRef)
    {
        spawner = enemySpawner;
        currentStage = stage;
    }

    void Start()
    {
        if (statsTable == null)
        {
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
            maxHp = data.monsterHP;
            currentHp = maxHp;
            monsterHP = maxHp;

            UpdateHealthBar();

        }
    }

    private void UpdateHealthBar()
    {
        if (healthBarFill != null)
            healthBarFill.fillAmount = (float)currentHp / maxHp;
    }

    private void Die()
    {
        EnemyStageData data = statsTable.GetStatsForStage(currentStage);
        spawner.OnEnemyDefeated();
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        int playerDamage = GameManager.Instance.player.GetBasicSTR();  // ✅ 이걸로 변경
        Debug.Log("플레이어 데미지: " + playerDamage);
        TakeDamage(playerDamage);
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        monsterHP = currentHp;

        if (currentHp <= 0)
        {
            currentHp = 0;
            Die();
        }
        UpdateHealthBar();
    }
}
