using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AreaNameDisplay : MonoBehaviour
{
    public string areaName;
    public TextMeshProUGUI areaNameText;
    public float displayDuration;
    public Animator textAnim;

    private bool canDisplay;

    // Start is called before the first frame update
    void Start()
    {
        canDisplay = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            if(canDisplay)
            {
                StartCoroutine(DisplayAreaName());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private IEnumerator DisplayAreaName()
    {
        areaNameText.text = areaName;
        areaNameText.gameObject.SetActive(true);

        canDisplay = false;

        yield return new WaitForSeconds(displayDuration);        
        textAnim.SetTrigger("fadeOut");

        yield return new WaitForSeconds(2);
        areaNameText.gameObject.SetActive(false);

        Destroy(gameObject, 3f);
    }
}
