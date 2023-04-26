using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_UpgradeButton : UI_Base
{
    enum Images
    {
        Image_UnitImage
    }

    private Image image_UnitImage;
    private Vector2 desiredSize = new Vector2( 2f, 2f );

    public override void Init()
    {
        Bind<Image>(typeof(Images));
        image_UnitImage = GetImage((int)Images.Image_UnitImage);
    }

    public void SetImage(Sprite sprite)
    {
        float pixelsPerUnit = sprite.pixelsPerUnit;

        float originalWidth = sprite.texture.width / pixelsPerUnit;
        float originalHeight = sprite.texture.height / pixelsPerUnit;
        float widthRatio = desiredSize.x / originalWidth;
        float heightRatio = desiredSize.y / originalHeight;
        float ratio = Mathf.Min(widthRatio, heightRatio);
        Vector2 size = new Vector2(originalWidth * ratio, originalHeight * ratio);

        image_UnitImage.sprite = sprite;
        image_UnitImage.transform.localScale = new Vector3(size.x, size.y, 1);
    }
}
