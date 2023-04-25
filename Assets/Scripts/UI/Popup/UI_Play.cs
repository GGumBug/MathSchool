using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI_Play : UI_Popup
{
    private GameObject stagePanel;
    private GameObject stageButtonPanel;

    enum GameObjects
    {
        StagePanel,
        StageButtonPanel
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        stagePanel = GetGameObject((int)GameObjects.StagePanel);
        stageButtonPanel = GetGameObject((int)GameObjects.StageButtonPanel);

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
}
