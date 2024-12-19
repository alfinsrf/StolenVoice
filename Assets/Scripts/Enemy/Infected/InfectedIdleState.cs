using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectedIdleState : InfectedGroundedState
{
    public InfectedIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Infected enemy) : base(_enemyBase, _stateMachine, _animBoolName, enemy)
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

        if(stateTimer < 0 && enemy.enemyPatrol == true && DialogManager.instance.isDialogActive == false)
        {
            AudioManager.instance.PlaySFX(19, enemy.transform);
            stateMachine.ChangeState(enemy.moveState);
        }
    }
}
