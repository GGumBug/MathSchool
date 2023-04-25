using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : BaseScene
{
    private TileSpawner tileSpawner;
    private UI_Game ui_Game;

    protected override void Init()
    {
        base.Init();

        tileSpawner = gameObject.GetOrAddComponent<TileSpawner>();
        tileSpawner.SpwanTile();

        Managers.UI.ShowSceneUI<UI_Game>();
        ui_Game = Managers.UI.uI_Scene as UI_Game;

        PlayerController player = Managers.Game.GetPlayer();
        player.GetComponent<PlayerStat>().SetStartStat();
        ui_Game.SetTextMathEnergy(player);
        ui_Game.CreateHeart();
    }

    public override void Clear()
    {
        
    }
}
