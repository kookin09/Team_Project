using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using TMPro;
using static UnityEditor.Experimental.GraphView.GraphView;


public class UIPlayer : MonoBehaviour
{
    [SerializeField]
    Player player ;

    public TextMeshProUGUI ouputgold;
    public TextMeshProUGUI curSTR;
    public TextMeshProUGUI curDEF;
    public TextMeshProUGUI curHP;
    public TextMeshProUGUI curCRT;


    //테스트골드
    public TextMeshProUGUI moneyGold;
    public string outputStringGold;

    static readonly string[] stringPreset = {
        "", "K", "M", "B", "T", // 10^3 ~ 10^12
        "aa", "ab", "ac", "ad", "ae", "af", "ag", "ah", "ai", "aj", "ak", "al","am","an","aO","ap","aq","ar","as","at","au","av","aw","ax","ay","az" // 10^15 ~ 10^48
    };


    void Start()
    {
        player = GameManager.Instance.player;
    }

    void Update()
    {
        UpdateUI();

        
    }



    public string CalGoldOutPut(BigInteger goldOutput)
    {
        BigInteger moneyTier = new BigInteger(999_999_999);
        int index = 0;

        if (goldOutput <= moneyTier)
        {
            return goldOutput.ToString("N0");
        }

        while (goldOutput > moneyTier && index < stringPreset.Length - 1)
        {
            goldOutput /= 1_000;
            index++;
        }

        return goldOutput.ToString("N0") + stringPreset[index];
    }
    void UpdateUI()
    {
        //현재 공격력
        curSTR.text = $"Now STR :{GameManager.Instance.player.GetBasicSTR()}";
        curDEF.text = $"Now DEF :{GameManager.Instance.player.GetBasicDEF()}";
        curHP.text = $"Now HP :{GameManager.Instance.player.GetBasicHP()}";
        curCRT.text = $"Now CRT :{GameManager.Instance.player.GetBasicCRT()}";
        //골드
        ouputgold.text = CalGoldOutPut(GameManager.Instance.player.GetbasicGold());

    }


    public void UpgradeSTR()
    {
        GameManager.Instance.player.SetBasicGold(GameManager.Instance.player.GetUpgradeCost());
        GameManager.Instance.player.SetBasicSTR(5);
    }
    public void UpgradeDEF()
    {
        GameManager.Instance.player.SetBasicGold(GameManager.Instance.player.GetUpgradeCost());
        GameManager.Instance.player.SetBasicDEF(5);
    }
    public void UpgradeHP()
    {
        GameManager.Instance.player.SetBasicGold(GameManager.Instance.player.GetUpgradeCost());
        GameManager.Instance.player.SetBasicHP(5);
    }
    public void UpgradeCRT()
    {
        float addCRT = 5;//추후 테이블에 따라 달라질것임
        float maxCRT = player.GetBasicCRT();
        //float calCRT = player.SetBasicCRT(5);

        float result = maxCRT + addCRT;
        if (player.GetbasicGold() >= player.GetUpgradeCost() && result <= 100)
        {
            GameManager.Instance.player.SetBasicGold(GameManager.Instance.player.GetUpgradeCost());
            GameManager.Instance.player.SetBasicCRT(5);
        }
        else
        {
            Debug.Log("치명타율을 100%를 넘을 수 없음.");
        }
    }

    public void CheatGold()
    {

        GameManager.Instance.player.CheatGoldMethod(999999999);
    }
    public void CheatGold2()
    {

        GameManager.Instance.player.CheatGoldMethod(999999999999);
    }
}


#region 더미코드

/*

BigInteger result;
if (BigInteger.TryParse(outputStringGold, out result))
{
    moneyGold.text = CalGoldOutPut(result);
}



*/
#endregion