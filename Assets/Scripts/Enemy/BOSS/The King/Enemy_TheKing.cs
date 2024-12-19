using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy_TheKing : Enemy
{
    #region States
    //states    
    public TheKingBattleState battleState { get; private set; }
    public TheKingAttackState attackState { get; private set; }
    public TheKingIdleState idleState { get; private set; }
    public TheKingDeadState deadState { get; private set; }  
    public TheKingSpellCastState spellCastState { get; private set; }
    public TheKingTeleportState teleportState { get; private set; }

    #endregion

    //conditions
    public bool flipToLeft;
    public int progressForPlayer;

    public bool bossFightBegun = false;

    [Header("Spell Cast Details")]
    [SerializeField] private GameObject spellPrefab;
    public int amountOfSpells;
    public float spellCooldown;
    public float lastTimeCast;
    [SerializeField] private float spellStateCooldown;
    [SerializeField] private Vector2 spellOffset;

    [Header("Teleport details")]
    [SerializeField] private BoxCollider2D arena;
    [SerializeField] private Vector2 surroundingCheckSize;
    public float chanceToTeleport;
    public float defaultChanceToTeleport = 25;


    protected override void Awake()
    {
        base.Awake();

        battleState = new TheKingBattleState(this, stateMachine, "Move", this);
        attackState = new TheKingAttackState(this, stateMachine, "Attack", this);
        idleState = new TheKingIdleState(this, stateMachine, "Idle", this);        
        deadState = new TheKingDeadState(this, stateMachine, "Die", this);
        spellCastState = new TheKingSpellCastState(this, stateMachine, "SpellCast", this);
        teleportState = new TheKingTeleportState(this, stateMachine, "Teleport", this);

    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);

        if (flipToLeft == true)
        {
            Flip();
        }
    }

    protected override void Update()
    {
        base.Update();

        if (PlayerManager.instance.playerProgress >= progressForPlayer)
        {
            Destroy(gameObject, 3);
        }
    }    

    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deadState);
    }

    public void CastSpell()
    {
        Player player = PlayerManager.instance.player;

        float xOffset = 0;

        if(player.rb.velocity.x != 0)
        {
            xOffset = player.facingDir * spellOffset.x;     
        }

        Vector3 spellPosition = new Vector3(player.transform.position.x + xOffset, transform.position.y + spellOffset.y);

        GameObject newSpell = Instantiate(spellPrefab, spellPosition, Quaternion.identity);
        newSpell.GetComponent<TheKingSpell_Controller>().SetupSpell(stats);
    }

    public void FindPosition()
    {
        float x = Random.Range(arena.bounds.min.x + 3, arena.bounds.max.x - 3);
        float y = Random.Range(arena.bounds.min.y + 3, arena.bounds.max.y - 3);

        transform.position = new Vector3(x, y);
        transform.position = new Vector3(transform.position.x, transform.position.y - GroundBelow().distance + (cd.size.y / 2));

        if (!GroundBelow() || SomethingIsAround())
        {
            Debug.Log("Looking for new position");
            FindPosition();
        }
    }

    private RaycastHit2D GroundBelow() => Physics2D.Raycast(transform.position, Vector2.down, 50, whatIsGround);

    private bool SomethingIsAround() => Physics2D.BoxCast(transform.position, surroundingCheckSize, 0, Vector2.zero, 0, whatIsGround);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - GroundBelow().distance));
        Gizmos.DrawWireCube(transform.position, surroundingCheckSize);
    }

    public bool CanTeleport()
    {
        if (Random.Range(0, 100) <= chanceToTeleport)
        {
            chanceToTeleport = defaultChanceToTeleport;
            return true;
        }

        return false;
    }

    public bool CanDoSpellCast()
    {
        if(Time.time >= lastTimeCast + spellStateCooldown)
        {            
            return true;
        }

        return false;
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject, 2f);
    }
}
