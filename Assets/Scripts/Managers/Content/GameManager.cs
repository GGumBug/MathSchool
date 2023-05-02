using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private PlayerController player = null;
    private Dictionary<string, int> unitLevels = new Dictionary<string, int>();

    public int          UnLockStage { get; private set; } = 1;
    public int          CurrentStageLevel { get; private set; } = 1;
    public List<bool>   UnLockUnit { get; private set; } = new List<bool>();
    public bool         IsUnitCollocating { get; protected set; } = false;

    public void Init()
    {
        CreatePlayer();
        LoadSaveData();
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

    private void LoadSaveData()
    {
        LoadUnLockUnit();
        LoadUnitLevel();
        UnLockStageLevel();
    }

    private void LoadUnLockUnit()
    {
        UnLockUnit.Add(true);

        for (int i = 1; i < Managers.Data.UnitStatDict.Count; i++)
        {
            if (PlayerPrefs.GetInt($"Unit_{i}") == 0)
            {
                UnLockUnit.Add(false);
            }
            else
            {
                UnLockUnit.Add(true);
            }
        }
    }

    private void LoadUnitLevel()
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

    private void UnLockStageLevel()
    {
        UnLockStage = PlayerPrefs.GetInt("UnLockStage");
        if (UnLockStage == 0)
            UnLockStage = 1;
    }

    public void SwitchUnLockUnit(int number)
    {
        PlayerPrefs.SetInt($"Unit_{number}", 1);
        UnLockUnit[number] = true;
    }

    public int GetUnitLevel(string name)
    {
        return unitLevels[name];
    }

    public void UpgradeUnitLevel(string name)
    {
        int level = unitLevels[name];
        level++;
        PlayerPrefs.SetInt(name, level);
        unitLevels[name] = level;
    }

    public void NextStage(int stageLevel)
    {
        if (stageLevel == UnLockStage)
            UnLockStage++;

        PlayerPrefs.SetInt("UnLockStage", UnLockStage);
    }

    public void SetCurrentStageLevel(int index)
    {
        CurrentStageLevel = index;
    }

    public void SwitchUnitCollocating()
    {
        if (IsUnitCollocating == true)
        {
            IsUnitCollocating = false;
            return;
        }

        IsUnitCollocating = true;
    }
}
