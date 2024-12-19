using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{    
    public bool canMove;    
    public bool isBusy { get; private set; }

    [Header("Movement & Jump Info")]
    public float moveSpeed;
    public float jumpForce;
    public float swordReturnImpact;
    private float defaultMoveSpeed;
    private float defaultJumpForce;

    [Header("Attacks Info")]
    public Vector2[] attackMovement;
    public float counterAttackDuration;

    [Header("Roll Info")]
    public float rollSpeed;
    public float rollDuration;
    private float defaultRollSpeed;
    public float rollCooldown;
    private float rollCooldownTimer;
    public float rollDir { get; private set; }       
    
    //FX
    public PlayerFX fx { get; private set; }

    [Header("Ground Check Position")]
    public Vector2 originalGCPosition;
    public Vector2 wallSlideGCPosition;

    #region States
    //state machine
    public PlayerStateMachine stateMachine { get; private set; }

    //state list
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }

    public PlayerWallSlideState wallSlide { get; private set; }
    public PlayerWallJumpState wallJump { get; private set; }

    public PlayerRollState rollState { get; private set; }    

    public PlayerPrimaryAttackState primaryAttack { get; private set; }    

    public PlayerDeadState deadState { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");

        wallSlide = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJump = new PlayerWallJumpState(this, stateMachine, "Jump");

        rollState = new PlayerRollState(this, stateMachine, "Roll");        

        primaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");        

        deadState = new PlayerDeadState(this, stateMachine, "Die");
    }


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
      
        fx = GetComponent<PlayerFX>();
        
        stateMachine.Initialize(idleState);
        
        defaultMoveSpeed = moveSpeed;
        defaultJumpForce = jumpForce;        
        defaultRollSpeed = rollSpeed;
        
        SetZeroVelocity();
        rb.velocity = Vector2.zero;        
        rollCooldownTimer = 0;        
        
        groundCheck.localPosition = originalGCPosition;

        if (PlayerManager.instance.playerProgress <= 1)
        {
            canMove = false;
        }
        else if (PlayerManager.instance.playerProgress >= 2)
        {
            canMove = true;
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        rollCooldownTimer -= Time.deltaTime;

        if (Time.timeScale == 0)
        {
            return;
        }

        base.Update();

        stateMachine.currentState.Update();

        
        CheckForRollInput();        
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (DialogManager.instance.isDialogActive)
            {
                return;
            }
            Inventory.instance.UseHealItem();
        }
    }

    public override void SlowEntityBy(float _slowPercentage, float _slowDuration)
    {
        moveSpeed = moveSpeed * (1 - _slowPercentage);
        jumpForce = jumpForce * (1 - _slowPercentage);        
        rollSpeed = rollSpeed * (1 - _slowPercentage);
        anim.speed = anim.speed * (1 - _slowPercentage);

        Invoke("ReturnDefaultSpeed", _slowDuration);
    }

    protected override void ReturnDefaultSpeed()
    {
        base.ReturnDefaultSpeed();

        moveSpeed = defaultMoveSpeed;
        jumpForce = defaultJumpForce;        
        rollSpeed = defaultRollSpeed;
    }    

    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;

        yield return new WaitForSeconds(_seconds);

        isBusy = false;
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();    

    #region Rolling
    public virtual bool CanUseSkill()
    {
        if (rollCooldownTimer < 0)
        {
            rollCooldownTimer = rollCooldown;

            return true;
        }        

        return false;
    }

    private void CheckForRollInput()
    {        
        if(DialogManager.instance.isDialogActive)
        {
            return;
        }

        if(PlayerManager.instance.playerProgress < 2)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && IsGroundDetected() && CanUseSkill())
        {            
            rollDir = Input.GetAxisRaw("Horizontal");

            if (rollDir == 0)
            {
                rollDir = facingDir;
            }

            stateMachine.ChangeState(rollState);
        }
    }
    #endregion

    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deadState);
    }

    #region Knockback Player
    protected override void SetupZeroKnockbackPower()
    {
        knockbackPower = Vector2.zero;
    }

    //for now just for trap
    public void Knockback(Transform damageTransform)
    {
        //AudioManager.instance.PlaySFX(9, null);

        fx.ScreenShake(fx.shakeHighDamage);

        isKnocked = true;

        #region x / horizontal direction for knockback
        int hDirection = 0;
        if (transform.position.x > damageTransform.position.x)
        {
            hDirection = 1;
        }
        else if (transform.position.x < damageTransform.position.x)
        {
            hDirection = -1;
        }
        #endregion

        rb.velocity = new Vector2(5 * hDirection, 10);

        Invoke("CancelKnockback", 0.5f);
    }

    private void CancelKnockback()
    {
        isKnocked = false;
        rb.velocity = new Vector2(0, rb.velocity.y);

    }
    #endregion    
}
