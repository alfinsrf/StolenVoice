using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheKingIdleState : EnemyState
{
    private Enemy_TheKing enemy;
    private Transform player;

    public TheKingIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_TheKing _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.idleTime;
        player = PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();        
    }

    public override void Update()
    {
        base.Update();        

        if(stateTimer < 0 && enemy.bossFightBegun && DialogManager.instance.isDialogActive == false)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}
