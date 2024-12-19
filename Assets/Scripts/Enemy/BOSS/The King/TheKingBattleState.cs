using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheKingBattleState : EnemyState
{
    private Enemy_TheKing enemy;
    private Transform player;

    private int moveDir;
    private bool flippedOnce;

    public TheKingBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_TheKing _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        player = PlayerManager.instance.player.transform;

        if (player.GetComponent<PlayerStats>().isDead)
        {
            stateMachine.ChangeState(enemy.idleState);
        }

        if (enemy.bossFightBegun == false)
        {
            stateMachine.ChangeState(enemy.idleState);
        }

        flippedOnce = false;

        //audio list
        //AudioManager.instance.PlaySFX(12, enemy.transform);
    }    

    public override void Update()
    {
        base.Update();

        if (DialogManager.instance.isDialogActive)
        {
            stateMachine.ChangeState(enemy.idleState);
            return;
        }

        if (enemy.bossFightBegun == false)
        {
            stateMachine.ChangeState(enemy.idleState);
        }

        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.battleTime;

            if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {
                if (CanAttack())
                {
                    stateMachine.ChangeState(enemy.attackState);
                }
                else
                {
                    stateMachine.ChangeState(enemy.idleState);
                }
            }
        }
        else
        {
            if(flippedOnce == false)
            {
                flippedOnce = true;
                enemy.Flip();
            }

            if (stateTimer < 0 || enemy.bossFightBegun == false)
            {
                stateMachine.ChangeState(enemy.idleState);
            }
        }

        if (player.position.x > enemy.transform.position.x)
        {
            moveDir = 1;
        }
        else if (player.position.x < enemy.transform.position.x)
        {
            moveDir = -1;
        }

        if (enemy.IsPlayerDetected() && enemy.IsPlayerDetected().distance < enemy.attackDistance - .1f)
        {
            return;
        }

        enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    private bool CanAttack()
    {
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            enemy.attackCooldown = Random.Range(enemy.minAttackCooldown, enemy.maxAttackCooldown);
            enemy.lastTimeAttacked = Time.time;
            return true;
        }

        return false;
    }
}
