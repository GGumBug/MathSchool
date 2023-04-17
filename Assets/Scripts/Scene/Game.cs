using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private TileSpawner tileSpawner;

    private void Awake()
    {
        tileSpawner = gameObject.GetOrAddComponent<TileSpawner>();
        tileSpawner.SpwanTile();

        Managers.UI.ShowSceneUI<UI_Game>();
    }
}
