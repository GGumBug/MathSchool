using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    private int     stageLength = 180;
    private float   curStageTime = 0;
    private int     enemyCount = 0;
    private int     maxEnemyCount = 10;
    private float   spawnDelay;
    private float   curSpawnDelay;

    public int      StageLevel { get; private set; } = 1;

    private void Start()
    {
        SetSpawnDelay();
        Invoke("SpawnEnemy", 3f);
    }

    private void Update()
    {
        curStageTime += Time.deltaTime;
        if (curStageTime >= stageLength)
            return;

        curSpawnDelay += Time.deltaTime;
        if (curSpawnDelay >= spawnDelay)
        {
            SpawnEnemy();
            enemyCount++;
            curSpawnDelay = 0;
        }
    }

    private void SetSpawnDelay()
    {
        spawnDelay = stageLength / (float)maxEnemyCount;
    }

    private void SpawnEnemy()
    {
        Managers.Resource.Instantiate("Kid");
    }

}
