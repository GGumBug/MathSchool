using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PencilController : UnitController
{
    protected override void UpdateSkill()
    {
        if (IsCollocating == true)
            return;

        _attackDelay += Time.deltaTime;
        if (_attackDelay >= _stat.AtkDelay)
        {
            Animator anim = GetComponentInChildren<Animator>();
            anim.CrossFade("ATTACK", 0.1f);
            GameObject go = Managers.Resource.Instantiate("Bullet");
            Bullet bullet = go.GetComponentInChildren<Bullet>();
            bullet.transform.position = transform.position;
            bullet.SetStat(_stat);
            _attackDelay = 0;
        }
    }
}
