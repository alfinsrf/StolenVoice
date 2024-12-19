using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectedAttackState : EnemyState
{
    private Enemy_Infected enemy;
    public InfectedAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Infected _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySFX(20, enemy.transform);
    }

    public override void Exit()
    {
        base.Exit();

        enemy.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        enemy.SetZeroVelocity();

        if(triggerCalled)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}
