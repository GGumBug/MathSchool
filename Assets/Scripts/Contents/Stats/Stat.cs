using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [field: SerializeField]
    public int Hp { get; protected set; }
    [field: SerializeField]
    public int MaxHp { get; protected set; }
    [field: SerializeField]
    public int Atk { get; protected set; }
    [field: SerializeField]
    public float AtkDelay { get; protected set; }

    public virtual void OnAttacked(Stat attacker)
    {
        int damage = Mathf.Max(0, attacker.Atk);
        Hp -= damage;
        Hp = Mathf.Clamp(Hp, 0, MaxHp);

        if (Hp <= 0)
        {
            OnDead(attacker);
        }
    }

    protected virtual void OnDead(Stat attacker)
    {
        UnitStat unitStat = attacker as UnitStat;
        if (unitStat != null)
        {
            // 수학에너지를 보너스로 받는 로직
            gameObject.GetComponent<EnemyStat>().ResetStat();
            unitStat.GetComponent<UnitController>().EndAttacked();
        }

        EnemyStat enemyStat = attacker as EnemyStat;
        if (enemyStat != null)
        {
            UnitController unit = gameObject.GetComponent<UnitController>();
            unit.Tile.SetIsEmpty();
            unit.EndAttacked();
        }

        Managers.Resource.Destroy(gameObject);
    }
}
