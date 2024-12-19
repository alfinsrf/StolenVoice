using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

    public GameObject dialogUI;
    public TextMeshProUGUI dialogText;
    public TextMeshProUGUI nameText;
    public float typingSpeed = 0.02f;
    
    private Queue<DialogData.DialogLine> dialogLines;
    public bool isDialogActive = false;

    public event Action OnDialogEnd;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        dialogLines = new Queue<DialogData.DialogLine>();        
    }

    public void StartDialog(DialogData dialogData)
    {
        isDialogActive = true;
        dialogUI.SetActive(true);        
        
        dialogLines.Clear();
        
        foreach(DialogData.DialogLine line in dialogData.dialogLines)
        {
            dialogLines.Enqueue(line);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {        
        if (dialogLines.Count == 0)
        {
            EndDialog();
            return;
        }
        
        DialogData.DialogLine line = dialogLines.Dequeue();
        nameText.text = line.speaker;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(line.sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void EndDialog()
    {
        isDialogActive = false;
        dialogUI.SetActive(false);

        OnDialogEnd?.Invoke();
    }    

    // Update is called once per frame
    void Update()
    {        
        if(isDialogActive && Input.GetKeyDown(KeyCode.Return) && Time.timeScale == 1)
        {
            DisplayNextSentence();
        }   
    }
}
