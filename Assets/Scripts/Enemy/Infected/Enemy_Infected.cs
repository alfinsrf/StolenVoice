using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Infected : Enemy
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
    public InfectedSpawnState spawnState { get; private set; }
    public InfectedIdleState idleState { get; private set; }
    public InfectedMoveState moveState { get; private set; }
    public InfectedBattleState battleState { get; private set; }
    public InfectedAttackState attackState { get; private set; }
    public InfectedStunnedState stunnedState { get; private set; }
    public InfectedDeadState deadState { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();

        spawnState = new InfectedSpawnState(this, stateMachine, "Spawn", this);
        idleState = new InfectedIdleState(this, stateMachine, "Idle", this);
        moveState = new InfectedMoveState(this, stateMachine, "Move", this);
        battleState = new InfectedBattleState(this, stateMachine, "Battle", this);
        attackState = new InfectedAttackState(this, stateMachine, "Attack", this);
        stunnedState = new InfectedStunnedState(this, stateMachine, "Stunned", this);
        deadState = new InfectedDeadState(this, stateMachine, "Die", this);

    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(spawnState);

        if(flipToLeft == true)
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
        if(base.CanBeStunned())
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
