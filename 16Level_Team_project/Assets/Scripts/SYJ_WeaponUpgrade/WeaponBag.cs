using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBag : MonoBehaviour
{
    [Header("버튼")] 
    public GameObject WeaponBtn;
    public GameObject BackBtn;
    public GameObject fitBtn;
    public GameObject OutFitkBtn;
    public GameObject UpgreadeBtn;

    [Header("껏다켰다 오브젝트")] 
    public GameObject WeaponCostom;
    
    
    public void OnclickWeaponBtn() //무기변경 버튼을 눌렀을때
    {
        WeaponCostom.SetActive(true); //무기가방 창이 뜬다.
    }

    public void OnclickBackBtn() //무기변경 뒤로가기 버튼을 눌렀을때
    {
        WeaponCostom.SetActive(false); //무기가방 창이 꺼진다.
    }
}
