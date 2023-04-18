using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitController : BaseController
{
    private float _attackDelay = 10f;
    private Color originColor;
    

    private GameObject guidUnit = null;
    private UnitStat _stat;

    public Coroutine EnemyAttacked { get; private set; }
    public Tile Tile { get; private set; }
    

    protected override void Init()
    {
        _stat = GetComponent<UnitStat>();
    }

    public void BeforeCollocate()
    {
        StartCoroutine("IEBeforeCollocate");
    }

    private void SetGuidUnit()
    {
        guidUnit = Managers.Resource.Instantiate("Unit");
        SpriteRenderer sprite = guidUnit.GetComponentInChildren<SpriteRenderer>();
        originColor = sprite.color;
        Color color = sprite.color;
        color.a = 0.5f;
        sprite.color = color;
    }

    private void Collocate()
    {
        transform.position = guidUnit.transform.position;
        guidUnit.GetComponentInChildren<SpriteRenderer>().color = originColor;
        Managers.Resource.Destroy(guidUnit);

        // 일단 공격하게 테스트
        State = Define.State.Skill;
    }

    private void CancelCollocate()
    {
        Managers.Resource.Destroy(gameObject);
        guidUnit.GetComponentInChildren<SpriteRenderer>().color = originColor;
        Managers.Resource.Destroy(guidUnit);
        guidUnit = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyStat enemyStat = collision.GetComponent<EnemyStat>();
            float delay = enemyStat.AtkDelay;

            StartCoroutine(IEOnAttacked(enemyStat, delay));
        }
    }

    public void EndAttacked()
    {
        StopCoroutine(EnemyAttacked);
    }

    protected override void UpdateSkill()
    {
        _attackDelay += Time.deltaTime;
        if (_attackDelay >= _stat.AtkDelay)
        {
            GameObject go = Managers.Resource.Instantiate("Bullet");
            Bullet bullet = go.GetComponentInChildren<Bullet>();
            bullet.transform.position = transform.position;
            bullet.SetStat(_stat);
            _attackDelay = 0;
        }
    }

    private IEnumerator IEBeforeCollocate()
    {
        if (guidUnit == null)
        {
            SetGuidUnit();
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
                if (!hit.collider.gameObject.GetComponent<Tile>().IsEmpty)
                {
                    Tile = hit.collider.GetComponent<Tile>();
                    Tile.SetIsEmpty();
                    Collocate();
                    yield break;
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                CancelCollocate();
            }

            yield return null;
        }
    }

    private IEnumerator IEOnAttacked(EnemyStat enemyStat, float delay)
    {
        _stat.OnAttacked(enemyStat);

        yield return new WaitForSeconds(delay);

        EnemyAttacked = StartCoroutine(IEOnAttacked(enemyStat, delay));
    }
}
