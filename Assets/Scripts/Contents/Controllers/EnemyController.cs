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
            LockTarget = collision.gameObject.GetComponent<Stat>();
            State = Define.State.Idle;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Unit"))
        {
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

    protected override void UpdateIdle()
    {
        _skillDelay += Time.deltaTime;
        if (_skillDelay >= _stat.AtkDelay)
        {
            if (LockTarget != null)
            {
                LockTarget.OnAttacked(_stat);
                Debug.Log(_stat.Atk);
            }
            Animator anim = GetComponentInChildren<Animator>();
            anim.CrossFade("ATTACK", 0.1f);
            _skillDelay = 0;
        }
    }
}
