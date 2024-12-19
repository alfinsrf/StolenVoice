using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum StatType
{    
    damage,    
    health,
    armor    
}

public class CharacterStats : MonoBehaviour
{
    private EntityFX fx;
    
    public Stat damage;        
    public Stat maxHealth;
    public Stat armor;    
    
    public int currentHealth;

    public System.Action onHealthChanged;    
    public bool isDead;
    public bool isInvincible { get; private set; }
    private bool isVulnerable;

    // Start is called before the first frame update
    protected virtual void Start()
    {        
        fx = GetComponent<EntityFX>();
        
        currentHealth = GetMaxHealthValue();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public void MakeVulnerableFor(float _duration) => StartCoroutine(VulnerableCoroutine(_duration));

    private IEnumerator VulnerableCoroutine(float _duration)
    {
        isVulnerable = true;

        yield return new WaitForSeconds(_duration);

        isVulnerable = false;
    }

    public virtual void IncreaseStatBy(int _modifier, float _duration, Stat _statToModify)
    {
        StartCoroutine(StatModCoroutine(_modifier, _duration, _statToModify));
    }

    private IEnumerator StatModCoroutine(int _modifier, float _duration, Stat _statToModify)
    {
        _statToModify.AddModifier(_modifier);

        yield return new WaitForSeconds(_duration);

        _statToModify.RemoveModifier(_modifier);
    }

    public virtual void DoDamage(CharacterStats _targetStats)
    {
        bool criticalStrike = false;

        if (_targetStats.isInvincible)
        {
            return;
        }        

        _targetStats.GetComponent<Entity>().SetupKnockbackDir(transform);

        int totalDamage = damage.GetValue();        

        fx.CreateHitFX(_targetStats.transform, criticalStrike);

        totalDamage = CheckTargetArmor(_targetStats, totalDamage);

        _targetStats.TakeDamage(totalDamage);                
    }

    public virtual void TakeDamage(int _damage)
    {
        if (isInvincible)
        {
            return;
        }

        DecreaseHealthBy(_damage);

        GetComponent<Entity>().DamageImpact();
        fx.StartCoroutine("FlashFX");

        if (currentHealth < 0 && !isDead)
        {
            Die();
        }
    }

    public virtual void IncreaseHealthBy(int _amount)
    {
        currentHealth += _amount;

        if (currentHealth > GetMaxHealthValue())
        {
            currentHealth = GetMaxHealthValue();
        }

        if (onHealthChanged != null)
        {
            onHealthChanged();
        }
    }

    protected virtual void DecreaseHealthBy(int _damage)
    {
        if (isVulnerable)
        {
            _damage = Mathf.RoundToInt(_damage * 1.1f);
        }

        currentHealth -= _damage;        

        if (onHealthChanged != null)
        {
            onHealthChanged();
        }
    }

    protected virtual void Die()
    {
        isDead = true;
    }

    public void KillEntity()
    {
        if (!isDead)
        {
            Die();
        }
    }

    public void MakeInvincible(bool _invincible)
    {
        isInvincible = _invincible;
    }

    #region stat Calculation

    protected int CheckTargetArmor(CharacterStats _targetStats, int totalDamage)
    {        
        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        return totalDamage;
    }    

    public virtual void OnEvasion()
    {

    }    

    public int GetMaxHealthValue()
    {
        return maxHealth.GetValue();
    }

    #endregion

    public Stat GetStat(StatType _statType)
    {
        
        if (_statType == StatType.damage)
        {
            return damage;
        }        
        else if (_statType == StatType.health)
        {
            return maxHealth;
        }
        else if (_statType == StatType.armor)
        {
            return armor;
        }        
        return null;
    }
}
