using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;             

    [Header("Heal Item")]
    public int maxHealItems; 
    public int currentHealItems;
    public float healPercent; 
    public float cooldownUseItemHeal; 

    [Header("items cooldown")]
    private float lastTimeUsedHeal;    

    public float healCooldown { get; private set; }    

    [Header("Check Progress Player")]
    [SerializeField] private int checkProgressPlayerCanStartUseHeal;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {        
        Invoke("SetFirstHealItems", 0.5f);
    }

    private void Update()
    {        
     
    }

    private void SetFirstHealItems()
    {
        currentHealItems = maxHealItems;
    }    

    public void UseHealItem()
    {        
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
     
        if(PlayerManager.instance.playerProgress < checkProgressPlayerCanStartUseHeal)
        {
            return;
        }

        if (currentHealItems <= 0)
        {
            return;
        }

        bool canUseFlask = Time.time > lastTimeUsedHeal + healCooldown;

        if (canUseFlask)
        {
            currentHealItems--;
            healCooldown = cooldownUseItemHeal;

            int healAmount = Mathf.RoundToInt(playerStats.GetMaxHealthValue() * healPercent);
            playerStats.IncreaseHealthBy(healAmount);
            Debug.Log("heal Amount: " + healAmount);
            AudioManager.instance.PlaySFX(10, null);

            lastTimeUsedHeal = Time.time;            
        }
        else
        {
            Debug.Log("Heal item on cooldown");
        }
    }    
}
