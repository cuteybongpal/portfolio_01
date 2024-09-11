using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureController : BaseController
{
    public enum CreatureState
    {
        Idle,
        Run,
        Attack,
        Jump,
        Damaged,
        Dead
    }
    public CreatureState _state = CreatureState.Idle;

    public CreatureState State
    {
        get { return _state; }
        set {
            _state = value;
            switch (_state)
            {
                case CreatureState.Idle:
                    idle();
                    break;
                case CreatureState.Run:
                    run();
                    break;
                case CreatureState.Attack:
                    attack();
                    break;
                case CreatureState.Jump:
                    jump();
                    break;
                case CreatureState.Damaged:
                    damaged();
                    break;
                case CreatureState.Dead:
                    dead();
                    break;
                default:
                    break;
            }
        }
    }
    protected virtual void idle()
    {

    }
    protected virtual void run()
    {

    }
    protected virtual void jump()
    {

    }
    protected virtual void attack()
    {

    }
    protected virtual void damaged()
    {

    }
    protected virtual void dead()
    {

    }
}
