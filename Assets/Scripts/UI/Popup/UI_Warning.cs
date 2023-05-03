using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class UI_Warning : UI_Popup
{
    private TextMeshProUGUI text_Info;

    private Button button_Cancel;
    private Button button_Confirm;

    enum Texts
    {
        Text_Info
    }

    enum Buttons
    {
        Button_Cancel,
        Button_Confirm
    }

    public override void Init()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        text_Info = GetTextMeshProUGUI((int)Texts.Text_Info);
        button_Cancel = GetButton((int)Buttons.Button_Cancel);
        button_Confirm = GetButton((int)Buttons.Button_Confirm);
    }

    public void addActionCancel(UnityAction action)
    {
        button_Cancel.onClick.AddListener(action);
    }

    public void addActionConfirm(UnityAction action)
    {
        button_Confirm.onClick.AddListener(action);
    }
}
