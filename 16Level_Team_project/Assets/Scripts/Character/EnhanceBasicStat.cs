using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEditor.PackageManager;
using UnityEngine;

public class EnhanceBasicStat : MonoBehaviour
{
    [SerializeField] private GameObject noGoldWarning;

    [SerializeField]
    SOPlayerStat so;

    private void Start()
    {
        StartCoroutine(TestCoroutine());
    }
    private IEnumerator TestCoroutine()
    {
        Debug.Log("🧪 코루틴 실행 테스트 성공!");
        yield return null;
    }

    //public  BigInteger =

    //강화가 가능한지만 판정하셈
    public bool EnhancePlayerBsaicStat(string statName,out BigInteger costCoin)
    {

        BigInteger nowSTRUpgradeCost = GameManager.Instance.player.GetNowSTRUpgradeCost();
        BigInteger nowDEFUpgradeCost = GameManager.Instance.player.GetNowDEFUpgradeCost();
        BigInteger nowHPUpgradeCost = GameManager.Instance.player.GetNowHPUpgradeCost();
        BigInteger nowCRTupgradeCost = GameManager.Instance.player.GetNowCRTUpgradeCost();



        BigInteger nowGold = GameManager.Instance.player.GetBasicGold();

        int nowLevel = GetStatLevel(statName);

        for (int i = 0; i < so.GetLevelCharTable().Length; i++)
        {


            //Debug.Log((100 * i + 1) + "~" + (i * 100 + 100));

            //minLevelRange ~ MaxLevelRange
            //1~100 maxLevelRange100 ,
            //101~200 maxLevelRange200,
            //201~300 maxLevelRange300
            int minLevelRange = (100 * i );
            int maxLevelRange = (i * 100 + 99);

            //할필요가 있나? 아 해당 범위에 있을때에서 for문 break하면 되네
            //예를 들면 500이야now레벨이 그러면 if문은 첫번쨰는 1~100이니까 통과를 안하겠지? 그리고 5번쨰 for문이 될때 if문 로직이 돌아가겠지?
            if (nowLevel >= minLevelRange && nowLevel <= maxLevelRange)
            {
                //딕셔너리에서 해당키값을 바탕으로 밸류를 불러오기 위해 i번째 코스트 티어배열에서 꺼내오고 
                string nowCostTierStringKey = so.GetLevelCharTable()[i];

                //꺼내온걸 바탕으로 딕셔너리에서 골드값을 계산해
                BigInteger GoldCost = so.GetLevelCostTable()[$"{nowCostTierStringKey}"];

                //해당 i번째 딕셔너리에서 값을 꺼내오고 && 보유골드>= 소모 비용 일경우에만 강화가능 
                //여기 소모값 계산 한김에 골드도 줄여 그냥 다 계산 여기서하고UI에서는 숨기자이게 ui랑 로직이랑 분리맞는거같음이렇게하는게
                if (nowGold >= GoldCost)
                {
                    costCoin = GoldCost;

                    //골드소모
                    switch (statName)
                    {
                        case "STR":

                            if (nowGold>=nowSTRUpgradeCost) {
                                GameManager.Instance.player.SetBasicGold(GameManager.Instance.player.GetNowSTRUpgradeCost());
                                GameManager.Instance.player.SetBasicSTRLevel(1);

                                Debug.Log($"현재 {statName} 레벨은:" + GameManager.Instance.player.GetBasicSTRLevel());
                                Debug.Log($"소모 골드 범위는: {minLevelRange} ~ {maxLevelRange},소모골드:{GameManager.Instance.player.GetNowSTRUpgradeCost()},현재 골드:{nowGold}");

                                GameManager.Instance.player.SetBasicSTR(5);
                                Debug.Log("현재공격력" + GameManager.Instance.player.GetBasicSTR());
                                GameManager.Instance.player.SetNowSTRUpgradeCost(GoldCost);
                                return true;

                            }
                            else {
                                Debug.Log("str골드부족");
                                StartCoroutine(ShowNoGoldWarning());
                                return false;
                            }


                        case "DEF":
                            
                            if(nowGold >= nowDEFUpgradeCost)
                            {
                                GameManager.Instance.player.SetBasicGold(GameManager.Instance.player.GetNowDEFUpgradeCost());
                                GameManager.Instance.player.SetBasicDEFLevel(1);

                                Debug.Log($"현재 {statName} 레벨은:" + GameManager.Instance.player.GetBasicDEFLevel());
                                Debug.Log($"소모 골드 범위는: {minLevelRange} ~ {maxLevelRange},소모골드:{GameManager.Instance.player.GetNowDEFUpgradeCost()},현재 골드:{nowGold}");

                                GameManager.Instance.player.SetBasicDEF(5);
                                Debug.Log("현재공격력" + GameManager.Instance.player.GetBasicDEF());
                                GameManager.Instance.player.SetNowDEFUpgradeCost(GoldCost);


                                return true;
                            }
                            else
                            {
                                Debug.Log("def골드부족");
                                StartCoroutine(ShowNoGoldWarning());
                                return false;
                            }
                            

                        case "HP":

                            if(nowGold >= nowHPUpgradeCost)
                            {
                                GameManager.Instance.player.SetBasicGold(GameManager.Instance.player.GetNowHPUpgradeCost());
                                GameManager.Instance.player.SetBasicHPLevel(1);

                                Debug.Log($"현재 {statName} 레벨은:" + GameManager.Instance.player.GetBasicHPLevel());
                                Debug.Log($"소모 골드 범위는: {minLevelRange} ~ {maxLevelRange},소모골드:{GameManager.Instance.player.GetNowHPUpgradeCost()},현재 골드:{nowGold}");


                                GameManager.Instance.player.SetBasicHP(50);
                                Debug.Log("현재공격력" + GameManager.Instance.player.GetBasicHP());
                                GameManager.Instance.player.SetNowHPUpgradeCost(GoldCost);


                                return true;
                            }
                            else
                            {
                                Debug.Log("hp골드부족");
                                StartCoroutine(ShowNoGoldWarning());
                                return false;

                            }
                            

                        case "CRT":

                            if(nowGold >= nowCRTupgradeCost)
                            {
                                GameManager.Instance.player.SetBasicGold(GameManager.Instance.player.GetNowCRTUpgradeCost());
                                GameManager.Instance.player.SetBasicCRTLevel(1);

                                Debug.Log($"현재 {statName} 레벨은:" + GameManager.Instance.player.GetBasicCRTLevel());
                                Debug.Log($"소모 골드 범위는: {minLevelRange} ~ {maxLevelRange},소모골드:{GameManager.Instance.player.GetNowCRTUpgradeCost()},현재 골드:{nowGold}");


                                GameManager.Instance.player.SetBasicCRT(1);
                                Debug.Log("현재공격력" + GameManager.Instance.player.GetBasicCRT());
                                GameManager.Instance.player.SetNowCRTUpgradeCost(GoldCost);

                                return true;
                            }
                            else
                            {
                                Debug.Log("crt골드부족");
                                StartCoroutine(ShowNoGoldWarning());
                                return false;
                            }
                            

                        default:
                            return false;
                    }
                }
                else
                {
                    Debug.Log("실행실패ㅐㅐ");
                    StartCoroutine(ShowNoGoldWarning());
                    return false;
                }
            }
        }



        return false;


    }

    private IEnumerator ShowNoGoldWarning()
    {
        Debug.Log("showNoGoldWarning 실행됨");

        if (noGoldWarning == null)
        {
            Debug.LogWarning("noGoldWarning 오브젝트가 연결되지 않음");
            yield break;
        }

        noGoldWarning.SetActive(true);
        yield return new WaitForSeconds(1f);
        noGoldWarning.SetActive(false);
    }




    /// <summary>
    /// 스트링매개변수( STR,DEF,HP,CRT )
    /// </summary>
    /// <param name="statName"></param>
    /// <returns></returns>
    int GetStatLevel(string statName)
    {

        int nowSTRLevel = GameManager.Instance.player.GetBasicSTRLevel();
        int nowDEFLevel = GameManager.Instance.player.GetBasicDEFLevel();
        int nowHPLevel = GameManager.Instance.player.GetBasicHPLevel();

        int nowCRTLevel = GameManager.Instance.player.GetBasicCRTLevel();

        switch (statName)
        {
            case "STR":
                return nowSTRLevel;
            case "DEF":
                return nowDEFLevel;
            case "HP":
                return nowHPLevel;
            case "CRT":
                return nowCRTLevel;
            default:
                Debug.Log("스탯값을 얻는 곳에서 오류 발생");
                return 0;
        }
    }

    //나중에 10,20 이런식으로 커스텀으로 올리기위한 메서드
    //스탯이름,스탯레벨,스탯골드,스탯 증가량
    //public void CustomEnhanceStatLevel(string statName,int levels)
    //{
    //    switch (statName)
    //    {
    //        case "STR":
    //            //스탯 레벨,스탯 증가량,소모 골드
    //            GameManager.Instance.player.SetBasicSTRLevel(levels);

    //            GameManager.Instance.player.SetBasicSTR(5);
    //            break;
    //        case "DEF":
    //            GameManager.Instance.player.SetBasicDEFLevel(levels);
    //            break;
    //        case "HP":
    //            GameManager.Instance.player.SetBasicHPLevel(levels);
    //            break;
    //        case "CRT":
    //            break;
    //    }
    //}
}
