using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitController : BaseController
{
    protected float     _attackDelay = 0f;
    private Color       originColor;


    private GameObject  guidUnit = null;
    protected UnitStat  _stat;

    public Tile         Tile { get; private set; }
    public AttackRange  attackRange { get; private set; } = null;
    public bool         IsCollocating { get; protected set; } = false;

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

    public void BeforeCollocate(string path, int price)
    {
        SwitchIsCollocating();
        StartCoroutine(IEBeforeCollocate(path, price));
    }

    private void SetGuidUnit(string path)
    {
        guidUnit = Managers.Resource.Instantiate(path);
        SpriteRenderer[] sprites= guidUnit.GetComponentsInChildren<SpriteRenderer>();
        guidUnit.GetComponent<UnitController>().SwitchIsCollocating();
        Color color = Color.white;
        color.a = 0.5f;
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].color = color;
        }
    }

    public void SwitchIsCollocating()
    {
        if (IsCollocating == true)
        {
            IsCollocating = false;
            return;
        }

        IsCollocating = true;
    }

    private void Collocate(int price)
    {
        SwitchIsCollocating();
        transform.position = guidUnit.transform.position;
        guidUnit.GetComponentInChildren<SpriteRenderer>().color = originColor;
        Managers.Resource.Destroy(guidUnit);
        PlayerController player = Managers.Game.GetPlayer();
        player.UseMathEnergy(price);
        UI_Game uI_Game = Managers.UI.uI_Scene as UI_Game;
        uI_Game.SetTextMathEnergy(player);
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

    private IEnumerator IEBeforeCollocate(string path, int price)
    {
        if (guidUnit == null)
        {
            SetGuidUnit(path);
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
                        Collocate(price);
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
