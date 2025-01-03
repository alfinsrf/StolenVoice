using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectedBattleState : EnemyState
{
    private Transform player;
    private Enemy_Infected enemy;

    private int moveDir;
    private bool flippedOnce;
    
    public InfectedBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Infected _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        player = PlayerManager.instance.player.transform;

        if(player.GetComponent<PlayerStats>().isDead)
        {
            stateMachine.ChangeState(enemy.moveState);
        }

        stateTimer = enemy.battleTime;
        flippedOnce = false;
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

        enemy.anim.SetFloat("xVelocity", enemy.rb.velocity.x);

        if(enemy.IsPlayerDetected())
        {
            stateTimer = enemy.battleTime;

            if(enemy.IsPlayerDetected().distance < enemy.attackDistance) 
            {
                if(CanAttack())
                {
                    stateMachine.ChangeState(enemy.attackState);
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

            if(stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > 4)
            {
                stateMachine.ChangeState(enemy.idleState);
            }
        }

        if(player.position.x > enemy.transform.position.x)
        {
            moveDir = 1;
        }
        else if(player.position.x < enemy.transform.position.x) 
        { 
            moveDir = -1; 
        }

        if (enemy.IsPlayerDetected() && enemy.IsPlayerDetected().distance < enemy.attackDistance - .1f)
        {
            return;
        }

        enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);
    }

    private bool CanAttack()
    {
        if(Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            enemy.attackCooldown = Random.Range(enemy.minAttackCooldown, enemy.maxAttackCooldown);
            enemy.lastTimeAttacked = Time.time;
            return true;
        }

        return false;
    }
}
