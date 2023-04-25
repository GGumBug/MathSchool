using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyController : BaseController
{
    private float _skillDelay = 0;

    private Vector3[] SpawnPos = new Vector3[3];

    private EnemyStat _stat;

    public bool StopAttack { get; private set; } = false;

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
            UnitController unit = collision.GetComponent<UnitController>();
            if (unit.IsCollocating)
                return;

            LockTarget = collision.gameObject.GetComponent<Stat>();
            State = Define.State.Idle;
        }

        if (collision.gameObject.CompareTag("EnemyDestination"))
        {
            Debug.Log("EnemyEscape!");
            State = Define.State.Idle;
            ChangeStopAttack();
            SpriteRenderer[] sprites = gameObject.GetComponentsInChildren<SpriteRenderer>();
            float duration = 2f;

            StartCoroutine(IEFadeOut(sprites, duration));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Unit"))
        {
            UnitController unit = collision.gameObject.GetComponent<UnitController>();
            if (unit.IsCollocating)
                return;
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

    public void ChangeStopAttack()
    {
        if (StopAttack == false)
        {
            StopAttack = true;
            return;
        }

        StopAttack = false;
    }

    protected override void UpdateMove()
    {
        transform.position += Vector3.left * Time.deltaTime * _stat.MoveSpeed;
    }

    protected override void UpdateIdle()
    {
        if (StopAttack)
            return;
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

    private IEnumerator IEFadeOut(SpriteRenderer[] sprites ,float duration)
    {
        for (int i = 0; i < sprites.Length; i++)
            sprites[i].material.DOFade(0f, duration);

        yield return new WaitForSeconds(duration);

        BaseScene curscene = Managers.Scene.CurrentScene;
        curscene.gameObject.GetComponent<StageController>().MinusFieldEnemyCount();
        _stat.ResetHP();

        Managers.Resource.Destroy(gameObject);
        ChangeStopAttack();
        State = Define.State.Move;
        for (int i = 0; i < sprites.Length; i++)
            sprites[i].material.color = Color.white;
    }
}
