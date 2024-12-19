using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossForbiddenLandCreatureSecondaryAttackState : EnemyState
{
    private Enemy_BossForbiddenLandCreature enemy;
    public BossForbiddenLandCreatureSecondaryAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_BossForbiddenLandCreature _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.attackCheck.localPosition = enemy.attackPosition2;
        enemy.attackCheckRadius = 4;
    }

    public override void Exit()
    {
        base.Exit();

        enemy.lastTimeAttacked = Time.time;

        enemy.attackCheck.localPosition = enemy.attackPosition1;
        enemy.attackCheckRadius = 1;
    }

    public override void Update()
    {
        base.Update();

        enemy.SetZeroVelocity();

        if (enemy.bossFightBegun == false)
        {
            stateMachine.ChangeState(enemy.idleState);
        }

        if (triggerCalled)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}
