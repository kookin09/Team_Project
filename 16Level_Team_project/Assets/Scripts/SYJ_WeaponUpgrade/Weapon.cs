using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    [Header("무기정보")] 
    public string Nameweapon; //무기 이름
    public int Lvweapon; //무기 레벨
    public float Powerweapon; //무기 공격력
    public float critChance; //무기 치명타확률

    [Header("무기가방 데이터")] 
    public string InputWeapon; //가방 무기이름
    public int Inputweaponlv; //가방 무기 레벨
    public float Inputweaponpower; //가방 무기 공격력
    public float Inputchancecrit; //가방 무기 치확
    public Weapon(string Nameweapon, int Lvweapon, float Powerweapon, float critChance , 
        string InputWeapon, int Inputweaponlv, float Inputweaponpower, float Inputchancecrit)
    {
        this.Nameweapon = Nameweapon; 
        this.Lvweapon = Lvweapon;
        this.Powerweapon = Powerweapon;
        this.critChance = critChance;

        this.InputWeapon = InputWeapon; //가방에 있는 데이터
        this.Inputweaponlv = Inputweaponlv;
        this.Inputweaponpower = Inputweaponpower;
        this.Inputchancecrit = Inputchancecrit;
    }

    
}
