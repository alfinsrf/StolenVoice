using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Summoner : Enemy
{
    public bool enemyPatrol;
    public bool flipToLeft;

    [Header("Sign Language")]
    public GameObject signLanguage_K;
    public GameObject signLanguage_I;
    public GameObject signLanguage_L;
    public GameObject signLanguage_L2;


    [Header("Infected Specific")]
    public int infectedToCreate;
    public GameObject infectedPrefab;    

    #region States
    //states    
    public SummonerIdleState idleState { get; private set; }
    public SummonerMoveState moveState { get; private set; }
    public SummonerBattleState battleState { get; private set; }
    public SummonerAttackState attackState { get; private set; }
    public SummonerStunnedState stunnedState { get; private set; }
    public SummonerDeadState deadState { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();
        
        idleState = new SummonerIdleState(this, stateMachine, "Idle", this);
        moveState = new SummonerMoveState(this, stateMachine, "Move", this);
        battleState = new SummonerBattleState(this, stateMachine, "Battle", this);
        attackState = new SummonerAttackState(this, stateMachine, "Attack", this);
        stunnedState = new SummonerStunnedState(this, stateMachine, "Stunned", this);
        deadState = new SummonerDeadState(this, stateMachine, "Die", this);

    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);

        if (flipToLeft == true)
        {
            Flip();
        }

        // Sign Language
        signLanguage_K.SetActive(false);
        signLanguage_I.SetActive(false);
        signLanguage_L.SetActive(false);
        signLanguage_L2.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            stateMachine.ChangeState(stunnedState);
            return true;
        }

        return false;
    }

    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(stunnedState);        
    }

    public void CreateInfected(int _amountOfInfected, GameObject _infectedPrefab)
    {        
        for (int i = 0; i < _amountOfInfected; i++)
        {
            GameObject newInfected = Instantiate(_infectedPrefab, transform.position, Quaternion.identity);
        }
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject, 3f);
    }
}
