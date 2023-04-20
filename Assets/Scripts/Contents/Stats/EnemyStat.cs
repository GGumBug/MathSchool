using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : Stat
{
    [field: SerializeField]
    public float MoveSpeed { get; protected set; }

    private void Start()
    {
        Name = "Kid";
        Hp = 50;
        MaxHp = 50;
        Atk = 10;
        AtkDelay = 2f;
        MoveSpeed = 5f;
    }

    public void ResetStat()
    {
        Hp = 50;
        MaxHp = 50;
        Atk = 10;
        AtkDelay = 2f;
        MoveSpeed = 5f;
    }
}
