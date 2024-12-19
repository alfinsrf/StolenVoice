using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerIdleState : SummonerGroundedState
{
    public SummonerIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolNam, Enemy_Summoner _enemy) : base(_enemyBase, _stateMachine, _animBoolNam, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.idleTime;
    }

    public override void Exit()
    {
        base.Exit();        
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0 && enemy.enemyPatrol == true && DialogManager.instance.isDialogActive == false)
        {            
            stateMachine.ChangeState(enemy.moveState);
        }
    }
}
