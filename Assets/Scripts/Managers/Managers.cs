using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    #region SingleTone
    private static Managers s_Instance = null;
    private static Managers Instance { get { Init(); return s_Instance; } }
    #endregion

    #region Content
    private GameManager             _game = new GameManager();

    public static GameManager       Game { get { return Instance._game; } }
    #endregion

    #region Core
    private ResourceManager         _resource = new ResourceManager();
    private UIManager               _ui = new UIManager();
    private PoolManager             _pool = new PoolManager();
    private ScenesManager           _scene = new ScenesManager();
    private DataManager             _data = new DataManager();

    public static ResourceManager   Resource { get { return Instance._resource; } }
    public static UIManager         UI { get { return Instance._ui; } }
    public static PoolManager       Pool { get { return Instance._pool; } }
    public static ScenesManager     Scene { get { return Instance._scene; } }
    public static DataManager      Data { get { return Instance._data; } }
    #endregion

    private void Start()
    {
        Init();
    }

    private static void Init()
    {
        if (s_Instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            s_Instance = go.GetComponent<Managers>();

            s_Instance._pool.Init();
            s_Instance._data.Init();
            s_Instance._game.Init();
        }
    }

    public static void Clear()
    {
        UI.Clear();
        Pool.Clear();
        Scene.Clear();
    }
}
