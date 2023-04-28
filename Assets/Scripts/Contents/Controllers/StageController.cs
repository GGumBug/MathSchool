using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    private int     stageLength = 120;
    private int     feverTimeLength = 30;
    private int     spawnCount = 0;
    private int     fieldEnemyCount = 0;
    private int     maxEnemyCount;
    private int     feverEnemyCount;
    private int     totralEnemyCount;
    private float   spawnDelay;
    private float   feverSpawnDelay;
    private float   curSpawnDelay;
    private float   curfeverSpawnDelay;
    private bool    isGameEnd;

    public Define.GameMode GameMode { get; private set; } = Define.GameMode.Nomal;

    public int      StageLevel { get; private set; } = 5;

    private void Start()
    {
        SetStageData();
        SetSpawnDelay();
    }

    private void Update()
    {
        switch (GameMode)
        {
            case Define.GameMode.Nomal:
                if (spawnCount >= maxEnemyCount)
                {
                    GameMode = Define.GameMode.Fever;
                    GameMessage("FEVER TIME");
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
                    GameMode = Define.GameMode.Clear;

                curfeverSpawnDelay += Time.deltaTime;
                if (curfeverSpawnDelay >= feverSpawnDelay)
                {
                    UpdateFever();
                }
                break;
            case Define.GameMode.Clear:
                if (fieldEnemyCount <= 0 && !isGameEnd)
                {
                    isGameEnd = true;
                    GameMessage("GAME CLEAR");
                    Managers.Game.NextStage();
                    StartCoroutine("IEGoToMainScene");
                }
                break;
            case Define.GameMode.Over:
                if (!isGameEnd)
                {
                    isGameEnd = true;
                    GameMessage("GAME OVER");
                    StartCoroutine("IEGoToMainScene");
                }
                break;
        }
    }

    private void SetStageData()
    {
        Data.Stage curStage = Managers.Data.StageDict[StageLevel];

        StageLevel = Managers.Game.CurrentStageLevel;
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

    public void GameOver()
    {
        GameMode = Define.GameMode.Over;
    }

    private void GameMessage(string message)
    {
        UI_GameMessage uI_GameMessage = Managers.UI.ShowPopupUI<UI_GameMessage>();
        uI_GameMessage.SetGameMessage(message);
        uI_GameMessage.StartTextAnimation();
    }

    private IEnumerator IEGoToMainScene()
    {
        yield return new WaitForSeconds(3f);

        Managers.Scene.LoadScene(Define.Scene.Main);
    }
}
