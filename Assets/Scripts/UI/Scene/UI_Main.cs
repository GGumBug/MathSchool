using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Main : UI_Scene
{
    private Button      button_Play;
    private Button      button_Upgrade;
    private Button      button_GameExit;

    enum Buttons
    {
        Button_Play,
        Button_Upgrade,
        Button_Option,
        Button_GameExit
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));

        button_Play = GetButton((int)Buttons.Button_Play);
        button_Play.onClick.AddListener(OpenUIPlay);
        button_Upgrade = GetButton((int)Buttons.Button_Upgrade);
        button_Upgrade.onClick.AddListener(OpenUIUpgrade);
        button_GameExit = GetButton((int)Buttons.Button_GameExit);
        button_GameExit.onClick.AddListener(GameExit);
    }

    public void OpenUIPlay()
    {
        Managers.UI.ShowPopupUI<UI_Play>();
    }

    public void OpenUIUpgrade()
    {
        Managers.UI.ShowPopupUI<UI_Upgrade>();
    }

    public void GameExit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
