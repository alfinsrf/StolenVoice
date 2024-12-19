using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SignLanguage : MonoBehaviour
{   
    private Quaternion initialRotation;

    // Start is called before the first frame update
    void Start()
    {        
        initialRotation = Quaternion.Euler(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = initialRotation;
    }    
}
