using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Number : MonoBehaviour
{
    public int number { get; private set; }
    private Tweener tween;
    SpriteRenderer spriteRenderer;

    public bool FildNumber { get; private set; }

    public void SetSpriteNumber(Sprite sprite)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
    }

    public void SetNumber(int number)
    {
        this.number = number;
    }

    public void SwitchFildNumber()
    {
        if (FildNumber)
        {
            FildNumber = false;
        }

        FildNumber = true;
    }

    public void SetTweener(Tweener tweener)
    {
        tween = tweener;
    }

    public void StopTweener()
    {
        tween.Kill();
    }

    public void ResetNumber()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.color = Color.white;
        FildNumber = false;
    }
}
