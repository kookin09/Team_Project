using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using TMPro;
using static UnityEditor.Experimental.GraphView.GraphView;


public class UIPlayer : MonoBehaviour
{
    [SerializeField]
    Player player;

    [SerializeField]
    EnhanceBasicStat enhance;

    public TMP_InputField showmeTheMoney;
    public string input;

    [Header("현재 스탯")]
    public TextMeshProUGUI ouputgold;
    public TextMeshProUGUI curSTR;
    public TextMeshProUGUI curDEF;
    public TextMeshProUGUI curHP;
    public TextMeshProUGUI curCRT;

    [Header("소모할 스탯당 골드")]
    public TextMeshProUGUI STRCoinValue;
    public TextMeshProUGUI DEFCoinValue;
    public TextMeshProUGUI HPCoinValue;
    public TextMeshProUGUI CRTCoinValue;
    //public TextMeshProUGUI strCoinValue;

    [Header("현재 스탯 레벨")]
    public TextMeshProUGUI STRLevelTitle;
    public TextMeshProUGUI DEFLevelTitle;
    public TextMeshProUGUI HPLevelTitle;
    public TextMeshProUGUI CRTLevelTitle;


    //테스트골드
    public TextMeshProUGUI moneyGold;
    public string outputStringGold;

    static readonly string[] stringPreset = {
        "", "K", "M", "B", "T", // 10^3 ~ 10^12
        "aa", "ab", "ac", "ad", "ae", "af", "ag", "ah", "ai", "aj",
        "ak", "al","am","an","aO","ap","aq","ar","as","at","au","av",
        "aw","ax","ay","az" // 10^15 ~ 10^48

    };


    void Start()
    {
        player = GameManager.Instance.player;
        STRCoinValue.text = CalGoldOutPut(GameManager.Instance.player.GetNowUpgradeCost());
        Debug.Log("시작 공격력 레벨: " + GameManager.Instance.player.GetBasicSTRLevel() + "입니다.");


        StartSettingUI();
    }

    void Update()
    {
        UpdateUI();





    }

    void StartSettingUI()
    {
        STRLevelTitle.text ="공격력 Lv "+ GameManager.Instance.player.GetBasicSTRLevel().ToString("N0");
        DEFLevelTitle.text = "방어력 Lv " + GameManager.Instance.player.GetBasicDEFLevel().ToString("N0");
        HPLevelTitle.text = "체력 Lv " + GameManager.Instance.player.GetBasicHPLevel().ToString("N0");
        CRTLevelTitle.text = "크리티컬 Lv " + GameManager.Instance.player.GetBasicCRTLevel().ToString("N0");

        Debug.Log("ui레벨타이틀세팅완료");

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
        ouputgold.text = CalGoldOutPut(GameManager.Instance.player.GetBasicGold());

        STRLevelTitle.text = "공격력 Lv " + GameManager.Instance.player.GetBasicSTRLevel().ToString("N0");
        DEFLevelTitle.text = "방어력 Lv " + GameManager.Instance.player.GetBasicDEFLevel().ToString("N0");
        HPLevelTitle.text = "체력 Lv " + GameManager.Instance.player.GetBasicHPLevel().ToString("N0");
        CRTLevelTitle.text = "크리티컬 Lv " + GameManager.Instance.player.GetBasicCRTLevel().ToString("N0");
        //strCoinValue.text =
        //DEFCoinValue.text =
        //HPCoinValue.text =
        //CRTCoinValue.text =

    }

    public void UpgradeSTR()
    {
        if (enhance.EnhancePlayerBsaicStat("STR", out BigInteger costCoin))
        {
            Debug.Log("강화성공");
            STRCoinValue.text = CalGoldOutPut(GameManager.Instance.player.GetNowUpgradeCost());//CalGoldOutPut(costCoin);
        }
        else
        {
            Debug.Log("강화실패");

        }
    }
    public void UpgradeDEF()
    {
        if (enhance.EnhancePlayerBsaicStat("DEF", out BigInteger costCoin))
        {
            Debug.Log("강화성공");
            DEFCoinValue.text = CalGoldOutPut(costCoin);
        }
        else
        {
            Debug.Log("강화실패");

        }
    }
    public void UpgradeHP()
    {
        if (enhance.EnhancePlayerBsaicStat("HP", out BigInteger costCoin))
        {
            Debug.Log("강화성공");
            HPCoinValue.text = CalGoldOutPut(costCoin);
        }
        else
        {
            Debug.Log("강화실패");

        }
    }
    public void UpgradeCRT()
    {
        float addCRT = 5;//추후 테이블에 따라 달라질것임
        float maxCRT = player.GetBasicCRT();
        //float calCRT = player.SetBasicCRT(5);

        float result = maxCRT + addCRT;
        if (player.GetBasicGold() >= player.GetNowUpgradeCost() && result <= 100)
        {
            if (enhance.EnhancePlayerBsaicStat("CRT", out BigInteger costCoin))
            {
                Debug.Log("강화성공");
                CRTCoinValue.text = CalGoldOutPut(costCoin);
            }
            else
            {
                Debug.Log("강화실패");

            }
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