using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectedGroundedState : EnemyState
{
    protected Enemy_Infected enemy;
    protected Transform player;
    public InfectedGroundedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Infected _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        player = PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        //TEST
        if (DialogManager.instance.isDialogActive)
        {
            stateMachine.ChangeState(enemy.idleState);
            return;
        }

        if (enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position, player.position) < 5)
        {
            stateMachine.ChangeState(enemy.battleState);
        }        
    }
}
