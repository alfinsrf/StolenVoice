using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player player;    

    protected override void Start()
    {
        base.Start();

        player = GetComponent<Player>();        
    }    

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
    }

    protected override void Die()
    {
        base.Die();
        player.Die();        
    }

    protected override void DecreaseHealthBy(int _damage)
    {
        base.DecreaseHealthBy(_damage);

        if (isDead)
        {
            return;
        }

        if (_damage > GetMaxHealthValue() * .3f) //damage taken >= 30%
        {
            player.SetupKnockbackPower(new Vector2(10, 8));
            player.fx.ScreenShake(player.fx.shakeHighDamage);

            // audio
            //int randomSound = Random.Range(0, 1);
            AudioManager.instance.PlaySFX(7, null);
        }
        else
        {            
            player.fx.ScreenShake(new Vector3(.25f, .25f));
            AudioManager.instance.PlaySFX(6, null);
        }        
    }    
}
