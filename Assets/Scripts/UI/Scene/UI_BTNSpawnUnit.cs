using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_BTNSpawnUnit : UI_Scene
{
    public Image UnitIcon { get; private set; }
    public TextMeshProUGUI txt_UnitName { get; private set; }
    public TextMeshProUGUI txt_UnitPrice { get; private set; }

    private Vector2 desiredSize = new Vector2( 0.8f, 0.8f );

    enum Texts
    {
        Text_UnitName,
        Text_UnitPrice
    }

    enum Images
    {
        UnitIcon
    }

    public override void Init()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));

        UnitIcon = Get<Image>((int)Images.UnitIcon);
        txt_UnitName = GetTextMeshProUGUI((int)Texts.Text_UnitName);
        txt_UnitPrice = GetTextMeshProUGUI((int)Texts.Text_UnitPrice);
    }

    public void SetName(string name)
    {
        if (txt_UnitName == null)
        {
            Debug.Log("NULL");
            return;
        }

        txt_UnitName.text = name;
    }

    // Sprite 종횡비를 유지하며 불러온다.
    public void SetImage(Sprite sprite)
    {
        float pixelsPerUnit = sprite.pixelsPerUnit;

        float originalWidth = sprite.texture.width / pixelsPerUnit;
        float originalHeight = sprite.texture.height / pixelsPerUnit;
        float widthRatio = desiredSize.x / originalWidth;
        float heightRatio = desiredSize.y / originalHeight;
        float ratio = Mathf.Min(widthRatio, heightRatio);
        Vector2 size = new Vector2(originalWidth * ratio, originalHeight * ratio);

        UnitIcon.sprite = sprite;
        UnitIcon.transform.localScale = new Vector3(size.x, size.y, 1);
    }

    public void SetPrice(int price)
    {
        txt_UnitPrice.text = price.ToString();
    }
}
