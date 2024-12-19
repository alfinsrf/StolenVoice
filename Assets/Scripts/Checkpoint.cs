using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator anim;

    public string id;
    [Space]
    public bool activationStatus; 
    [Space]
    public bool interactionWithPlayer;
    public GameObject buttonE;
    public bool canInteract = false;    
        
    [Header("Area sound & SFX")]
    public int activateSound;    
    public GameObject areaSound;

    [Header("Campfire Light")]
    [SerializeField] private GameObject campfireLight;

    void Start()
    {
        anim = GetComponent<Animator>();

        //deactive object 
        if(buttonE != null)
        {
            buttonE.SetActive(false);
        }        

        if(areaSound != null)
        {
            areaSound.SetActive(false);
        }

        if(campfireLight != null)
        {
            campfireLight.SetActive(false);
        }
    }

    [ContextMenu("Generate checkpoint id")]
    private void GenerateId()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private void Update()
    {       
        if (activationStatus == true)
        {
            areaSound.SetActive(true);
            campfireLight.SetActive(true);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            ActivateCheckpoint();

            if(interactionWithPlayer == true)
            {
                if(buttonE != null)
                {
                    buttonE.SetActive(true);
                }

                canInteract = true;                
            }            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            if(interactionWithPlayer == true)
            {
                if (buttonE != null)
                {
                    buttonE.SetActive(false);
                }

                canInteract = false;                
            }

            AudioManager.instance.StopSFXWithTime(activateSound);
        }
    }

    public void ActivateCheckpoint()
    {
        if (activationStatus == false)
        {
            AudioManager.instance.PlaySFX(activateSound, transform); //sound campfire ketika baru di nyalakan

            PlayerManager.instance.player.fx.CreatePopUpText("Checkpoint Saved", Color.white, 2);
            PlayerManager.instance.player.stats.IncreaseHealthBy(1000);
        }

        activationStatus = true;
        anim.SetBool("active", true);
    }
}
