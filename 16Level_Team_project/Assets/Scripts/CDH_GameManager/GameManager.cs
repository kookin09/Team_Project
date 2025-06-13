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
