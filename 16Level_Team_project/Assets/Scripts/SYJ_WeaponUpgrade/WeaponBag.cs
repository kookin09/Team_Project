using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBag : MonoBehaviour
{
    [Header("버튼")] 
    public GameObject FitBtn;
    public GameObject OutFitkBtn;
    public GameObject UpgradeBtn;
    
    [Header("껏다켰다 오브젝트")] 
    public GameObject WeaponCostom;
    public GameObject Sword;
    
    
    private Dictionary<int, int> powerGrowthTable = new Dictionary<int, int>() //공격력
    {
        { 1, 25 },
        { 2, 30},
        { 3, 37 },
        { 4, 40 },
        { 5, 51 },
        { 6, 63 },
        { 7, 68 },
        { 8, 70 },
        { 9, 81 },
        { 10, 92 },
        { 11, 99 },
        { 12, 111 },
        { 13, 120 },
        { 14, 140 },
        { 15, 160 },
        { 16, 190 },
        { 17, 220 },
        { 18, 250 },
        { 19, 330 },
        { 20, 450 },

    };
    
    
    private Dictionary<int, double> critGrowthTable = new Dictionary<int,double>() //공격력
    {
        { 1, 0 },
        { 2, 1.5 },
        { 3, 2.4 },
        { 4, 3.9 },
        { 5, 4.8 },
        { 6, 7.4 },
        { 7, 9.1 },
        { 8, 14.0 },
        { 9, 15.9 },
        { 10, 22.3 },
        { 11, 30.1 },
        { 12, 39.4 },
        { 13, 44.2 },
        { 14, 49.5 },
        { 15, 55.7 },
        { 16, 60.0 },
        { 17, 63.5 },
        { 18, 66.5 },
        { 19, 69.1 },
        { 20, 72.0 },

    };
    
    public void OnclickWeaponBtn() //무기변경 버튼
    {
        WeaponCostom.SetActive(true); //무기가방 창이 뜬다.
    }

    public void OnclickBackBtn() //무기변경 뒤로가기 버튼을 눌렀을때
    {
        WeaponCostom.SetActive(false); //무기가방 창이 꺼진다.
    }

    public void OnclickOutFitBtn() //장착버튼 누를때
    {
        FitBtn.SetActive(true); //장착중 버튼이 켜지고
        OutFitkBtn.SetActive(false); //장착 버튼이 꺼진다
        
        Sword.SetActive(true);//웨폰무기사진이 뜬다
        
        WeaponManager.Instance.ReWeapon();
    }

    public void OnclickFitBtn() //장착중버튼 누를때
    {
        FitBtn.SetActive(false); //장착중 버튼이 꺼지고
        OutFitkBtn.SetActive(true); //장착 버튼이 켜진다
        
        Sword.SetActive(false);//웨폰무기사진이 꺼진다
        
        WeaponManager.Instance.Reset();
    }

    
    public void OnclickUpgradeBtn()
    { 
        if (WeaponManager.Instance.Weapon.Inputweaponlv >= WeaponManager.Instance.Weapon.MaxLvweapon) //가방의 무기레벨이 최대치보다 클때
        {
            Debug.Log("이미 최대 레벨입니다.");
            return;
        }
        
        int currentLevel = WeaponManager.Instance.Weapon.Inputweaponlv;

        
        if (critGrowthTable.TryGetValue(currentLevel, out double Inputchancecrit)) //치확
        {
            WeaponManager.Instance.Weapon.Inputchancecrit = Inputchancecrit;
        }
        
        if (critGrowthTable.TryGetValue(currentLevel, out double critChance))//가방 치확
        {
            WeaponManager.Instance.Weapon.critChance = critChance;
        }
        
        

        WeaponManager.Instance.Weapon.Lvweapon += 1; //레벨업
        WeaponManager.Instance.Weapon.Inputweaponlv += 1; //무기가방 레벨업
        Debug.Log("+1 레벨업 되었습니다.");
        
        WeaponManager.Instance.Weapon.Powerweapon = powerGrowthTable[WeaponManager.Instance.Weapon.Lvweapon]; //공격력 
        WeaponManager.Instance.Weapon.Inputweaponpower = powerGrowthTable[WeaponManager.Instance.Weapon.Inputweaponlv]; //가방공격력
        Debug.Log("공격력이 올라갔습니다.");
        
        
        
        WeaponManager.Instance.ReWeapon();
        
        
    }
}
