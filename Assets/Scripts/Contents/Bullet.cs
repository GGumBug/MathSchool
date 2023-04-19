using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _speed = 10f;
    public UnitStat unitStat { get; private set; }

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
            EnemyStat stat = collision.gameObject.GetComponent<EnemyStat>();
            stat.OnAttacked(unitStat);
            Managers.Resource.Destroy(gameObject);
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
