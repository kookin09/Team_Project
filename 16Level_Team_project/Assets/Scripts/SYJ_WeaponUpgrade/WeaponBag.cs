using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBag : MonoBehaviour
{
    [Header("버튼")] 
    public GameObject FitBtn;
    public GameObject OutFitkBtn;

    [Header("껏다켰다 오브젝트")] 
    public GameObject WeaponCostom;
    public GameObject Sword;
    
    
    public void OnclickWeaponBtn() //무기변경 버튼
    {
        WeaponCostom.SetActive(true); //무기가방 창이 뜬다.
    }

    public void OnclickBackBtn() //무기변경 뒤로가기 버튼을 눌렀을때
    {
        WeaponCostom.SetActive(false); //무기가방 창이 꺼진다.
    }

    public void OnclickOutFitBtn() //장착버튼
    {
        FitBtn.SetActive(true); //장착중 버튼이 켜지고
        OutFitkBtn.SetActive(false); //장착 버튼이 꺼진다
        
        Sword.SetActive(true);//웨폰무기사진이 뜬다
        //능력치 숫자 보임
    }

    public void OnclickFitBtn() //장착중버튼
    {
        FitBtn.SetActive(false); //장착중 버튼이 꺼지고
        OutFitkBtn.SetActive(true); //장착 버튼이 켜진다
        
        Sword.SetActive(false);//웨폰무기사진이 꺼진다
        //능력치 숫자 0으로 
    }
}
