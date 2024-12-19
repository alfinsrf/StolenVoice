using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    protected Entity entity => GetComponentInParent<Entity>();
    protected CharacterStats myStats => GetComponentInParent<CharacterStats>();
    protected RectTransform myTransform;

    public Slider easeHealthSlider;
    public Slider slider; 
    private float lerpSpeed = 0.025f;    

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //get component
        myTransform = GetComponent<RectTransform>();        

        UpdateHealthUI();
    }

    //// Update is called once per frame
    protected virtual void Update()
    {
        UpdateHealthUI();
    }

    protected virtual void UpdateHealthUI()
    {
        slider.maxValue = myStats.GetMaxHealthValue();
        slider.value = myStats.currentHealth;

        easeHealthSlider.maxValue = myStats.GetMaxHealthValue();        

        if(slider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, myStats.currentHealth, lerpSpeed);
        }
    }

    protected virtual void OnEnable()
    {
        if (entity != null)
        {
            entity.onFlipped += FlipUI;
        }

        if (myStats != null)
        {
            myStats.onHealthChanged += UpdateHealthUI;
        }
    }

    protected virtual void OnDisable()
    {
        if (entity != null)
        {
            entity.onFlipped -= FlipUI;
        }

        if (myStats != null)
        {
            myStats.onHealthChanged -= UpdateHealthUI;
        }
    }
    
    protected virtual void FlipUI()
    {
        if(myTransform != null)
        {
            myTransform.Rotate(0, 180, 0);
        }
    }
}
