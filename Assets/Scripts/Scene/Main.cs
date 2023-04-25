using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : BaseScene
{

    protected override void Init()
    {
        base.Init();

        Managers.UI.ShowSceneUI<UI_Main>();
        if (Managers.UI.IsUIPlayOpen)
            Managers.UI.ShowPopupUI<UI_Play>();
    }

    public override void Clear() { }
}
