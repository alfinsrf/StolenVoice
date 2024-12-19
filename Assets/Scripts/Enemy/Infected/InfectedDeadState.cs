using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectedDeadState : EnemyState
{
    private Enemy_Infected enemy;
    public InfectedDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Infected _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
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
