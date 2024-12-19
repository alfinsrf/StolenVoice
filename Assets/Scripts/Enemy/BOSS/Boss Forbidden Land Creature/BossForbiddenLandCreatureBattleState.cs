using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossForbiddenLandCreatureBattleState : EnemyState
{
    private Enemy_BossForbiddenLandCreature enemy;
    private Transform player;

    private int moveDir;
    private bool flippedOnce;

    public BossForbiddenLandCreatureBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_BossForbiddenLandCreature _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        player = PlayerManager.instance.player.transform;

        if (player.GetComponent<PlayerStats>().isDead)
        {
            enemy.bossFightBegun = false;
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
                    int attackNumber = Random.Range(1, 3);

                    switch (attackNumber)
                    {
                        case 1:
                            stateMachine.ChangeState(enemy.attackState);
                            break;
                        case 2:
                            stateMachine.ChangeState(enemy.thirdAttackState);
                            break;                        
                        default:
                            break;
                    }
                }
                else
                {
                    stateMachine.ChangeState(enemy.idleState);
                }
            }
        }
        else
        {
            if (flippedOnce == false)
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
            int AttackChance = Random.Range(1, 3);

            switch (AttackChance)
            {
                case 1:                    
                    break;
                case 2:
                    stateMachine.ChangeState(enemy.secondaryAttackState);
                    break;
                case 3:
                    break;
                default:
                    break;
            }
        }

        enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);
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
