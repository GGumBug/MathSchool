using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : BaseScene
{
    private TileSpawner tileSpawner;

    protected override void Init()
    {
        base.Init();

        tileSpawner = gameObject.GetOrAddComponent<TileSpawner>();
        tileSpawner.SpwanTile();

        Managers.UI.ShowSceneUI<UI_Game>();
    }

    public override void Clear()
    {
        
    }
}
