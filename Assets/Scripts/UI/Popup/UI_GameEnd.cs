using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UI_GameEnd : UI_Popup
{
    private Button button_Retry;
    private Button button_GoToMain;
    private TextMeshProUGUI text_GameEndTitle;
    private TextMeshProUGUI text_GearCount;

    private CanvasGroup canvasGroup;

    enum Buttons
    {
        Button_Retry,
        Button_GoToMain
    }

    enum Texts
    {
        Text_GameEndTitle,
        Text_GearCount
    }

    public override void Init()
    {
        canvasGroup = GetComponentInChildren<CanvasGroup>();
        canvasGroup.alpha = 0f;

        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        text_GameEndTitle = GetTextMeshProUGUI((int)Texts.Text_GameEndTitle);
        text_GearCount = GetTextMeshProUGUI((int)Texts.Text_GearCount);
        button_GoToMain = GetButton((int)Buttons.Button_GoToMain);
        button_Retry = GetButton((int)Buttons.Button_Retry);

        button_GoToMain.onClick.AddListener(GoToMainScene);
        button_Retry.onClick.AddListener(Retry);

        FadeIn();
    }

    public void ChangeGameEndTitle(Define.GameMode gameMode)
    {
        if (gameMode == Define.GameMode.Clear)
        {
            text_GameEndTitle.text = "GAME CLEAR";
            text_GameEndTitle.color = Color.green;
        }
        else
        {
            text_GameEndTitle.text = "GAME OVER";
            text_GameEndTitle.color = Color.red;
        }
    }

    public void SetGearCount(int gearCount)
    {
        text_GearCount.text = $"X {gearCount.ToString()}";
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        Managers.Scene.LoadScene(Define.Scene.Game);
    }

    public void GoToMainScene()
    {
        Time.timeScale = 1f;
        Managers.Scene.LoadScene(Define.Scene.Main);
    }

    public void FadeIn()
    {
        canvasGroup.DOFade(1f, 2f).OnComplete(OnTweenComplete);
    }

    private void OnTweenComplete()
    {
        Time.timeScale = 0f;
    }
}
