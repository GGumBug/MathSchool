using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseController
{
    private float _skillDelay = 0;

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
        if (collision.gameObject.CompareTag("Unit"))
        {
            Debug.Log("Enter");
            LockTarget = collision.gameObject.GetComponent<Stat>();
            State = Define.State.Skill;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Unit"))
        {
            Debug.Log("Exit");
            LockTarget = null;
            _skillDelay = 0;
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

    protected override void UpdateSkill()
    {
        _skillDelay += Time.deltaTime;
        if (_skillDelay >= _stat.AtkDelay)
        {
            if (LockTarget != null)
            {
                LockTarget.OnAttacked(_stat);
                Debug.Log(_stat.Atk);
            }
            _skillDelay = 0;
        }
    }
}
