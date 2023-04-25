using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    [field: SerializeField]
    public int MathEnergy { get; protected set; }
    private int _startEnergy = 500;

    private void Awake()
    {
        Hp = 3;
        MaxHp = 3;
        MathEnergy = 0;
    }

    public void SetMathEnergy(int value)
    {
        MathEnergy = Mathf.Max(0, MathEnergy + value);
    }

    public void SetStartStat()
    {
        MathEnergy = _startEnergy;

    }

    public void UseMathEnergy(int price)
    {
        MathEnergy -= price;
    }

    public void PlusMathEnerge(int value)
    {
        MathEnergy += value;
    }

    public void MinusHP()
    {
        Hp--;
    }
}
