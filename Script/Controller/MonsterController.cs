using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : CreatureController, HP, Speed, OnDamaged, Attack
{
    public float speed { get; set; }
    public float HP { get; set; }
    public int MaxHP { get; set; }
    public Define.MonsterName Monstername = Define.MonsterName.None;
    public Define.MonsterType type { get; set; }
    public float Attack { get; set; }
    
    void Start()
    {
        Init();
    }
    protected override void Init()
    {
        name = Managers.DataManager.MonsterInfoDict[(int)Monstername].Name;
        MaxHP = Managers.DataManager.MonsterInfoDict[(int)Monstername].MaxHP;
        HP = MaxHP;
        speed = Managers.DataManager.MonsterInfoDict[(int)Monstername].Speed;
        Attack = Managers.DataManager.MonsterInfoDict[(int)Monstername].Atk;
        Debug.Log(name);
        Debug.Log(MaxHP);
        Debug.Log(HP);
        Debug.Log(speed);
        Debug.Log(Attack);
    }
    public void Damaged(float dmg)
    {
        HP -= dmg;
        if (HP <= 0)
            Die();
    }
    public virtual void Die()
    {

    }

    void Attack.attack()
    {
        
    }
}
