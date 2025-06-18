using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;


/// <summary>
/// Get~() : 읽기전용함수,Set~() : 조건부 쓰기전용 함수
/// </summary>
public class Player
{
    UIPlayer uiPlayer;
    public void SetUIPlayer(UIPlayer ui)
    {
        uiPlayer = ui;
    }
    public Player()
    {
        Debug.Log("생성자 호출");
    }

    [SerializeField]
    SOPlayerStat playerstatSO;

    int basicSTR = 11;
    int basicDEF = 12;
    int basicHP = 100;
    float basicCRT = 0f;
    float critDamageMultiplier = 2f;

    BigInteger basicGold = new BigInteger(200000);

    BigInteger nowSTRUpgradeCost = new BigInteger(0);
    BigInteger nowDEFUpgradeCost = new BigInteger(0);
    BigInteger nowHPUpgradeCost = new BigInteger(0);
    BigInteger nowCRTUpgradeCost = new BigInteger(0);

    int basicSTRLevel = 0;
    int basicDEFLevel = 0;
    int basicHPLevel = 0;
    int basicCRTLevel = 0;


    public float GetCritDamageMultiplier()
    {
        return critDamageMultiplier;
    }
    public void AddCritDamageMultiplier(float value)
    {
        if (value > 0)
            critDamageMultiplier += value;
        else
            Debug.Log("치명타 데미지 증가값 오류");
    }

    public BigInteger GetNowSTRUpgradeCost()
    {
        return nowSTRUpgradeCost;
    }
    public void SetNowSTRUpgradeCost(BigInteger accumulateCost)
    {
        if(accumulateCost > 0 && 0 <= nowSTRUpgradeCost)
        {
            nowSTRUpgradeCost += accumulateCost;

        }
        else { Debug.Log("공격력 스탯 누적 강화비용에서 오류 발생"); }
    }
    public BigInteger GetNowDEFUpgradeCost()
    {
        return nowDEFUpgradeCost;
    }
    public void SetNowDEFUpgradeCost(BigInteger accumulateCost)
    {

        if (accumulateCost > 0 && 0 <= nowDEFUpgradeCost)
        {
            nowDEFUpgradeCost += accumulateCost;

        }
        else { Debug.Log("방어력 스탯 누적 강화비용에서 오류 발생"); }
    }
    public BigInteger GetNowHPUpgradeCost()
    {
        return nowHPUpgradeCost;
    }
    public void SetNowHPUpgradeCost(BigInteger accumulateCost)
    {
        if (accumulateCost > 0 && 0 <= nowHPUpgradeCost)
        {
            nowHPUpgradeCost += accumulateCost;

        }
        else { Debug.Log("체력 스탯 누적 강화비용에서 오류 발생"); }
    }
    public BigInteger GetNowCRTUpgradeCost()
    {
        return nowCRTUpgradeCost;
    }
    public void SetNowCRTUpgradeCost(BigInteger accumulateCost)
    {

        if (accumulateCost > 0 && 0 <= nowCRTUpgradeCost)
        {
            nowCRTUpgradeCost += accumulateCost;

        }
        else { Debug.Log("치명타 스탯 누적 강화비용에서 오류 발생"); }
    }

    public int GetBasicSTR()
    {
        return basicSTR;
    }

    /// <summary>
    /// SetBasicSTR(int값), 올릴 공격력 매개변수 int값
    /// </summary>
    /// <param name="BasicSTR">증가시킬 공격력 값 (int)</param>
    public void SetBasicSTR(int BasicSTR)
    {
        if (basicGold >= nowSTRUpgradeCost)
        {
            basicSTR += BasicSTR;
        }
        else { Debug.Log("공격력강화에서 오류 발생"); }
    }
    public int GetBasicDEF()
    {
        return basicDEF;
    }

    public void SetBasicDEF(int BasicDEF)
    {
        if (basicGold >= nowDEFUpgradeCost)
        {
            basicDEF += BasicDEF;
        }
        else { Debug.Log("방어력강화에서 오류 발생"); }
    }
    public int GetBasicHP()
    {
        return basicHP;
    }
    public void SetBasicHP(int BasicHP)
    {
        if (basicGold >= nowHPUpgradeCost)
        {
            basicHP += BasicHP;
        }
        else { Debug.Log("체력강화에서 오류 발생"); }
    }
    public float GetBasicCRT()
    {
        return basicCRT;
    }
    public void SetBasicCRT(float BasicCRT)
    {
        //이걸 여기서 판정하면 안되네  100일때 조건이 만족하니까 105가 되는데
        //매니저에서 100넘어가면 다 100으로 판정하거나
        //버튼에 get으로 판정 해야할듯
        if (basicCRT < 100 && basicGold >= nowCRTUpgradeCost)
        {
            basicCRT += BasicCRT;
        }
        else { Debug.Log("치명타강화에서 오류 발생"); }
    }
    public BigInteger GetBasicGold()
    {
        return basicGold;
    }
    /// <summary>
    /// SetBasicGold(업그레이드비용)
    /// </summary>
    /// <param name="Gold"></param>
    public void SetBasicGold(BigInteger Gold)
    {
        BigInteger before = basicGold;

        if (basicGold >= Gold)
        {
            basicGold -= Gold;

            if(uiPlayer != null)
            {
                uiPlayer.AnimateGold(before, basicGold, 2f);
            }
            else { Debug.Log("player클래스uiplayer할당안됨"); }
        }
        else { Debug.Log("돈쓰기 오류 발생"); }
    }

    public int GetBasicSTRLevel()
    {
        return basicSTRLevel;
    }
    public void SetBasicSTRLevel(int basicSTRLevel)
    {

        if (this.basicSTRLevel <= 999_999 && basicGold >= nowSTRUpgradeCost)
        {
            this.basicSTRLevel += basicSTRLevel;
        }
        else { Debug.Log("공격력 레벨 강화에서 오류발생"); }

    }


    public int GetBasicDEFLevel()
    {
        return basicDEFLevel;
    }
    public void SetBasicDEFLevel(int basicDEFLevel)
    {

        if (this.basicDEFLevel <= 999_999 && basicGold >= nowDEFUpgradeCost)
        {
            this.basicDEFLevel += basicDEFLevel;
        }
        else { Debug.Log("방어력 레벨 강화에서 오류발생"); }
    }
    public int GetBasicHPLevel()
    {
        return basicHPLevel;
    }
    public void SetBasicHPLevel(int basicHPLevel)
    {
        if (this.basicHPLevel <= 999_999 && basicGold >= nowHPUpgradeCost)
        {
            this.basicHPLevel += basicHPLevel;
        }
        else { Debug.Log("체력 레벨 강화에서 오류발생"); }
    }
    public int GetBasicCRTLevel()
    {
        return basicCRTLevel;
    }
    public void SetBasicCRTLevel(int basicCRTLevel)
    {


        if (basicCRT < 100 && basicGold >= nowCRTUpgradeCost)
        {
            this.basicCRTLevel += basicCRTLevel;
        }
        else { Debug.Log("치명타 레벨 강화에서 오류발생"); }
    }

    /// <summary>
    /// 이 매개변수엔 원하는 만큼 돈복사가능BigInteger
    /// </summary>
    /// <param name="Gold"></param>
    public void CheatGoldMethod(BigInteger Gold)
    {
        BigInteger before = basicGold;
        basicGold += Gold;

        if(uiPlayer != null)
        {
            uiPlayer.AnimateGold(before, basicGold, 2f);
        }
        else { Debug.Log("player클래스uiplayer할당안됨"); }

    }





}
