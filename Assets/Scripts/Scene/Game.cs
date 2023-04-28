using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : BaseScene
{
    private StageController stageController;
    private QuizController quizController;
    private TileSpawner tileSpawner;
    private SlotSpawner slotSpawner;
    private UI_Game ui_Game;

    protected override void Init()
    {
        base.Init();

        stageController = gameObject.GetOrAddComponent<StageController>();
        quizController = gameObject.GetOrAddComponent<QuizController>();
        quizController.MakeQuiz();
        quizController.MakeQuestion();
        tileSpawner = gameObject.GetOrAddComponent<TileSpawner>();
        tileSpawner.SpwanTile();
        slotSpawner = gameObject.GetOrAddComponent<SlotSpawner>();
        slotSpawner.SetSlotCount(quizController.quizLength);
        slotSpawner.SpwanSlot();
        quizController.FillQuizSlot(slotSpawner.QuizSlots);

        Managers.UI.ShowSceneUI<UI_Game>();
        ui_Game = Managers.UI.uI_Scene as UI_Game;

        PlayerController player = Managers.Game.GetPlayer();
        player.GetComponent<PlayerStat>().SetStartStat();
        ui_Game.SetTextMathEnergy(player);
        ui_Game.CreateHeart();
    }

    private void Update()
    {
        quizController.UpDateSpawnNumber(stageController.GameMode);
    }

    public override void Clear()
    {
        
    }
}
