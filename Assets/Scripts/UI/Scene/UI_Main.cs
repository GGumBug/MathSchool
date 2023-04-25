using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Main : UI_Scene
{
    private GameObject  button_Panel;
    private Button      button_Play;

    enum GameObjects
    {
        Button_Panel
    }

    enum Buttons
    {
        Button_Play,
        Button_Upgrade,
        Button_Option
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));

        button_Panel = GetGameObject((int)GameObjects.Button_Panel);
        button_Play = GetButton((int)Buttons.Button_Play);
        button_Play.onClick.AddListener(OpenUIPlay);
    }

    public void OpenUIPlay()
    {
        Managers.UI.ShowPopupUI<UI_Play>();
        Managers.UI.SwitchIsUIPlayOpen();
    }
}
