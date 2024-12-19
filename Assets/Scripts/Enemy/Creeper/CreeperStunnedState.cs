using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreeperStunnedState : EnemyState
{
    private Enemy_Creeper enemy;

    private string targetWord = "kill";
    private int currentIndex = 0;

    private string[] ignoredKeys = { "w", "a", "s", "d", "j", "Tab", "Escape", "Space", "LeftArrow", "RightArrow" };
    public CreeperStunnedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Creeper enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.stunDuration;

        // Sign Language
        enemy.signLanguage_K.SetActive(true);
        enemy.signLanguage_I.SetActive(true);
        enemy.signLanguage_L.SetActive(true);
        enemy.signLanguage_L2.SetActive(true);

        currentIndex = 0;

        enemy.fx.InvokeRepeating("RedColorBlink", 0, 0.1f);


    }

    public override void Exit()
    {
        base.Exit();

        enemy.fx.Invoke("CancelColorChange", 0);
    }

    public override void Update()
    {
        base.Update();

        if (currentIndex == 1) enemy.signLanguage_K.SetActive(false);
        if (currentIndex == 2) enemy.signLanguage_I.SetActive(false);
        if (currentIndex == 3) enemy.signLanguage_L.SetActive(false);
        if (currentIndex == 4) enemy.signLanguage_L2.SetActive(false);

        if (Input.anyKeyDown)
        {
            string keyPressed = Input.inputString;

            if (!string.IsNullOrEmpty(keyPressed))
            {
                if (IsIgnoredKey(keyPressed))
                {
                    return;
                }

                if (keyPressed.ToLower() == targetWord[currentIndex].ToString())
                {
                    currentIndex++;

                    if (currentIndex >= targetWord.Length)
                    {
                        stateMachine.ChangeState(enemy.deadState);
                        currentIndex = 0;
                        enemy.signLanguage_L2.SetActive(false);
                    }
                }
                else
                {
                    enemy.signLanguage_K.SetActive(false);
                    enemy.signLanguage_I.SetActive(false);
                    enemy.signLanguage_L.SetActive(false);
                    enemy.signLanguage_L2.SetActive(false);

                    currentIndex = 0;
                    enemy.stats.isDead = false;
                    enemy.stats.IncreaseHealthBy(100);
                    stateMachine.ChangeState(enemy.idleState);
                }
            }
        }

        if (stateTimer < 0)
        {           
            // Sign Language
            enemy.signLanguage_K.SetActive(false);
            enemy.signLanguage_I.SetActive(false);
            enemy.signLanguage_L.SetActive(false);
            enemy.signLanguage_L2.SetActive(false);

            currentIndex = 0;
            enemy.stats.isDead = false;
            enemy.stats.IncreaseHealthBy(150);
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    private bool IsIgnoredKey(string key)
    {
        foreach (string ignoredKey in ignoredKeys)
        {
            if (key.Equals(ignoredKey, System.StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }
        return false;
    }
}