using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UI_Upgrade : UI_Popup
{
    private GameObject upgradePanel;
    private GameObject upgradeButtonPanel;
    private Button button_GoToMain;
    private TextMeshProUGUI text_GearCount;

    enum GameObjects
    {
        UpgradePanel,
        UpgradeButtonPanel
    }

    enum Buttons
    {
        Button_GoToMain
    }

    enum Texts
    {
        Text_GearCount
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        upgradePanel = GetGameObject((int)GameObjects.UpgradePanel);
        upgradeButtonPanel = GetGameObject((int)GameObjects.UpgradeButtonPanel);
        button_GoToMain = GetButton((int)Buttons.Button_GoToMain);
        text_GearCount = GetTextMeshProUGUI((int)Texts.Text_GearCount);
        button_GoToMain.onClick.AddListener(GoToMain);

        MakeUpgradeButton();
        SetGearCount();
        StartAnimation();
    }

    private void MakeUpgradeButton()
    {
        for (int i = 0; i < Managers.Data.UnitStatDict.Count; i++)
        {
            int unitNumber = i;
            GameObject go = Managers.Resource.Instantiate("UI/SubItem/UI_UpgradeButton", upgradeButtonPanel.transform);
            UI_UpgradeButton upgradeButton = go.GetComponent<UI_UpgradeButton>();
            string name = Managers.Data.UnitNames[i];
            Dictionary<int, Data.Unit> unitDict = Managers.Data.UnitStatDict[name];
            int level = Managers.Game.GetUnitLevel(name);
            
            Sprite unitSprite = Managers.Resource.Load<Sprite>($"Prefabs/Images/{name}");
            upgradeButton.SetImage(unitSprite);
            upgradeButton.setGearAction += SetGearCount;
            upgradeButton.AddListenerButtonUpgrade(unitNumber, unitDict);

            if (!Managers.Game.UnLockUnit[unitNumber])
            {
                upgradeButton.SetUnLockItem(Color.black, "UnLock");
                int unlockPrice = unitDict[level].unlockPrice;
                upgradeButton.SetTextUpgradePrice(unlockPrice);
                upgradeButton.SetTextLevel();
                continue;
            }

            if (level >= unitDict.Count)
            {
                upgradeButton.AppearMaxLevel();
                continue;
            }

            upgradeButton.SetTextLock(false);
            upgradeButton.SetTextLevel($"{level.ToString()} -> {(level+1).ToString()}");
            int levelUpPrice = unitDict[level].levelUpPrice;
            upgradeButton.SetTextUpgradePrice(levelUpPrice);
        }
    }

    public void GoToMain()
    {
        float duration = 1f;
        upgradePanel.transform.DOLocalMoveY(-1080f, duration);
        StartCoroutine(IEClosePopupUI(duration));
    }

    private void StartAnimation()
    {
        upgradePanel.transform.DOLocalMoveY(0f, 1f);
    }

    private IEnumerator IEClosePopupUI(float duration)
    {
        yield return new WaitForSeconds(duration);

        ClosePopupUI();
    }

    public void SetGearCount()
    {
        PlayerController player = Managers.Game.GetPlayer();
        text_GearCount.text = $"X {player.playerStat.Gear.ToString()}";
    }
}
