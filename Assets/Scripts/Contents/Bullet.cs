using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _speed = 10f;
    public int Atk { get; private set; }

    private void OnEnable()
    {
        StopCoroutine("IEDestroy");
        StartCoroutine("IEDestroy");
    }

    private void Update()
    {
        transform.position += Vector3.right * Time.deltaTime * _speed;
    }

    public void SetAtk(int value)
    {
        Atk = value;
    }

    private IEnumerator IEDestroy()
    {
        yield return new WaitForSeconds(2f);

        Managers.Resource.Destroy(gameObject);
    }
}
