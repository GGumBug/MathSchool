using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UI_UpgradeButton : UI_Base
{
    enum Buttons
    {
        Button_Upgrade
    }

    enum Images
    {
        Image_UnitImage,
        Image_UpgradeGear
    }

    enum Texts
    {
        Text_Info,
        Text_Lock,
        Text_UpgradePrice,
        Text_Level
    }

    private Button button_Upgrade;
    private Image image_UnitImage;
    private Image image_UpgradeGear;
    private TextMeshProUGUI text_Info;
    private TextMeshProUGUI text_Lock;
    private TextMeshProUGUI text_UpgradePrice;
    private TextMeshProUGUI text_Level;

    private Vector2 desiredSize = new Vector2( 2f, 2f );
    public Action setGearAction;

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));
        button_Upgrade = GetButton((int)Buttons.Button_Upgrade);
        image_UnitImage = GetImage((int)Images.Image_UnitImage);
        image_UpgradeGear = GetImage((int)Images.Image_UpgradeGear);
        text_Info = GetTextMeshProUGUI((int)Texts.Text_Info);
        text_Lock = GetTextMeshProUGUI((int)Texts.Text_Lock);
        text_UpgradePrice = GetTextMeshProUGUI((int)Texts.Text_UpgradePrice);
        text_Level = GetTextMeshProUGUI((int)Texts.Text_Level);
    }

    public void SetImage(Sprite sprite)
    {
        float pixelsPerUnit = sprite.pixelsPerUnit;

        float originalWidth = sprite.texture.width / pixelsPerUnit;
        float originalHeight = sprite.texture.height / pixelsPerUnit;
        float widthRatio = desiredSize.x / originalWidth;
        float heightRatio = desiredSize.y / originalHeight;
        float ratio = Mathf.Min(widthRatio, heightRatio);
        Vector2 size = new Vector2(originalWidth * ratio, originalHeight * ratio);

        image_UnitImage.sprite = sprite;
        image_UnitImage.transform.localScale = new Vector3(size.x, size.y, 1);
    }

    public void SetUnLockItem(Color color, string info)
    {
        image_UnitImage.color = color;
        text_Info.text = info;
    }

    public void SetTextLock(bool active, string info = null)
    {
        text_Lock.text = info;
        text_Lock.gameObject.SetActive(active);
    }

    public void SetTextUpgradePrice(int price)
    {
        text_UpgradePrice.text = $"X {price.ToString()}";
    }

    public void SetTextLevel(string level = null)
    {
        text_Level.text = level;
    }

    public void AddListenerButtonUpgrade(int unitNumber, Dictionary<int, Data.Unit> unitDict)
    {
        button_Upgrade.onClick.AddListener(() => Upgrade(unitNumber, unitDict));
    }

    private void Upgrade(int unitNumber, Dictionary<int, Data.Unit> unitDict)
    {
        PlayerController player = Managers.Game.GetPlayer();
        string name = Managers.Data.UnitNames[unitNumber];
        int unitLevel = Managers.Game.GetUnitLevel(name);

        if (!Managers.Game.UnLockUnit[unitNumber])
        {
            if (player.playerStat.Gear >= unitDict[unitLevel].unlockPrice)
            {
                Managers.Game.SwitchUnLockUnit(unitNumber);
                player.playerStat.MinusGear(unitDict[unitLevel].unlockPrice);
                setGearAction();
                SetUnLockItem(Color.white, "LevelUp");
                SetTextLock(false);
                SetTextLevel($"{unitLevel.ToString()} -> {(unitLevel+1).ToString()}");
                SetTextUpgradePrice(unitDict[unitLevel].levelUpPrice);
            }
        }
        else if (unitLevel >= unitDict.Count - 1)
        {
            if (player.playerStat.Gear >= unitDict[unitLevel].levelUpPrice)
            {
                player.playerStat.MinusGear(unitDict[unitLevel].levelUpPrice);
                setGearAction();
                Managers.Game.UpgradeUnitLevel(name);
                AppearMaxLevel();
            }
        }
        else
        {
            if (player.playerStat.Gear >= unitDict[unitLevel].levelUpPrice)
            {
                player.playerStat.MinusGear(unitDict[unitLevel].levelUpPrice);
                setGearAction();
                Managers.Game.UpgradeUnitLevel(name);
                SetTextLevel($"{(unitLevel + 1).ToString()} -> {(unitLevel + 2).ToString()}");
                SetTextUpgradePrice(unitDict[unitLevel + 1].levelUpPrice);
            }
        }
    }

    public void AppearMaxLevel()
    {
        image_UpgradeGear.gameObject.SetActive(false);
        button_Upgrade.gameObject.SetActive(false);
        text_Level.gameObject.SetActive(false);
        SetTextLock(true, "Max");
    }
}
