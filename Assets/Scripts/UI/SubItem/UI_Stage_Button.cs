using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Stage_Button : UI_Base
{
    TextMeshProUGUI text_StageNumber;
    Button          button_Stage;

    enum Texts
    {
        Text_StageNumber
    }

    public override void Init()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
        text_StageNumber = GetTextMeshProUGUI((int)Texts.Text_StageNumber);
        button_Stage = GetComponent<Button>();
    }

    public void SetStageNumber(int index)
    {
        text_StageNumber.text = index.ToString();
    }

    public void AddLogic(int stageNumber)
    {
        button_Stage.onClick.AddListener(() => GoToGameScene(stageNumber));
    }

    private void GoToGameScene(int stageNumber)
    {
        Managers.Game.SetCurrentStageLevel(stageNumber);
        Managers.Scene.LoadScene(Define.Scene.Game);
    }
}
