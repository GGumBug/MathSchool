using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlotSpawner : MonoBehaviour, QuizClear
{
    private Vector2Int slotCount = new Vector2Int(6, 1);
    private Vector2 offset = new Vector2(0.25f, -4.5f);
    private float interval = 0.3f;

    public List<GameObject> QuizSlots { get; private set; } = new List<GameObject>(); 

    public void SetSlotCount(int index)
    {
        slotCount.x = index;
    }

    public void SpwanSlot()
    {
        GameObject original = Managers.Resource.Load<GameObject>("Prefabs/Slot");
        SpriteRenderer sprite = original.GetComponent<SpriteRenderer>();
        float xSize = sprite.bounds.size.x;
        float ySize = sprite.bounds.size.y;

        for (int y = 0; y < slotCount.y; y++)
        {
            int fillSlot = Random.Range(0, 2);

            for (int x = 0; x < slotCount.x; x++)
            {
                GameObject go;
                if (x == 1)
                {
                    go = Managers.Resource.Instantiate("MultiplySlot", transform);
                }
                else if (x == 3)
                {
                    go = Managers.Resource.Instantiate("EqualSignSlot", transform);
                }
                else
                {
                    go = Managers.Resource.Instantiate("Slot", transform);
                }
                
                float px = ((-slotCount.x * 0.5f - offset.x) + x * xSize) + x * interval;
                float py = (slotCount.y * 0.5f + offset.y) - y * ySize;
                Vector3 position = new Vector3(px, py, 0);

                go.transform.position = position;
                go.name = $"Slot_{y},{x}";
                QuizSlots.Add(go);
            }
        }
    }

    public void QuizFadeOut()
    {
        List<SpriteRenderer> sprites = new List<SpriteRenderer>();
        float delay = 2f;

        foreach (GameObject item in QuizSlots)
        {
            sprites.Add(item.GetComponent<SpriteRenderer>());
        }

        foreach (SpriteRenderer item in sprites)
        {
            item.DOColor(Color.clear, delay);
        }

        StartCoroutine(WaitFadeOut(delay));
    }

    public IEnumerator WaitFadeOut(float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (GameObject item  in QuizSlots)
        {
            Slot slot = item.GetComponent<Slot>();
            if (slot != null)
                slot.ResetSlot();

            Managers.Resource.Destroy(item);
        }
        QuizSlots.Clear();
    }
}
