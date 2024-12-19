using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollState : PlayerState
{
    public PlayerRollState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        if (player.GetComponent<PlayerStats>().isDead)
        {
            stateMachine.ChangeState(player.deadState);
            return;
        }

        base.Enter();               

        stateTimer = player.rollDuration;

        player.stats.MakeInvincible(true);
        player.fx.PlayDustFX();
    }

    public override void Exit()
    {
        base.Exit();        

        player.SetVelocity(0, rb.velocity.y);

        player.stats.MakeInvincible(false);
    }

    public override void Update()
    {
        base.Update();

        if (!player.IsGroundDetected() && player.IsWallDetected())
        {
            stateMachine.ChangeState(player.wallSlide);
        }

        player.SetVelocity(player.rollSpeed * player.rollDir, 0);

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
