using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : BaseScene
{

    protected override void Init()
    {
        base.Init();

        Managers.UI.ShowSceneUI<UI_Main>();
    }

    public override void Clear() { }
}
