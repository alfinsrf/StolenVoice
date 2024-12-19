using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BossForbiddenLandCreature : Enemy
{
    //conditions
    public bool flipToLeft;
    public int progressForPlayer;

    [Header("Attack Check Position")]
    public Vector2 attackPosition1;
    public Vector2 attackPosition2;
    public Vector2 attackPosition3;

    #region States
    //states    
    public BossForbiddenLandCreatureBattleState battleState { get; private set; }
    public BossForbiddenLandCreaturePrimaryAttackState attackState { get; private set; }
    public BossForbiddenLandCreatureSecondaryAttackState secondaryAttackState { get; private set; }
    public BossForbiddenLandCreatureThirdAttackState thirdAttackState { get; private set; }
    public BossForbiddenLandCreatureIdleState idleState { get; private set; }
    public BossForbiddenLandCreatureDeadState deadState { get; private set; }
    #endregion

    [Header("Boss Fight Trigger")]
    public bool bossFightBegun;

    protected override void Awake()
    {
        base.Awake();

        battleState = new BossForbiddenLandCreatureBattleState(this, stateMachine, "Move", this);
        attackState = new BossForbiddenLandCreaturePrimaryAttackState(this, stateMachine, "Attack", this);
        secondaryAttackState = new BossForbiddenLandCreatureSecondaryAttackState(this, stateMachine, "SecondAttack", this);
        thirdAttackState = new BossForbiddenLandCreatureThirdAttackState(this, stateMachine, "ThirdAttack", this);
        idleState = new BossForbiddenLandCreatureIdleState(this, stateMachine, "Idle", this);
        deadState = new BossForbiddenLandCreatureDeadState(this, stateMachine, "Die", this);
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);

        if (flipToLeft == true)
        {
            Flip();
        }
    }

    protected override void Update()
    {
        base.Update();

        if (PlayerManager.instance.playerProgress >= progressForPlayer)
        {
            Destroy(gameObject, 3);
        }
    }

    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deadState);
    }    

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();        
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject, 2f);
    }
}
