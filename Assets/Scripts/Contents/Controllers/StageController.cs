using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    private int     stageLength = 120;
    private int     feverTimeLength = 30;
    private int     spawnCount = 0;
    [SerializeField]
    private int     fieldEnemyCount = 0;
    private int     maxEnemyCount;
    private int     feverEnemyCount;
    private int     totralEnemyCount;
    private float   spawnDelay;
    private float   feverSpawnDelay;
    private float   curSpawnDelay;
    private float   curfeverSpawnDelay;

    Define.GameMode gameMode = Define.GameMode.Nomal;

    public int      StageLevel { get; private set; } = 4;

    private void Start()
    {
        SetStageData();
        SetSpawnDelay();
    }

    private void Update()
    {
        switch (gameMode)
        {
            case Define.GameMode.Nomal:
                if (spawnCount >= maxEnemyCount)
                {
                    gameMode = Define.GameMode.Fever;
                    float totalEnemyCount = maxEnemyCount + feverEnemyCount;
                    UI_Game uI_Game = Managers.UI.uI_Scene as UI_Game;
                    uI_Game.StageGaugeChangeColor(Color.red);
                }

                curSpawnDelay += Time.deltaTime;
                if (curSpawnDelay >= spawnDelay)
                {
                    UpdateNomalMode();
                }
                break;
            case Define.GameMode.Fever:
                if (spawnCount > totralEnemyCount)
                {
                    gameMode = Define.GameMode.Clear;
                    if (fieldEnemyCount <= 0)
                    {
                        Debug.Log("GameClear");
                    }
                }

                curfeverSpawnDelay += Time.deltaTime;
                if (curfeverSpawnDelay >= feverSpawnDelay)
                {
                    UpdateFever();
                }
                break;
        }
    }

    private void SetStageData()
    {
        Data.Stage curStage = Managers.Data.StageDict[StageLevel];

        maxEnemyCount = curStage.maxEnemyCount;
        feverEnemyCount = curStage.feverEnemyCount;
        totralEnemyCount = maxEnemyCount + feverEnemyCount;
    }

    private void SetSpawnDelay()
    {
        spawnDelay = stageLength / (float)maxEnemyCount;
        feverSpawnDelay = feverTimeLength / (float)feverEnemyCount;
    }

    private void SpawnEnemy(string name)
    {
        Managers.Resource.Instantiate(name);
    }

    private void UpdateNomalMode()
    {
        SpawnLogic();
        curSpawnDelay = 0;
    }

    private void UpdateFever()
    {
        SpawnLogic();        
        curfeverSpawnDelay = 0;
    }

    private void SpawnLogic()
    {
        Data.Stage stage = Managers.Data.StageDict[StageLevel];

        if (spawnCount != 0 && spawnCount % 9 == 0)
        {
            SpawnEnemy(stage.eliteEnemy);
        }
        else if (spawnCount != 0 && spawnCount % 5 == 0)
        {
            SpawnEnemy(stage.highEnemy);
        }
        else
        {
            SpawnEnemy(stage.nomalEnemy);
        }

        spawnCount++;
        SetStageGauge();
        fieldEnemyCount++;
    }

    private void SetStageGauge()
    {
        UI_Game uI_Game = Managers.UI.uI_Scene as UI_Game;
        uI_Game.SetStageGauge(totralEnemyCount, spawnCount);
    }

    public void MinusFieldEnemyCount()
    {
        fieldEnemyCount--;
        Mathf.Clamp(fieldEnemyCount, 0, totralEnemyCount);
    }
}
