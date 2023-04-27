using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Number : MonoBehaviour
{
    private int number;

    SpriteRenderer spriteRenderer;

    public void SetSpriteNumber(Sprite sprite)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
    }

    public void SetNumber(int number)
    {
        this.number = number;
    }
}
