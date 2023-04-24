using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int      MathEnergy { get; private set; }
    private int     _startEnergy = 500;
    public PlayerStat playerStat;

    private void Start()
    {
        playerStat = GetComponent<PlayerStat>();
    }

    public void SetStartEnergy()
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
}
