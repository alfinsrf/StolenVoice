using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{    
    public DialogData dialogData;

    private bool isPlayerInRange = false;
    public GameObject buttonE;

    [Header("Important Dialog")]
    public bool isImportant;
    public GameObject exclamationMark;

    // Start is called before the first frame update
    void Start()
    {
        buttonE.SetActive(false);

        if(isImportant == true)
        {
            if(exclamationMark != null)
            {
                exclamationMark.SetActive(true);
            }
        }
        else if(isImportant == false)
        {
            if(exclamationMark != null)
            {
                exclamationMark.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if(isImportant == true)
            {
                isImportant = false;
                if(exclamationMark != null)
                {
                    exclamationMark.SetActive(false);
                }
            }

            DialogManager.instance.StartDialog(dialogData);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            isPlayerInRange = true;
            buttonE.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            isPlayerInRange = false;
            buttonE.SetActive(false);
        }
    }
}
