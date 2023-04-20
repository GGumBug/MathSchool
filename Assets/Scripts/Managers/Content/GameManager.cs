using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    PlayerController player = null;

    public void Init()
    {
        CreatePlayer();
    }

    public void CreatePlayer()
    {
        if (player == null)
        {
            GameObject go = Managers.Resource.Instantiate("Player");
            player = go.GetComponent<PlayerController>();
            Object.DontDestroyOnLoad(go);
        }
    }

    public PlayerController GetPlayer()
    {
        if (player == null)
            Debug.Log("플레이어가 존재하지 않습니다.");

        return player;
    }
}
