using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Game : UI_Scene
{
    enum GameObjects
    {
        UnitPanel,
        Button,
        Button_1
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject unitPanel = GetGameObject((int)GameObjects.UnitPanel);
        Button button = GetGameObject((int)GameObjects.Button_1).GetComponent<Button>();
        button.onClick.AddListener(SpawnEnemy);
    }

    public void SpawnUnit()
    {
        GameObject unit = Managers.Resource.Instantiate("Unit");
        unit.GetComponentInChildren<UnitController>().BeforeCollocate();
    }

    public void SpawnEnemy()
    {
        Managers.Resource.Instantiate("Enemy");
    }
}
