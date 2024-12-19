using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Credit : MonoBehaviour
{
    [SerializeField] private UI_DarkScreen darkScreen;
    [SerializeField] private RectTransform rectTransformCredit;
    [SerializeField] private float scrollSpeed = 200;
    [SerializeField] private float offScreenPosition;

    private string sceneName = "MainMenu";
    private bool creditsSkipped;

    private void Awake()
    {
        darkScreen.gameObject.SetActive(true);
    }

    private void Start()
    {
        AudioManager.instance.PlayBGM(0); //play music credit
    }

    // Update is called once per frame
    void Update()
    {
        if (rectTransformCredit.anchoredPosition.y < offScreenPosition)
        {
            rectTransformCredit.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
        }

        if (rectTransformCredit.anchoredPosition.y > offScreenPosition)
        {
            BackhToMainMenu();
        }
    }

    public void SkipCredit()
    {
        if (creditsSkipped == false)
        {
            scrollSpeed += 10;
            creditsSkipped = true;
        }
        else
        {
            Invoke("BackhToMainMenu", 5f);            
        }
    }

    private void BackhToMainMenu()
    {
        StartCoroutine(LoadSceneWithFadeEffect("MainMenu", 3f));
    }

    IEnumerator LoadSceneWithFadeEffect(string _sceneName, float _delay)
    {
        sceneName = _sceneName;
        darkScreen.FadeOut();

        yield return new WaitForSeconds(_delay);

        SceneManager.LoadScene(sceneName);
    }
}
