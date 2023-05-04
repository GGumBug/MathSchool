using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharpController : UnitController
{
    protected override void UpdateSkill()
    {
        if (IsCollocating)
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
            Invoke("DoubleShot", 0.2f);
            _attackDelay = 0;
        }
    }

    private void DoubleShot()
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
