using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int mathEnergy { get; private set; }
    public PlayerStat playerStat;

    private void Start()
    {
        playerStat = GetComponent<PlayerStat>();
    }
}
