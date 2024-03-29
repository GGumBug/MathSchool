using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    private int startMathEnergy = 10;

    public int MathEnergy { get; protected set; }
    public int Gear { get; protected set; }

    private void Awake()
    {
        MaxHp = 3;
        MathEnergy = 0;
        //Gear = PlayerPrefs.GetInt("Gear");
        Gear = 1000;
    }

    public void SetMathEnergy(int value)
    {
        MathEnergy = Mathf.Max(0, MathEnergy + value);
    }

    public void SetStartStat()
    {
        Data.Stage curStage = Managers.Data.StageDict[Managers.Game.CurrentStageLevel];

        startMathEnergy = curStage.startMathEnergy;
        MathEnergy = startMathEnergy;
        Hp = MaxHp;
    }

    public void UseMathEnergy(int price)
    {
        MathEnergy -= price;
    }

    public void PlusMathEnerge(int value)
    {
        MathEnergy += value;
    }

    public void PlusGear(int value)
    {
        Gear += value;
        PlayerPrefs.SetInt("Gear", Gear);
    }

    public void MinusGear(int value)
    {
        Gear -= value;
        PlayerPrefs.SetInt("Gear", Gear);
    }

    public void MinusHP()
    {
        Hp--;
    }
}
