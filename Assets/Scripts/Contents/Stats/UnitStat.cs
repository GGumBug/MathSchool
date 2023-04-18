using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStat : Stat
{
    [field:SerializeField]
    public int Level { get; protected set; }
    [field: SerializeField]
    public int Price { get; protected set; }

    private void Start()
    {
        Hp = 50;
        MaxHp = 50;
        Atk = 10;
        AtkDelay = 2f;
        Level = 1;
        Price = 10;
    }
}
