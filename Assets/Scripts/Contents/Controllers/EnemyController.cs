using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseController
{
    private Vector3[] SpawnPos = new Vector3[3];

    private EnemyStat _stat;

    private void Awake()
    {
        SetSpawnPos();
    }

    private void OnEnable()
    {
        Spawn();
    }

    protected override void Init()
    {
        _stat = GetComponent<EnemyStat>();
        State = Define.State.Move;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            _stat.OnAttacked(bullet.unitStat);
        }

        if (collision.gameObject.CompareTag("Unit"))
        {
            State = Define.State.Skill;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Unit"))
        {
            State = Define.State.Move;
        }
    }

    private void SetSpawnPos()
    {
        for (int i = 0; i < 3; i++)
        {
            float yPos = 2.5f - (2.5f * i);
            Vector3 pos = new Vector3(10, yPos, 0);
            SpawnPos[i] = pos;
        }
    }

    private void Spawn()
    {
        int rand = Random.Range(0, SpawnPos.Length);
        transform.position = SpawnPos[rand];
    }

    protected override void UpdateMove()
    {
        transform.position += Vector3.left * Time.deltaTime * _stat.MoveSpeed;
    }
}
