using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;


/// <summary>
/// Get~() : 읽기전용함수,Set~() : 조건부 쓰기전용 함수
/// </summary>
public class Player 
{

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
    
    BigInteger basicGold = new BigInteger(200000);
    BigInteger UpgradeCost = new BigInteger(5000); //이건 나중에 테이블에서 가져와야함

    int basicSTRLevel = 1;
    int basicDEFLevel = 1;
    int basicHPLevel = 1;
    int basicCRTLevel = 1;




    public BigInteger GetUpgradeCost()
    {
        return UpgradeCost;
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
        if (basicGold >= UpgradeCost)
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
        if (basicGold >= UpgradeCost)
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
        if (basicGold >= UpgradeCost)
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
        if (basicCRT<=100 && basicGold >= UpgradeCost)
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
        if (basicGold >= UpgradeCost)
        {
            basicGold -= Gold;
        }
        else { Debug.Log("돈쓰기 오류 발생"); }
    }

    public int GetBasicSTRLevel()
    {
        return basicSTRLevel;
    }
    public void SetBasicSTRLevel(int basicSTRLevel)
    {
        this.basicSTRLevel += basicSTRLevel;
    }


    public int GetBasicDEFLevel()
    {
        return basicDEFLevel;
    }
    public void SetBasicDEFLevel(int basicDEFLevel)
    {
        this.basicDEFLevel += basicDEFLevel;
    }
    public int GetBasicHPLevel()
    {
        return basicHPLevel;
    }
    public void SetBasicHPLevel(int basicHPLevel)
    {
        this.basicHPLevel += basicHPLevel;
    }
    public int GetBasicCRTLevel()
    {
        return basicCRTLevel;
    }
    public void SetBasicCRTLevel(int basicCRTLevel)
    {
        this.basicCRTLevel += basicCRTLevel;
    }

    /// <summary>
    /// 이 매개변수엔 원하는 만큼 돈복사가능BigInteger
    /// </summary>
    /// <param name="Gold"></param>
    public void CheatGoldMethod(BigInteger Gold)
    {
        basicGold += Gold;
    }

    


}
