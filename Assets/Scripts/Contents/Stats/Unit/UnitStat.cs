using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStat : Stat
{
    Dictionary<string, Dictionary<int, Data.Unit>> unitsDatas;
    Dictionary<int, Data.Unit> unitDatas;

    [field: SerializeField]
    public int Price { get; protected set; }

    public void LevelUp()
    {
        int level = Level;

        Data.Unit unitStat;
        if (!unitDatas.TryGetValue(level + 1, out unitStat))
        {
            Debug.Log("레벨이 한계치 입니다!");
            return;
        }

        Level += 1;
        SetStat();
    }

    protected void SetStat()
    {
        unitsDatas = Managers.Data.UnitStatDict;
        unitDatas = unitsDatas[Name];
        Data.Unit unitStat = unitDatas[Level];

        Hp = unitStat.hp;
        MaxHp = unitStat.maxHp;
        Atk = unitStat.atk;
        AtkDelay = unitStat.atkDelay;
        Price = unitStat.price;
    }
}
