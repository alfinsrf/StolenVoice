using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.groundCheck.localPosition = player.wallSlideGCPosition;
    }

    public override void Exit()
    {
        base.Exit();

        player.groundCheck.localPosition = player.originalGCPosition;
    }

    public override void Update()
    {
        base.Update();

        if (player.IsWallDetected() == false)
        {
            stateMachine.ChangeState(player.airState);
        }

        if (Input.GetButtonDown("Jump"))
        {
            stateMachine.ChangeState(player.wallJump);
            return;
        }

        if (yInput < 0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y * 0.7f);
        }

        if (xInput != 0 && player.facingDir != xInput)
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
