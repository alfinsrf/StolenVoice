using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialog : MonoBehaviour
{
    public DialogData dialogData;

    private bool dialogTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null && !dialogTriggered)
        {
            dialogTriggered = true;
            DialogManager.instance.StartDialog(dialogData);
            DialogManager.instance.OnDialogEnd += DestroyTrigger;
        }
    }

    private void DestroyTrigger()
    {
        DialogManager.instance.OnDialogEnd -= DestroyTrigger;
        Destroy(gameObject, 0.25f);
    }
}
