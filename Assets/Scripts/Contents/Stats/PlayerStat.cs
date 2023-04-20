using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    [field: SerializeField]
    public int MathEnergy { get; protected set; }

    private void Start()
    {
        Hp = 100;
        MaxHp = 100;
        MathEnergy = 0;
    }

    public void SetMathEnergy(int value)
    {
        MathEnergy = Mathf.Max(0, MathEnergy + value);
    }
}
