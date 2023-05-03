using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrateOnClick : MonoBehaviour
{
    public void StartVibrate()
    {
        StartCoroutine(VibrateObject());
    }

    public IEnumerator VibrateObject(float distance = 0.1f, float speed = 10.0f, float duration = 0.5f, float timeElapsed = 0.0f)
    {
        Vector3 startPosition = transform.position;

        while (true)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed < duration)
            {
                float t = timeElapsed / duration;
                float theta = t * Mathf.PI * 2.0f;
                float distanceMultiplier = Mathf.Sin(theta * speed) * (1.0f - t);
                Vector3 newPosition = startPosition + (transform.right * distance * distanceMultiplier);

                transform.position = newPosition;
            }
            else
            {
                transform.position = startPosition;
                yield break;
            }
            yield return null;
        }
    }
}
