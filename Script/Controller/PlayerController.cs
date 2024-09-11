using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CreatureController, Move, Jump
{
    Rigidbody2D rbody;
    Animator _anim;
    protected override void Init()
    {
        Managers.Input._inputEvent += KeyCheck;
        Managers.Input._mouseEvent += MouseCheck;
        rbody = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        Camera.main.transform.parent = transform;
        
    }

    public void Damaged(float dmg)
    {
        if (Managers.DataManager.PlayerHP <= 0)
            return;
        Managers.DataManager.PlayerHP -= dmg;
        if (Managers.DataManager.PlayerHP <= 0)
        {
            dead();
        }
    }
    protected override void idle()
    {
        _anim.Play("PlayerIdle");
    }
    protected override void dead()
    {
        base.dead();
    }
    protected override void attack()
    {
        base.attack();
    }
    protected override void damaged()
    {
        base.damaged();
    }
    protected override void jump()
    {
        base.jump();
    }
    protected override void run()
    {
        _anim.Play("PlayerRun");
    }
    void KeyCheck(Define.Keys k)
    {
        switch (k)
        {
            case Define.Keys.left:
                MoveLeft();
                break;
            case Define.Keys.right:
                MoveRight();
                break;
            case Define.Keys.Jump:
                Jump();
                break;
            case Define.Keys.None:
                if (State != CreatureState.Jump)
                    State = CreatureState.Idle;
                rbody.velocity = new Vector2(0, rbody.velocity.y);
                break;
        }
    }
    void MouseCheck(Vector2 mousePos, Define.Mouse click)
    {
        Ray2D ray = new Ray2D();
        ray.origin = transform.position;
        ray.direction = (mousePos - (Vector2)transform.position);
        if (ray.direction.magnitude > 6)
            ray.direction = ray.direction.normalized * 6;
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);
        Debug.DrawRay(ray.origin, ray.direction);
        foreach( RaycastHit2D hit in hits)
        {
            if (hit.transform.CompareTag("block"))
            {
                BlockController _block = hit.transform.GetComponent<BlockController>();
                if (_block == null)
                    return ;
                switch (click)
                {
                    case Define.Mouse.LeftClick:
                        _block.BlockBreaking();
                        break;
                    case Define.Mouse.LeftClickUp:
                        _block.UnBreaking();
                        break;
                    case Define.Mouse.RightClick:
                        break;
                }
                break;
            }
        }
    }

    public void MoveRight()
    {
        if (State != CreatureState.Jump)
        {
            State = CreatureState.Run;
        }
        transform.Translate(Managers.DataManager.Playerspeed * Time.deltaTime, 0, 0);
        GetComponent<SpriteRenderer>().flipX = false;
    }

    public void MoveLeft()
    {
        if (State != CreatureState.Jump)
        {
            State = CreatureState.Run;
        }
        transform.Translate(-Managers.DataManager.Playerspeed * Time.deltaTime, 0, 0);
        GetComponent<SpriteRenderer>().flipX = true;
    }
    public void Jump()
    {
        if (State != CreatureState.Jump)
        {
            State = CreatureState.Jump;
            rbody.velocity = new Vector3(rbody.velocity.x, Managers.DataManager.PlayerJumpPower, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("block"))
        {
            State = CreatureState.Idle;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("block"))
        {
            State = CreatureState.Jump;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (State != CreatureState.Jump)
            return;
        if (transform.position.y - collision.contacts[0].point.y < 1f)
        {
            if (collision.gameObject.CompareTag("block"))
            {
                State = CreatureState.Idle;
            }
        }

    }
}
