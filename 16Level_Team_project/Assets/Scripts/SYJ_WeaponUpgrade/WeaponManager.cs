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
        NameweaponText.text = Weapon.Nameweapon;
        LvweaponText.text = Weapon.Lvweapon.ToString();
        PowerweaponText.text = Weapon.Powerweapon.ToString();
        critChanceText.text = Weapon.critChance.ToString();
    }
    
}
