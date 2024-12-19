using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossForbiddenLandCreatureIdleState : EnemyState
{
    private Enemy_BossForbiddenLandCreature enemy;
    public BossForbiddenLandCreatureIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_BossForbiddenLandCreature _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.SetZeroVelocity();

        stateTimer = enemy.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0 && enemy.bossFightBegun && DialogManager.instance.isDialogActive == false)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}
