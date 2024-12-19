using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreeperIdleState : CreeperGroundedState
{
    public CreeperIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Creeper _enemy) : base(_enemyBase, _stateMachine, _animBoolName, _enemy)
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
            AudioManager.instance.PlaySFX(20, enemy.transform);
            stateMachine.ChangeState(enemy.moveState);
        }
    }
}
