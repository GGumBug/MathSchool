using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [field: SerializeField]
    public int Level { get; protected set; }
    [field: SerializeField]
    public string Name { get; protected set; }
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
        // 적이 죽을때 로직
        if (unitStat != null)
        {
            // 수학에너지를 보너스로 받는 로직
            EnemyStat stat = gameObject.GetComponent<EnemyStat>();
            stat.ResetHP();
            PlayerController player = Managers.Game.GetPlayer();
            player.PlusMathEnerge(stat.Value);
            UI_Game uI_Game = Managers.UI.uI_Scene as UI_Game;
            uI_Game.SetTextMathEnergy(player);
        }

        EnemyStat enemyStat = attacker as EnemyStat;
        // 유닛이 죽을때 로직
        if (enemyStat != null)
        {
                UnitController unit = gameObject.GetComponent<UnitController>();
                unit.Tile.SetIsEmpty();
        }

        Managers.Resource.Destroy(gameObject);
    }
}
