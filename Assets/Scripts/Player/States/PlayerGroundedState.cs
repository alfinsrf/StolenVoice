using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        
        if (!player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.airState);
        }

        //test
        if (DialogManager.instance.isDialogActive)
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }


        //INPUT LIST
        
        //Attack
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.J))
        {
            if (PlayerManager.instance.playerProgress < 2)
            {
                return;
            }

            stateMachine.ChangeState(player.primaryAttack);
        }

        //Jump
        if (Input.GetButtonDown("Jump") && player.IsGroundDetected())
        {
            if (PlayerManager.instance.playerProgress < 2)
            {
                return;
            }

            player.fx.PlayDustFX();
            stateMachine.ChangeState(player.jumpState);
        }
    }
}
