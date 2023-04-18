using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Game : UI_Scene
{
    enum GameObjects
    {
        UnitPanel
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject unitPanel = GetGameObject((int)GameObjects.UnitPanel);
    }

    //임시 코드 나중에 수정
    public void SpawnUnit()
    {
        GameObject unit = Managers.Resource.Instantiate("Unit");
        unit.GetComponentInChildren<UnitController>().BeforeCollocate();
    }
}
