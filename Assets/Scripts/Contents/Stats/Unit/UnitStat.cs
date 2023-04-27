using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStat : Stat
{
    Dictionary<string, Dictionary<int, Data.Unit>> unitsDatas;
    Dictionary<int, Data.Unit> unitDatas;

    [field: SerializeField]
    public int Price { get; protected set; }

    protected void SetStat(string name)
    {
        unitsDatas = Managers.Data.UnitStatDict;
        unitDatas = unitsDatas[name];
        Level = Managers.Game.GetUnitLevel(name);
        Data.Unit unitStat = unitDatas[Level];

        Hp = unitStat.hp;
        MaxHp = unitStat.maxHp;
        Atk = unitStat.atk;
        AtkDelay = unitStat.atkDelay;
        Price = unitStat.price;
    }
}
