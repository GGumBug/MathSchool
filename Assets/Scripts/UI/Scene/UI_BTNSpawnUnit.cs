using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BTNSpawnUnit : UI_Scene
{
    private Button  btn_SpawnUnit;
    private Image   unitIcon;

    enum Texts
    {
        UnitName
    }

    enum Buttons
    {
        BTN_SpawnUnit
    }

    enum Images
    {
        UnitIcon
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        unitIcon = Get<Image>((int)Images.UnitIcon);
        // 유닛 데이터와 연동작업후에 다시 작업 시작
    }
}
