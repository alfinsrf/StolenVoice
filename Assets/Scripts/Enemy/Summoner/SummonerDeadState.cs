using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerDeadState : EnemyState
{
    private Enemy_Summoner enemy;

    public SummonerDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Summoner _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySFX(20, enemy.transform);        
    }

    public override void Update()
    {
        base.Update();

        enemy.SetZeroVelocity();        
    }
}
