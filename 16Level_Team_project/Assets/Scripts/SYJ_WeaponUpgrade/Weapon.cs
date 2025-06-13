using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    [Header("무기 정보")] 
    public string Nameweapon; //무기 이름
    public int Lvweapon; //무기 레벨
    public int Powerweapon; //무기 공격력
    public float critChance; //무기 치명타확률

    public Weapon(string Nameweapon, int Lvweapon, int Powerweapon, float critChance)
    {
        this.Nameweapon = Nameweapon;
        this.Lvweapon = Lvweapon;
        this.Powerweapon = Powerweapon;
        this.critChance = critChance;
    }
}
