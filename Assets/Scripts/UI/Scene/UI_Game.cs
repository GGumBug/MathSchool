using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Game : UI_Scene
{
    GameObject unitPanel;
    GameObject heartPanel;
    TextMeshProUGUI txtMathEnergy;
    Slider stageGauge;
    Button button_Pause;


    public Stack<GameObject> Hearts { get; private set; } = new Stack<GameObject>();

    enum GameObjects
    {
        UnitPanel,
        HeartPanel,
        Slider_StageGauge
    }

    enum Texts
    {
        Text_MathEnegy
    }

    enum Buttons
    {
        Button_Pause
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Button>(typeof(Buttons));


        unitPanel = GetGameObject((int)GameObjects.UnitPanel);
        heartPanel = GetGameObject((int)GameObjects.HeartPanel);

        txtMathEnergy = GetTextMeshProUGUI((int)Texts.Text_MathEnegy);

        stageGauge = GetGameObject((int)GameObjects.Slider_StageGauge).GetComponent<Slider>();
        MakeSpwanUnitButton();

        button_Pause = GetButton((int)Buttons.Button_Pause);
        button_Pause.onClick.AddListener(GamePause);
    }

    public void CreateHeart()
    {
        PlayerStat player = Managers.Game.GetPlayer().gameObject.GetComponent<PlayerStat>();
        int heartCount = player.Hp;

        for (int i = 0; i < heartCount; i++)
        {
            GameObject go = Managers.Resource.Instantiate("UI/SubItem/Image_Heart", heartPanel.transform);
            Hearts.Push(go);
        }
    }

    private void MakeSpwanUnitButton()
    {
        for (int i = 0; i < Managers.Data.UnitStatDict.Count; i++)
        {
            if (!Managers.Game.UnLockUnit[i])
                continue;

            GameObject go = Managers.Resource.Instantiate("UI/Scene/BTN_SpawnUnit", unitPanel.transform);
            UI_BTNSpawnUnit uI_BTNSpawnUnit = go.GetComponent<UI_BTNSpawnUnit>();
            Button spwanButton = go.GetComponent<Button>();

            int index = i;
            string name = Managers.Data.UnitNames[index];
            int level = Managers.Game.GetUnitLevel(name);
            Dictionary<int, Data.Unit> targetUnitDict = Managers.Data.UnitStatDict[name];
            int price = targetUnitDict[level].price;


            Sprite unitImage = Managers.Resource.Load<Sprite>($"Prefabs/Images/{name}");

            uI_BTNSpawnUnit.SetImage(unitImage);
            uI_BTNSpawnUnit.SetName(name);
            uI_BTNSpawnUnit.SetPrice(price);
            spwanButton.onClick.AddListener(() => SpawnUnit(index));
        }
    }

    public void SpawnUnit(int index)
    {
        PlayerController player = Managers.Game.GetPlayer();
        string name = Managers.Data.UnitNames[index];
        int level = Managers.Game.GetUnitLevel(name);
        Dictionary<int, Data.Unit> targetUnitDict = Managers.Data.UnitStatDict[name];
        int price = targetUnitDict[level].price;

        if (player.GetComponent<PlayerStat>().MathEnergy < price)
        {
            Debug.Log("수학 에너지가 부족합니다.");
            return;
        }
        else
        {
            GameObject unit = Managers.Resource.Instantiate(name);
            unit.GetComponentInChildren<UnitController>().BeforeCollocate(name, price);
        }
    }

    public void SetTextMathEnergy(PlayerController player)
    {
        txtMathEnergy.text = player.GetComponent<PlayerStat>().MathEnergy.ToString();
    }

    public void MinusHeart()
    {
        GameObject go = Hearts.Pop();
        Managers.Resource.Destroy(go);

        PlayerStat player = Managers.Game.GetPlayer().gameObject.GetComponent<PlayerStat>();
        player.MinusHP();

        if (player.Hp <= 0)
            Managers.Scene.CurrentScene.GetComponent<StageController>().GameOver();
    }

    public void SetStageGauge(float maxValue, float value)
    {
        stageGauge.maxValue = maxValue;
        stageGauge.value = value;
    }

    public void StageGaugeChangeColor(Color color)
    {
        Image fill = stageGauge.fillRect.GetComponent<Image>();
        fill.color = color;
    }

    public void GamePause()
    {
        Managers.UI.ShowPopupUI<UI_Pause>();
        Time.timeScale = 0f;
    }
}
