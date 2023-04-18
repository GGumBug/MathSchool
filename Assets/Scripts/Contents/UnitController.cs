using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitController : MonoBehaviour
{
    private GameObject guidUnit = null;

    public void BeforeCollocate()
    {
        StartCoroutine("IEBeforeCollocate");
    }

    private IEnumerator IEBeforeCollocate()
    {
        if (guidUnit == null)
        {
            guidUnit = Managers.Resource.Instantiate("Unit");
            SpriteRenderer sprite = guidUnit.GetComponentInChildren<SpriteRenderer>();
            Color color = sprite.color;
            color.a = 0.5f;
            sprite.color = color;
        }

        while (true)
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos;

            RaycastHit hit;
            LayerMask mask = LayerMask.GetMask("Tile");

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, mask))
            {
                Vector3 dest = hit.collider.transform.position;
                dest.z = 0;
                guidUnit.transform.position = dest;
            }


            if (Input.GetMouseButtonDown(0))
            {
                transform.position = guidUnit.transform.position;
                Managers.Resource.Destroy(guidUnit);
                yield break;
            }

            if (Input.GetMouseButtonDown(1))
            {
                Managers.Resource.Destroy(gameObject);
                Managers.Resource.Destroy(guidUnit);
            }

            yield return null;
        }
    }


}
