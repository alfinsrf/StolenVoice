using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreeperGroundedState : EnemyState
{
    protected Enemy_Creeper enemy;

    protected Transform player;
    public CreeperGroundedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Creeper _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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

        if (DialogManager.instance.isDialogActive)
        {
            stateMachine.ChangeState(enemy.idleState);
            return;
        }        

        if (Vector2.Distance(enemy.transform.position, player.position) < 7)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}
