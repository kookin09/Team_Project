using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public PlayerData playerData;
    public Player player { get; private set; }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        player = new Player();
        DontDestroyOnLoad(gameObject);

        LoadPlayerData();       //      나중에 구현되는 부분
    }

    public void SavePlayerData()      //      나중에 구현 
    {

    }

    public void LoadPlayerData()      //      나중에 구현
    {

    }

    public void UpgradeCriticalStat()
    {
        var player = GameManager.Instance.player;

        BigInteger upgradeCost = player.GetNowCRTUpgradeCost();

        if (player.GetBasicGold() >= upgradeCost)
        {
            // 골드 차감
            player.SetBasicGold(upgradeCost);

            // 레벨 증가
            player.SetBasicCRTLevel(1);

            // 확률 증가 1% = 0.01f
            player.SetBasicCRT(0.01f);

            // 누적 강화 비용 증가
            player.SetNowCRTUpgradeCost(upgradeCost);

            // ClickEvent.cs에 최신 확률 전달
            FindObjectOfType<ClickEvent>().UpdateCriticalChance(player.GetBasicCRT());

            Debug.Log($"치명타 레벨: {player.GetBasicCRTLevel()} / 확률: {player.GetBasicCRT() * 100}%");
        }
        else
        {
            Debug.Log("치명타 강화에 필요한 골드가 부족합니다!");
        }
    }
    public void UpgradeCriticalDamage()
    {
        var player = GameManager.Instance.player;
        BigInteger cost = new BigInteger(200); // 예시 비용

        if (player.GetBasicGold() >= cost)
        {
            player.SetBasicGold(cost); // 골드 차감
            player.AddCritDamageMultiplier(0.5f); // 배율 +0.5
            Debug.Log($"[강화됨] 치명타 데미지 배율: x{player.GetCritDamageMultiplier()}");
        }
        else
        {
            Debug.Log("골드가 부족해 치명타 데미지를 강화할 수 없습니다.");
        }
    }


    // 게임 시작 시 저장된 데이터로 Player cs 설정
    public void InitializePlayerFromData()
    {
        player = new Player();

        player.SetBasicSTR(playerData.basicSTR);        //     저장된 값 불러오기
        player.SetBasicDEF(playerData.basicDEF);
        player.SetBasicHP(playerData.basicHP);
        player.SetBasicCRT(playerData.basicCRT);

        player.CheatGoldMethod(new BigInteger(playerData.gold));
    }

    // 캐릭터 강화 후 저장하는 코드
    public void SyncPlayerToData()
    {
        playerData.basicSTR = player.GetBasicSTR();     //      Player cs 에서 STR을 가져와 저장
        playerData.basicDEF = player.GetBasicDEF();
        playerData.basicHP = player.GetBasicHP();
        playerData.basicCRT = player.GetBasicCRT();

        //playerData.gold = float.Parse(player.GetGold().ToString());       //      아직 GetGold 없음
    }

}
