using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerStat   playerStat;

    private void Start()
    {
        playerStat = GetComponent<PlayerStat>();
    }
}
