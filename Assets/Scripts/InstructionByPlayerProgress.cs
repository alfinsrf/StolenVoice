using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InstructionByPlayerProgress : MonoBehaviour
{
    public bool canShowInstruction;

    public int checkProgressPlayer;

    public string instructionValue;
    
    [SerializeField] private TextMeshProUGUI textInstruct;
    [SerializeField] private Animator animTextInstruct;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerManager.instance.playerProgress >= checkProgressPlayer)
        {
            Destroy(gameObject, 1);
        }
        else
        {
            canShowInstruction = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null && canShowInstruction)
        {
            StartCoroutine(InstructionCoroutine());
            canShowInstruction = false;
            Destroy(gameObject, 10);
        }
    }

    IEnumerator InstructionCoroutine()
    {
        textInstruct.text = instructionValue;        
        yield return new WaitForSeconds(0.5f);        

        yield return new WaitForSeconds(1);
        textInstruct.gameObject.SetActive(true);

        yield return new WaitForSeconds(3);
        animTextInstruct.SetTrigger("fadeOut");

        yield return new WaitForSeconds(2f);
        textInstruct.gameObject.SetActive(false);
        Destroy(gameObject, 1);
    }
}
