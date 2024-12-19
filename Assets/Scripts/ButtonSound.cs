using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public void ButtonClickSound()
    {
        AudioManager.instance.PlaySFXUI(0);
    }
}
