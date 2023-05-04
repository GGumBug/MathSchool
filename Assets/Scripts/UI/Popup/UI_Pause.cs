using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Pause : UI_Popup
{
    Button button_Resume;
    Button button_Exit;

    enum Buttons
    {
        Button_Resume,
        Button_Exit
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        button_Resume = GetButton((int)Buttons.Button_Resume);
        button_Exit = GetButton((int)Buttons.Button_Exit);

        button_Resume.onClick.AddListener(Resume);
        button_Exit.onClick.AddListener(Exit);
    }

    public void Resume()
    {
        Managers.UI.ClosePopupUI();
        Time.timeScale = 1f;
    }

    public void Exit()
    {
        UI_Warning uI_Warning = Managers.UI.ShowPopupUI<UI_Warning>();
        uI_Warning.addActionCancel(Cancel);
        uI_Warning.addActionConfirm(GoToMain);
    }

    public void Cancel()
    {
        Managers.UI.ClosePopupUI();
    }

    public void GoToMain()
    {
        Time.timeScale = 1f;
        Managers.Scene.LoadScene(Define.Scene.Main);
    }
}
