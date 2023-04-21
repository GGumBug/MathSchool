using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_BTNSpawnUnit : UI_Scene
{
    public Image UnitIcon { get; private set; }
    public TextMeshProUGUI UnitName { get; private set; }

    enum Texts
    {
        UnitName
    }

    enum Images
    {
        UnitIcon
    }

    public override void Init()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));

        UnitIcon = Get<Image>((int)Images.UnitIcon);
        UnitName = Get<TextMeshProUGUI>((int)Texts.UnitName);
        // 유닛 데이터와 연동작업후에 다시 작업 시작
    }

    public void SetName(string name)
    {
        if (UnitName== null)
        {
            Debug.Log("NULL");
            return;
        }

        UnitName.text = name;
    }

    public void SetImage(Sprite sprite)
    {
        UnitIcon.sprite = sprite;
    }
}
