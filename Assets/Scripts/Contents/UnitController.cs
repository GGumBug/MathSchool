using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitController : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine("IEBeforeCollocate");
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.activeSelf)
        {
            Debug.Log("충돌");
        }
    }

    private IEnumerator IEBeforeCollocate()
    {
        while (true)
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            transform.position = mousePos;

            if (Input.GetMouseButtonDown(0))
            {
                yield break;
            }

            if (Input.GetMouseButtonDown(1))
            {
                Managers.Resource.Destroy(gameObject);
            }

            yield return null;
        }
    }
}
