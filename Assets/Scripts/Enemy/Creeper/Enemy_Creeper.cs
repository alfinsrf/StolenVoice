using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Creeper : Enemy
{
    public bool enemyPatrol;
    public bool flipToLeft;

    [Header("Sign Language")]
    public GameObject signLanguage_K;
    public GameObject signLanguage_I;
    public GameObject signLanguage_L;
    public GameObject signLanguage_L2;

    #region States
    //states
    public CreeperSpawnState spawnState { get; private set; }
    public CreeperIdleState idleState { get; private set; }
    public CreeperMoveState moveState { get; private set; }
    public CreeperBattleState battleState { get; private set; }
    public CreeperAttackState attackState { get; private set; }
    public CreeperStunnedState stunnedState { get; private set; }
    public CreeperDeadState deadState { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();

        spawnState = new CreeperSpawnState(this, stateMachine, "Spawn", this);
        idleState = new CreeperIdleState(this, stateMachine, "Idle", this);
        moveState = new CreeperMoveState(this, stateMachine, "Move", this);
        battleState = new CreeperBattleState(this, stateMachine, "Battle", this);
        attackState = new CreeperAttackState(this, stateMachine, "Attack", this);
        stunnedState = new CreeperStunnedState(this, stateMachine, "Stunned", this);
        deadState = new CreeperDeadState(this, stateMachine, "Die", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(spawnState);

        if (flipToLeft == true)
        {
            Flip();
        }

        // Sign Language
        signLanguage_K.SetActive(false);
        signLanguage_I.SetActive(false);
        signLanguage_L.SetActive(false);
        signLanguage_L2.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            stateMachine.ChangeState(stunnedState);
            return true;
        }

        return false;
    }

    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(stunnedState);
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject, 3f);
    }
}
