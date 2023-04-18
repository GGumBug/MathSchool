using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : Stat
{
    [field: SerializeField]
    public float MoveSpeed { get; protected set; }
}
