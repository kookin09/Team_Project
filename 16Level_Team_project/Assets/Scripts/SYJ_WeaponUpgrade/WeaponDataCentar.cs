using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "SsriptableData/WeaponData", order = 1)]
public class WeaponDataCentar : ScriptableObject
{
    [SerializeField]
    private string weaponname;
    public string WeaponName { get { return weaponname; } }
    
    [SerializeField]
    private float weaponlv;
    public float WeaponLv { get { return weaponlv; } }

    [SerializeField]
    private float weaponpower;
    public float WeaponPower { get { return weaponpower; } }

    [SerializeField]
    private float chancecrit;
    public float Chancecrit { get { return chancecrit; } }
}

