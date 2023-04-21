using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _speed = 10f;
    public UnitStat unitStat { get; private set; }

    private bool hasTriggered = false;

    private void OnEnable()
    {
        StopCoroutine("IEDestroy");
        StartCoroutine("IEDestroy");
    }

    private void Update()
    {
        transform.position += Vector3.right * Time.deltaTime * _speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!hasTriggered)
            {
                EnemyStat stat = collision.gameObject.GetComponent<EnemyStat>();
                stat.OnAttacked(unitStat);
            }
            hasTriggered = true;
            Managers.Resource.Destroy(gameObject);
            hasTriggered = false;
        }
    }

    public void SetStat(Stat stat)
    {
        UnitStat unit = stat as UnitStat;
        if (unit != null)
        {
            unitStat = unit;
        }
    }

    private IEnumerator IEDestroy()
    {
        yield return new WaitForSeconds(2f);

        Managers.Resource.Destroy(gameObject);
    }
}
