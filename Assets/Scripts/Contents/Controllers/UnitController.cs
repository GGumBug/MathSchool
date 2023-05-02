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
        SwitchIsCollocate();
        Managers.Game.SwitchUnitCollocating();
        StartCoroutine(IEBeforeCollocate(path, price));
    }

    private void SetGuidUnit(string path)
    {
        guidUnit = Managers.Resource.Instantiate(path);
        guidUnit.GetComponent<UnitController>().SwitchIsCollocate();
        SpriteRenderer[] sprites= guidUnit.GetComponentsInChildren<SpriteRenderer>();
        Color color = Color.white;
        color.a = 0.5f;
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].color = color;
        }
    }

    private void Collocate(int price)
    {
        transform.position = guidUnit.transform.position;
        guidUnit.GetComponentInChildren<SpriteRenderer>().color = originColor;
        Managers.Resource.Destroy(guidUnit);
        PlayerController player = Managers.Game.GetPlayer();
        player.GetComponent<PlayerStat>().UseMathEnergy(price);
        UI_Game uI_Game = Managers.UI.uI_Scene as UI_Game;
        uI_Game.SetTextMathEnergy(player);
        SwitchIsCollocate();
        Managers.Game.SwitchUnitCollocating();
    }

    public void SwitchIsCollocate()
    {
        if (IsCollocating == true)
        {
            IsCollocating = false;
            return;
        }

        IsCollocating = true;
    }

    private void CancelCollocate()
    {
        Managers.Game.SwitchUnitCollocating();
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

            RaycastHit[] hits;
            LayerMask mask = LayerMask.GetMask("Tile");
            hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition), mask);

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.CompareTag("Tile"))
                {
                    Vector3 dest = hit.collider.transform.position;
                    dest.z = 0;
                    guidUnit.transform.position = dest;

                    if (Input.GetMouseButtonDown(0))
                    {
                        if (!hit.collider.gameObject.GetComponent<Tile>().IsEmpty)
                        {
                            Tile = hit.collider.GetComponent<Tile>();
                            Tile.SetIsEmpty();
                            Collocate(price);
                            yield break;
                        }
                        else
                        {
                            Debug.Log("잘못 된 배치 입니다.");
                            CancelCollocate();
                        }
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
