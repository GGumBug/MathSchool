using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private SpriteRenderer[] spriteRenderers;
    private SpriteRenderer qustionMark;

    public int AnswerNumber { get; private set; }
    public bool IsEmpty { get; private set; }

    public bool SetIsEmpty()
    {
        if (IsEmpty)
            return IsEmpty = false;

        return IsEmpty = true;
    }

    public void SetQustionMarkColor(Color color)
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        qustionMark = spriteRenderers[1];
        qustionMark.color = color;
    }

    public void SetAnswerNumber(int value)
    {
        AnswerNumber = value;
    }

    public void ResetSlot()
    {
        IsEmpty = false;
        AnswerNumber = 0;
        qustionMark.color = Color.white;
        SpriteRenderer slotSprite = GetComponent<SpriteRenderer>();
        slotSprite.color = Color.white;
    }
}
