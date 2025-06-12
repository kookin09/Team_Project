using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using TMPro;


public class UICharacter : MonoBehaviour
{
    [SerializeField]
    Character character;
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

    }

    void Update()
    {
        UpdateUI();

        BigInteger result;
        if (BigInteger.TryParse(outputStringGold, out result))
        {
            moneyGold.text = CalGoldOutPut(result);
        }
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
        curSTR.text = $"Now STR :{character.GetBasicSTR()}";
        //골드
        ouputgold.text = CalGoldOutPut(character.GetbasicGold());

    }


    public void UpgradeSTR()
    {
        character.SetBasicGold(character.GetUpgradeCost());
        character.SetBasicSTR(500);
    }
    public void UpgradeDEF()
    {
        character.SetBasicGold(character.GetUpgradeCost());
        character.SetBasicSTR(500);
    }
    public void UpgradeHP()
    {
        character.SetBasicGold(character.GetUpgradeCost());
        character.SetBasicSTR(500);
    }
    public void UpgradeCRT()
    {
        character.SetBasicGold(character.GetUpgradeCost());
        character.SetBasicSTR(500);
    }

    public void CheatGold()
    {

        character.CheatGoldMethod(999999999);
    }
    public void CheatGold2()
    {

        character.CheatGoldMethod(999999999999);
    }
}
