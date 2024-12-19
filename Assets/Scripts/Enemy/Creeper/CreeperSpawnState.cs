using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreeperSpawnState : EnemyState
{
    private Enemy_Creeper enemy;
    public CreeperSpawnState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Creeper _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
