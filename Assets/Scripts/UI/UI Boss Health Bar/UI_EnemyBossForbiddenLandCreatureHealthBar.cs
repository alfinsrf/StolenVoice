using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_EnemyBossForbiddenLandCreatureHealthBar : UI_HealthBar
{
    private Enemy_BossForbiddenLandCreature enemy;

    [Header("Game Object")]
    [SerializeField] private GameObject easeHealthBar;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject bossName;

    // Start is called before the first frame update    
    protected override void Start()
    {
        base.Start();

        enemy = GetComponentInParent<Enemy_BossForbiddenLandCreature>();

        healthBar.SetActive(false);
        easeHealthBar.SetActive(false);
        bossName.SetActive(false);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (enemy.bossFightBegun == true)
        {
            healthBar.SetActive(true);
            easeHealthBar.SetActive(true);
            bossName.SetActive(true);
        }
    }
}
