using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossForbiddenLandCreatureDeadState : EnemyState
{
    private Enemy_BossForbiddenLandCreature enemy;

    private bool haveLastMessage = false;
    public BossForbiddenLandCreatureDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_BossForbiddenLandCreature _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = 0.2f;

        enemy.bossFightBegun = false;
        enemy.SetZeroVelocity();
        //AudioManager.instance.PlaySFX(6, enemy.transform); //monster before dead sound
        //AudioManager.instance.PlaySFX(6, enemy.transform); //explosion sounds

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
                PlayerManager.instance.playerProgress = enemy.progressForPlayer;
                haveLastMessage = false;
                enemy.DestroyEnemy();
            }
        }        
    }
}
