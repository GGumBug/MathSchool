using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : Stat
{
    Dictionary<int, Data.Enemy> enemyStats;

    [field: SerializeField]
    public float MoveSpeed { get; protected set; }

    protected void SetStat()
    {
        enemyStats = Managers.Data.EnemyStatDict;
        Data.Enemy enemyStat = enemyStats[Level];

        Name = enemyStat.name;
        Hp = enemyStat.hp;
        MaxHp = enemyStat.maxHp;
        Atk = enemyStat.atk;
        AtkDelay = enemyStat.atkDelay;
        MoveSpeed = enemyStat.moveSpeed;
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
