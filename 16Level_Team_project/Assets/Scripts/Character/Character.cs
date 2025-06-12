using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    SOPlayerStat playerstatSO;

    int basicSTR = 10;
    int basicDEF = 10;
    int basicHP = 100;
    float basicCRT = 0f;
    
    BigInteger basicGold = new BigInteger(200000);

    BigInteger UpgradeCost = new BigInteger(5000);

    void Start()
    {

    }


    public void CurPlayerStat()
    {
        
    }

    public BigInteger GetUpgradeCost()
    {
        return UpgradeCost;
    }

    public int GetBasicSTR()
    {
        return basicSTR;
    }

    /// <summary>
    /// 올릴 공격력 매개변수 int값
    /// </summary>
    /// <param name="BasicSTR"></param>
    public void SetBasicSTR(int BasicSTR)
    {
        if (UpgradeCost<=basicGold)
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
        if (UpgradeCost <= basicGold)
        {
            basicSTR += BasicDEF;
        }
        else { Debug.Log("방어력강화에서 오류 발생"); }
    }
    public int GetBasicHP()
    {
        return basicHP;
    }
    public void SetBasicHP(int BasicHP)
    {
        if (UpgradeCost <= basicGold)
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
        if (UpgradeCost <= basicGold)
        {
            basicCRT += BasicCRT;
        }
        else { Debug.Log("치명타강화에서 오류 발생"); }
    }
    public BigInteger GetbasicGold()
    {
        return basicGold;
    }
    public void SetBasicGold(BigInteger Gold)
    {
        if (basicGold >= UpgradeCost)
        {
            basicGold -= Gold;
        }
        else { Debug.Log("돈쓰기 오류 발생"); }
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
