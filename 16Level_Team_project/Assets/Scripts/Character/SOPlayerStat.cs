using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;


[CreateAssetMenu(fileName = "ScriptableObject",menuName ="ScriptableObject/Player/BasicStat",order = 1)]
public class SOPlayerStat : ScriptableObject
{
    [SerializeField]
    string[] levelCharTable = { "maxLevelRange100", "maxLevelRange200", "maxLevelRange300", "maxLevelRange400", "maxLevelRange500",
            "maxLevelRange600", "maxLevelRange700", "maxLevelRange800", "maxLevelRange900", "maxLevelRange1_000" };

    [SerializeField]
    Dictionary<string, int> levelCostTable = new Dictionary<string, int>()
        {
            {"maxLevelRange100",500 },
            {"maxLevelRange200",1_000 },
            {"maxLevelRange300",3_000 },
            {"maxLevelRange400",5_000 },
            {"maxLevelRange500",8_000 },
            {"maxLevelRange600",10_000 },
            {"maxLevelRange700",30_000 },
            {"maxLevelRange800",50_000 },
            {"maxLevelRange900",80_000 },
            {"maxLevelRange1_000",100_000 },
        };

}
