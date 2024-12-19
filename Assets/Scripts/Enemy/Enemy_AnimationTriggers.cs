using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AnimationTriggers : MonoBehaviour
{
    private Enemy enemy => GetComponentInParent<Enemy>();

    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
                PlayerStats target = hit.GetComponent<PlayerStats>();
                enemy.stats.DoDamage(target);
            }
        }
    }
    private void SpeicalAttackTrigger()
    {
        enemy.AnimationSpecialAttackTrigger();
    }

    private void OpenCounterWindow() => enemy.OpenCounterAttackWindow();
    private void CloseCounterWindow() => enemy.CloseCounterAttackWindow();
    private void MakeEnemyInvisible() => enemy.fx.MakeTransparent(true);
    private void MakeEnemyVisible() => enemy.fx.MakeTransparent(false);
    //
    private void MakeEnemyHealthBarInvisible() => enemy.fx.MakeHealthBarTransparent(true);
    private void MakeEnemyHealthBarVisible() => enemy.fx.MakeHealthBarTransparent(false);

    //
    private void MakeEnemyInvincibleFromDamage() => enemy.stats.MakeInvincible(true);
    private void MakeEnemyCanTakeDamage() => enemy.stats.MakeInvincible(false);

    //ATTACK SOUND
    public void MasterPandaAttackSound()
    {
        AudioManager.instance.PlaySFX(9, enemy.transform);
    }

    public void GoblinAttackSpearSound()
    {
        AudioManager.instance.PlaySFX(13, enemy.transform);
    }

    public void AssassinAttackDaggerSound()
    {
        AudioManager.instance.PlaySFX(16, enemy.transform);
    }

    public void TrollSlamGround()
    {
        AudioManager.instance.PlaySFX(17, enemy.transform);
    }
}
