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
    }

    private void Update()
    {
        quizController.UpDateSpawnNumber(stageController.GameMode);
    }

    public override void Clear()
    {
        
    }
}
