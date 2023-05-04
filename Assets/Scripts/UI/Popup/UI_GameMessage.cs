using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UI_GameMessage : UI_Popup
{
    private TextMeshProUGUI text_Message;

    enum Texts
    {
        Text_Message
    }

    public override void Init()
    {
        base.Init();

        Bind<TextMeshProUGUI>(typeof(Texts));

        text_Message = GetTextMeshProUGUI((int)Texts.Text_Message);
    }

    public void SetGameMessage(string message)
    {
        text_Message.text = message;
    }

    public void StartTextAnimation()
    {
        StartCoroutine("IETextAnimation");
    }

    private IEnumerator IETextAnimation()
    {
        Vector3 endScale = new Vector3(1f, 1f, 1f);
        float duration = 2f;
        text_Message.transform.DOScale(endScale, duration);

        yield return new WaitForSeconds(duration*2);

        ClosePopupUI();
    }
}
