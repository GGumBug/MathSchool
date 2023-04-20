using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitController : BaseController
{
    private float _attackDelay = 0f;
    private Color originColor;


    private GameObject guidUnit = null;
    private UnitStat _stat;
    private AttackRange attackRange = null;

    public Tile Tile { get; private set; }


    protected override void Init()
    {
        _stat = GetComponent<UnitStat>();
        attackRange = gameObject.GetComponentInChildren<AttackRange>();
        if (attackRange == null)
        {
            Debug.Log("null");
        }
        attackRange.startAttackEvent += StartAttack;
        attackRange.stopAttackEvent += StopAttack;
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

        State = Define.State.Idle;
    }

    private void CancelCollocate()
    {
        Managers.Resource.Destroy(gameObject);
        guidUnit.GetComponentInChildren<SpriteRenderer>().color = originColor;
        Managers.Resource.Destroy(guidUnit);
        guidUnit = null;
    }

    private void StartAttack()
    {
        State = Define.State.Skill;
        _attackDelay = _stat.AtkDelay;
    }

    private void StopAttack()
    {
        State = Define.State.Idle;
        _attackDelay = _stat.AtkDelay;
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
                if (hit.collider == null)
                {
                    Debug.Log("잘못 된 배치 입니다.");
                    CancelCollocate();
                }
                else
                {
                    if (!hit.collider.gameObject.GetComponent<Tile>().IsEmpty)
                    {
                        Tile = hit.collider.GetComponent<Tile>();
                        Tile.SetIsEmpty();
                        Collocate();
                        yield break;
                    }
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                CancelCollocate();
            }

            yield return null;
        }
    }
}
