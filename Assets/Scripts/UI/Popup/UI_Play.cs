using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI_Play : UI_Popup
{
    private GameObject stagePanel;
    private GameObject stageButtonPanel;
    private Button button_GoToMain;

    enum GameObjects
    {
        StagePanel,
        StageButtonPanel
    }

    enum Buttons
    {
        Button_GoToMain
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));
        stagePanel = GetGameObject((int)GameObjects.StagePanel);
        stageButtonPanel = GetGameObject((int)GameObjects.StageButtonPanel);
        button_GoToMain = GetButton((int)Buttons.Button_GoToMain);
        button_GoToMain.onClick.AddListener(GoToMain);

        MakeStageButton();
        StartAnimation();
    }

    private void MakeStageButton()
    {
        int stageCount = Managers.Data.StageDict.Count;
        for (int i = 1; i < stageCount+1; i++)
        {
            GameObject go = Managers.Resource.Instantiate("UI/SubItem/Button_Stage", stageButtonPanel.transform);
            UI_Stage_Button stageButton = go.GetComponent<UI_Stage_Button>();
            int index = i;
            stageButton.SetStageNumber(index);

            if (i > Managers.Game.UnLockStage)
            {
                stageButton.GetComponent<Image>().color = Color.grey;
                continue;
            }

            stageButton.AddLogic(index);
        }
    }

    private void StartAnimation()
    {
        stagePanel.transform.DOLocalMoveX(720f, 2f);
    }

    public void GoToMain()
    {
        float duration = 2f;
        stagePanel.transform.DOLocalMoveX(1920f, duration);
        StartCoroutine(IEClosePopupUI(duration));
    }

    private IEnumerator IEClosePopupUI(float duration)
    {
        yield return new WaitForSeconds(duration);

        ClosePopupUI();
    }
}
