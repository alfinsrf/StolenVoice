using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{
    private Player player;
    
    [SerializeField] private PlayerStats playerStats;

    [Header("UI Gameobject")]
    [SerializeField] private GameObject skillDisplayUI;

    [Header("Health Bar")]
    [SerializeField] private GameObject HealthBar;
    [SerializeField] private Slider easeHealthSlider;
    [SerializeField] private Slider slider; //player health bar    
    private float easeSliderLerpSpeed = 0.025f;

    [Header("Sign Language")]
    [SerializeField] private GameObject paperSignLanguage;    

    [Header("Heal Item")]
    [SerializeField] private GameObject healItemGroup;
    [SerializeField] private Image healItemImage;    
    [SerializeField] private Image healItemImageCooldown;
    [SerializeField] private TextMeshProUGUI currentHealItems;

    [Header("Currency Info")]
    [SerializeField] private TextMeshProUGUI currentCurrency;
    [SerializeField] private float currencyAmount;
    [SerializeField] private float increaseRate = 100;

    [Header("Player Progress To Display")]
    [SerializeField] private int progressToDisplaySkill;

    [Header("Colors")]
    [SerializeField] private Color lockedSkillColor;

    // Start is called before the first frame update
    void Start()
    {
        if (playerStats != null)
        {
            playerStats.onHealthChanged += UpdateHealthUI;
        }        
        
        player = PlayerManager.instance.player;
        
        if(PlayerManager.instance.playerProgress < progressToDisplaySkill)
        {
            skillDisplayUI.SetActive(false);
        }        
        
        HealthBar.SetActive(false);
        if(paperSignLanguage != null)
        {
            paperSignLanguage.SetActive(false);
        }
        healItemGroup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCurrencyUI();
        UpdateHealthUI();        

        if(PlayerManager.instance.playerProgress >= 2)
        {
            HealthBar.SetActive(true);
            if (paperSignLanguage != null) paperSignLanguage.SetActive(true);
            healItemGroup.SetActive(true);
        }

        if(currentHealItems != null)
        {
            currentHealItems.text = Inventory.instance.currentHealItems.ToString();
        }                

        if (Input.GetKeyDown(KeyCode.Q) && Inventory.instance.currentHealItems > 0)
        {
            SetCooldownOf(healItemImageCooldown);
        }        
        
        CheckCooldownOf(healItemImageCooldown, Inventory.instance.healCooldown);
    }
    
    private void UpdateCurrencyUI()
    {
        if(currentCurrency != null)
        {
            if (currencyAmount < PlayerManager.instance.GetCurrency())
            {
                currencyAmount += Time.deltaTime * increaseRate;
            }
            else
            {
                currencyAmount = PlayerManager.instance.GetCurrency();
            }

            currentCurrency.text = ((int)currencyAmount).ToString();
        }        
    }

    private void UpdateHealthUI()
    {
        slider.maxValue = playerStats.GetMaxHealthValue();
        slider.value = playerStats.currentHealth;

        easeHealthSlider.maxValue = playerStats.GetMaxHealthValue();        

        if(slider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, playerStats.currentHealth, easeSliderLerpSpeed);
        }
    }    

    private void SetCooldownOf(Image _image)
    {
        if (_image.fillAmount <= 0)
        {
            _image.fillAmount = 1;
        }
    }

    private void CheckCooldownOf(Image _image, float _cooldown)
    {
        if (_image.fillAmount > 0)
        {
            _image.fillAmount -= 1 / _cooldown * Time.deltaTime;
        }
    }
}
