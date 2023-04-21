using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    public Stat LockTarget {get; protected set;}

    private Define.State _state = Define.State.Idle;

    public virtual Define.State State 
    {
        get { return _state; }
        set
        {
            _state = value;

            Animator anim = GetComponentInChildren<Animator>();
            switch (_state)
            {
                case Define.State.Die:
                    break;
                case Define.State.Move:
                    break;
                case Define.State.Idle:
                    anim.CrossFade("IDLE", 0.1f);
                    break;
                case Define.State.Skill:
                    break;
            }
        }
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        switch (_state)
        {
            case Define.State.Die:
                UpdateDie();
                break;
            case Define.State.Move:
                UpdateMove();
                break;
            case Define.State.Idle:
                UpdateIdle();
                break;
            case Define.State.Skill:
                UpdateSkill();
                break;
        }
    }

    protected abstract void Init();
    protected virtual void UpdateDie() { }
    protected virtual void UpdateIdle() { }
    protected virtual void UpdateMove() { }
    protected virtual void UpdateSkill() { }
}
