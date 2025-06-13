using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager  Instance;
    public Weapon Weapon;

    [Header("바뀔 무기이름")]
    public TextMeshProUGUI  NameweaponText;
    public TextMeshProUGUI LvweaponText;
    public TextMeshProUGUI PowerweaponText;
    public TextMeshProUGUI critChanceText;
    
    [Header("바뀔 가방무기이름")]
    public TextMeshProUGUI  InputNameweaponText;
    public TextMeshProUGUI InputLvweaponText;
    public TextMeshProUGUI InputPowerweaponText;
    public TextMeshProUGUI InputcritChanceText;
    
    
    private void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        ReWeapon();
    }

    public void ReWeapon() //하나하나 선언 하지 않아도 이것만 사용해서 내용 바뀌게 해주는 문구
    {
        NameweaponText.text = Weapon.Nameweapon; //일반 화면
        LvweaponText.text = Weapon.Lvweapon.ToString();
        PowerweaponText.text = Weapon.Powerweapon.ToString();
        critChanceText.text = Weapon.critChance.ToString("F1")+ "%";

        InputNameweaponText.text = Weapon.InputWeapon; //가방 화면
        InputLvweaponText.text = Weapon.Inputweaponlv.ToString();
        InputPowerweaponText.text = Weapon.Inputweaponpower.ToString();
        InputcritChanceText.text = Weapon.Inputchancecrit.ToString("F1")+ "%";
    }
    
    public void Reset() //무기를 해제했을때 
    {
        NameweaponText.text = "빈손";
        LvweaponText.text = "1";
        PowerweaponText.text = "10";
        critChanceText.text = "0.0%";
    }
}
