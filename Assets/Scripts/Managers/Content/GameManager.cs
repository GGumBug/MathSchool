using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private PlayerController player = null;
    private Dictionary<string, int> unitLevels = new Dictionary<string, int>();

    public int UnLockStage { get; private set; } = 1;
    public int CurrentStageLevel { get; private set; } = 1;

    public void Init()
    {
        CreatePlayer();
        SetUnitLevel();
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

    private void SetUnitLevel()
    {
        for (int i = 0; i < Managers.Data.UnitStatDict.Count; i++)
        {
            string name = Managers.Data.UnitNames[i];
            int level = PlayerPrefs.GetInt(name);
            if (level == 0)
                level = 1;
            unitLevels.Add(name, level);
        }
    }

    public int GetUnitLevel(string name)
    {
        return unitLevels[name];
    }

    public void NextStage()
    {
        UnLockStage++;
    }

    public void SetCurrentStageLevel(int index)
    {
        CurrentStageLevel = index;
    }
}
