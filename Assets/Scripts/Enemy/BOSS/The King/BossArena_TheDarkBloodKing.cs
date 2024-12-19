using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArena_TheDarkBloodKing : MonoBehaviour
{
    public Enemy_TheKing enemy;

    [SerializeField] private BoxCollider2D boxCollider;

    public GameObject leftBarrier;
    public GameObject rightBarrier;

    //BGM
    [SerializeField] private int bgmToPlay;

    [SerializeField] private TriggerDialog bossDialogTrigger;
    private bool canStartBossFight = false;
    private bool playerChallengeBoss = false;
    private bool oneTimeChallenge = true;

    private void Start()
    {        
        bossDialogTrigger = GetComponentInChildren<TriggerDialog>();

        leftBarrier.SetActive(false);
        rightBarrier.SetActive(false);
    }

    private void Update()
    {
        if (enemy == null)
        {
            AudioManager.instance.PlayBGM(0);
            Destroy(gameObject, 3);
        }

        if (bossDialogTrigger == null)
        {
            canStartBossFight = true;
            playerChallengeBoss = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            if (canStartBossFight)
            {
                SetupBossAndArena();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            if (canStartBossFight && playerChallengeBoss && oneTimeChallenge)
            {
                SetupBossAndArena();

                playerChallengeBoss = false;
                oneTimeChallenge = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            enemy.bossFightBegun = false;
            Debug.Log("Player leave the boss arena");

            if (leftBarrier != null)
            {
                leftBarrier.SetActive(false);
            }

            if (rightBarrier != null)
            {
                rightBarrier.SetActive(false);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center, boxCollider.bounds.size);
    }

    private void SetupBossAndArena()
    {
        Debug.Log("Boss Fight!");
        enemy.bossFightBegun = true;

        AudioManager.instance.PlayBGM(bgmToPlay); //play bgm boss battle 

        if (leftBarrier != null)
        {
            leftBarrier.SetActive(true);
        }

        if (rightBarrier != null)
        {
            rightBarrier.SetActive(true);
        }
    }
}
