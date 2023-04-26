using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI_Upgrade : UI_Popup
{
    private GameObject upgradePanel;
    private GameObject upgradeButtonPanel;
    private Button button_GoToMain;

    enum GameObjects
    {
        UpgradePanel,
        UpgradeButtonPanel
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
        upgradePanel = GetGameObject((int)GameObjects.UpgradePanel);
        upgradeButtonPanel = GetGameObject((int)GameObjects.UpgradeButtonPanel);
        button_GoToMain = GetButton((int)Buttons.Button_GoToMain);
        button_GoToMain.onClick.AddListener(GoToMain);

        MakeUpgradeButton();
        StartAnimation();
    }

    private void MakeUpgradeButton()
    {
        for (int i = 0; i < Managers.Data.UnitStatDict.Count; i++)
        {
            GameObject go = Managers.Resource.Instantiate("UI/SubItem/UI_UpgradeButton", upgradeButtonPanel.transform);
            UI_UpgradeButton upgradeButton = go.GetComponent<UI_UpgradeButton>();
            string name = Managers.Data.UnitNames[i];
            Sprite unitSprite = Managers.Resource.Load<Sprite>($"Prefabs/Images/{name}");
            upgradeButton.SetImage(unitSprite);

        }
    }

    public void GoToMain()
    {
        float duration = 2f;
        upgradePanel.transform.DOLocalMoveY(-1080f, duration);
        StartCoroutine(IEClosePopupUI(duration));
    }

    private void StartAnimation()
    {
        upgradePanel.transform.DOLocalMoveY(0f, 2f);
    }

    private IEnumerator IEClosePopupUI(float duration)
    {
        yield return new WaitForSeconds(duration);

        ClosePopupUI();
    }
}
