using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialMedia : MonoBehaviour
{
    public void OpenInstagram()
    {
        AudioManager.instance.PlaySFXUI(1); //button sound
        string url = "http://instagram.com/afr.developer";
        Application.OpenURL(url);
    }

    public void OpenYoutube()
    {
        AudioManager.instance.PlaySFXUI(1); //button sound
        string url = "https://www.youtube.com/channel/UCnkQ74pS1kS6sUljzBvcwPg";
        Application.OpenURL(url);
    }

    public void OpenItchIo()
    {
        AudioManager.instance.PlaySFXUI(1); //button sound
        string url = "https://afr-developer.itch.io/";
        Application.OpenURL(url);
    }

    public void OpenForms()
    {
        AudioManager.instance.PlaySFXUI(1); //button sound
        string url = "https://forms.gle/NZyRNSwXo9wiTNs6A";
        Application.OpenURL(url);
    }

    public void OpenLinkedIn()
    {
        AudioManager.instance.PlaySFXUI(1); //button sound
        string url = "https://www.linkedin.com/in/alfin-syaghaf-rifai-367262229/";
        Application.OpenURL(url);
    }
}
