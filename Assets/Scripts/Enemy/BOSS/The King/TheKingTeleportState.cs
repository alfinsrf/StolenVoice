public class TheKingTeleportState : EnemyState
{
    private Enemy_TheKing enemy;
    public TheKingTeleportState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_TheKing _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.stats.MakeInvincible(true);
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            if(enemy.CanDoSpellCast())
            {
                stateMachine.ChangeState(enemy.spellCastState);
            }
            else
            {
                stateMachine.ChangeState(enemy.battleState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        
        enemy.stats.MakeInvincible(false);
    }
}
