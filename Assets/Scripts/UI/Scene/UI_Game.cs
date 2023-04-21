using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Game : UI_Scene
{
    GameObject unitPanel;

    enum GameObjects
    {
        UnitPanel,
        Button_1
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        unitPanel = GetGameObject((int)GameObjects.UnitPanel);
        Button button = GetGameObject((int)GameObjects.Button_1).GetComponent<Button>();
        button.onClick.AddListener(SpawnEnemy);
        MakeSpwanUnitButton();
    }

    private void MakeSpwanUnitButton()
    {
        for (int i = 0; i < Managers.Data.UnitStatDict.Count; i++)
        {
            GameObject go = Managers.Resource.Instantiate("UI/Scene/BTN_SpawnUnit", unitPanel.transform);
            UI_BTNSpawnUnit uI_BTNSpawnUnit = go.GetComponent<UI_BTNSpawnUnit>();
            Button spwanButton = go.GetComponent<Button>();

            int index = i;
            string name = Managers.Data.UnitNames[index];

            Sprite unitImage = Managers.Resource.Load<Sprite>($"Prefabs/Images/{name}");

            uI_BTNSpawnUnit.SetImage(unitImage);
            uI_BTNSpawnUnit.SetName(name);
            spwanButton.onClick.AddListener(() => SpawnUnit(index));
        }
    }

    public void SpawnUnit(int index)
    {
        string name = Managers.Data.UnitNames[index];
        GameObject unit = Managers.Resource.Instantiate(name);
        unit.GetComponentInChildren<UnitController>().BeforeCollocate(name);
    }

    public void SpawnEnemy()
    {
        Managers.Resource.Instantiate("Kid");
        Managers.Resource.Instantiate("Adult");
        Managers.Resource.Instantiate("Tiger");
    }
}
