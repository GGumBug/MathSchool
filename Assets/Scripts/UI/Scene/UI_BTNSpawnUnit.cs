using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_BTNSpawnUnit : UI_Scene
{
    public Image UnitIcon { get; private set; }
    public TextMeshProUGUI UnitName { get; private set; }

    private Vector2 desiredSize = new Vector2( 1f, 1f );

    enum Texts
    {
        UnitName
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
        UnitName = Get<TextMeshProUGUI>((int)Texts.UnitName);
    }

    public void SetName(string name)
    {
        if (UnitName== null)
        {
            Debug.Log("NULL");
            return;
        }

        UnitName.text = name;
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
}
