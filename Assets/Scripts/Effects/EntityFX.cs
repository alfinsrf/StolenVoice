using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    protected Player player;    
    protected SpriteRenderer sr;

    [Header("Pop Up Text")]
    [SerializeField] private GameObject popUpTextPrefab;

    [Header("Flash FX")]
    [SerializeField] private float flashDuration;
    [SerializeField] private Material hitMat;
    private Material originalMat;    

    [Header("Hit FX")]
    [SerializeField] private GameObject hitFx;
    [SerializeField] private GameObject criticalHitFx;

    private GameObject myHealthBar;

    protected virtual void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        player = PlayerManager.instance.player;

        originalMat = sr.material;

        if (GetComponentInChildren<UI_HealthBar>() != null)
        {
            myHealthBar = GetComponentInChildren<UI_HealthBar>(true).gameObject;
        }
    }

    public void CreatePopUpText(string _text, Color _newColor, float _lifetime)
    {
        if (popUpTextPrefab != null)
        {
            float randomX = Random.Range(-1, 1);
            float randomY = Random.Range(1.5f, 2);

            Vector3 positionOffset = new Vector3(randomX, randomY, 0);

            GameObject newText = Instantiate(popUpTextPrefab, transform.position + positionOffset, Quaternion.identity);

            newText.GetComponent<TextMeshPro>().text = _text;
            newText.GetComponent<TextMeshPro>().color = _newColor;
            newText.GetComponent<PopUpTextFX>().lifeTime = _lifetime;
        }
    }

    public void MakeTransparent(bool _transparent)
    {
        if (_transparent)
        {
            if (myHealthBar != null)
            {
                myHealthBar.SetActive(false);
            }
            sr.color = Color.clear;
        }
        else
        {
            if (myHealthBar != null)
            {
                myHealthBar.SetActive(true);
            }
            sr.color = Color.white;
        }
    }

    public void MakeHealthBarTransparent(bool _transparent)
    {
        if (_transparent)
        {
            if(myHealthBar != null)
            {
                myHealthBar.SetActive(false);
            }
        }
        else
        {
            if (myHealthBar != null)
            {
                myHealthBar.SetActive(true);
            }
        }
    }

    private IEnumerator FlashFX()
    {
        sr.material = hitMat;
        Color currentColor = sr.color;
        sr.color = Color.white;

        yield return new WaitForSeconds(flashDuration);

        sr.color = currentColor;
        sr.material = originalMat;
    }
    
    private void RedColorBlink()
    {
        if (sr.color != Color.white)
        {
            sr.color = Color.white;
        }
        else
        {
            sr.color = Color.red;
        }
    }

    private void CancelColorChange()
    {
        CancelInvoke();
        sr.color = Color.white;        
    }    

    public void CreateHitFX(Transform _target, bool _critical)
    {
        float zRotation = Random.Range(-90, 90);
        float xPosition = Random.Range(-.25f, .25f);
        float yPosition = Random.Range(-.25f, .25f);

        Vector3 hitFxRotation = new Vector3(0, 0, zRotation);

        GameObject hitPrefab = hitFx;

        if (_critical)
        {
            hitPrefab = criticalHitFx;

            float yRotation = 0;
            zRotation = Random.Range(-45, 45);

            if (GetComponent<Entity>().facingDir == -1)
            {
                yRotation = 180;
            }

            hitFxRotation = new Vector3(0, yRotation, zRotation);
        }

        GameObject newHitFx = Instantiate(hitPrefab, _target.position + new Vector3(xPosition, yPosition), Quaternion.identity); // add , target

        newHitFx.transform.Rotate(hitFxRotation);

        Destroy(newHitFx, 0.5f);
    }
}
