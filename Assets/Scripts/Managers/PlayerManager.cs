using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, ISaveManager
{
    public static PlayerManager instance;
    
    public Player player;

    public int currency; 
    public int playerProgress; 
    public int playerLevel;

    public int expForPlayerToLevelUp;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }        
    }

    private void Start()
    {        
        if(player != null)
        {
            if (playerLevel == 0)
            {
                playerLevel = 1;
            }
        }        

        if (player != null)
        {                        
            Invoke("SetFirstCurrentHealth", 0.5f);
        }
    }

    private void Update()
    {        
        if (currency >= 1000000)
        {
            currency = 1000000; 
        }        
    }    

    public bool HaveEnoughMoney(int _price)
    {
        if (_price > currency)
        {
            Debug.Log("Not Enough currency");
            return false;
        }

        currency -= _price;
        return true;
    }

    public int GetCurrency()
    {
        return currency;
    }

    public void LoadData(GameData _data)
    {
        this.currency = _data.currency;
        this.playerProgress = _data.playerProgress;
        this.playerLevel = _data.playerLevel;
    }

    public void SaveData(ref GameData _data)
    {
        _data.currency = this.currency;
        _data.playerProgress = this.playerProgress;
        _data.playerLevel = this.playerLevel;
    }
    
    public void MakePlayerInvincible()
    {
        if (player != null)
        {
            player.stats.MakeInvincible(true);
        }
    }

    public void MakePlayerNotInvincible()
    {
        if (player != null)
        {
            player.stats.MakeInvincible(false);
        }
    }    

    private void SetFirstCurrentHealth()
    {
        player.stats.currentHealth = player.stats.GetMaxHealthValue();
    }
}
