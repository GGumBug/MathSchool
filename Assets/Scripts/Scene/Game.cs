using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : BaseScene
{
    private StageController stageController;
    private TileSpawner     tileSpawner;
    private UI_Game         ui_Game;

    public SlotSpawner slotSpawner { get; private set; }
    public QuizController quizController { get; private set; }
    public Vector3 MainMathEnergyPos {get; private set;}

    protected override void Init()
    {
        base.Init();

        stageController = gameObject.GetOrAddComponent<StageController>();
        quizController = gameObject.GetOrAddComponent<QuizController>();
        tileSpawner = gameObject.GetOrAddComponent<TileSpawner>();
        slotSpawner = gameObject.GetOrAddComponent<SlotSpawner>();
        quizController.MakeQuiz();
        quizController.MakeQuestion();
        tileSpawner.SpwanTile();
        slotSpawner.SetSlotCount(quizController.quizLength);
        slotSpawner.SpwanSlot();
        quizController.FillQuizSlot(slotSpawner.QuizSlots);

        Managers.UI.ShowSceneUI<UI_Game>();
        ui_Game = Managers.UI.uI_Scene as UI_Game;

        PlayerController player = Managers.Game.GetPlayer();
        player.GetComponent<PlayerStat>().SetStartStat();
        ui_Game.SetTextMathEnergy(player);
        ui_Game.CreateHeart();

        GetMainMathEnergeObject();
    }

    private void Update()
    {
        quizController.UpDateSpawnNumber(stageController.GameMode);
    }

    private void GetMainMathEnergeObject()
    {
        GameObject mainMathEnergy = GameObject.FindGameObjectWithTag("MainMathEnergy");
        if (mainMathEnergy != null)
            MainMathEnergyPos = mainMathEnergy.transform.position;
        else
            Debug.Log("MainMathEnergy가 존재하지 않습니다.");
    }

    public override void Clear()
    {
        
    }
}
