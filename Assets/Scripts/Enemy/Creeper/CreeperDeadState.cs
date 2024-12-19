using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreeperDeadState : EnemyState
{
    private Enemy_Creeper enemy;
    public CreeperDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Creeper enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        
        AudioManager.instance.PlaySFX(21, enemy.transform);        
    }

    public override void Update()
    {
        base.Update();

        enemy.SetZeroVelocity();

        enemy.DestroyEnemy();
    }

}
