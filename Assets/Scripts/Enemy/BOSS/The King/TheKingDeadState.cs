using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheKingDeadState : EnemyState
{
    private Enemy_TheKing enemy;

    private bool haveLastMessage = false;
    public TheKingDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_TheKing _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        //AudioManager.instance.PlaySFX(6, enemy.transform);

        stateTimer = 0.2f;

        enemy.bossFightBegun = false;
        enemy.SetZeroVelocity();

        haveLastMessage = true;        
    }

    public override void Update()
    {
        base.Update();

        enemy.SetZeroVelocity();

        if (stateTimer < 0)
        {
            if (haveLastMessage)
            {
                enemy.fx.CreatePopUpText("I will be back...", Color.white, 3);
                PlayerManager.instance.playerProgress = enemy.progressForPlayer;
                haveLastMessage = false;
                enemy.DestroyEnemy();
            }
        }        
    }
}
