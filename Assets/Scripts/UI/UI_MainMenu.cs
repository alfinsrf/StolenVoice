using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    private string sceneName;

    [Header("Scene Name")]
    public string chapterOne;    
    [Space]
    [SerializeField] private UI_DarkScreen darkScreen;

    [Header("UI Gameobject")]
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject startScreenUI;

    [Header("Button")]
    [SerializeField] private GameObject continueButton;

    [Header("Volume Controller")]
    [SerializeField] private UI_VolumeController[] volumeController;

    private void Awake()
    {
        if (darkScreen != null)
        {
            darkScreen.gameObject.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(SaveManager.instance.HasSaveData() == false || PlayerManager.instance.playerProgress < 1)
        {
            continueButton.SetActive(false);
        }
        
        for (int i = 0; i < volumeController.Length; i++)
        {
            volumeController[i].GetComponent<UI_VolumeController>().SetupVolumeSlider();
        }

        AudioManager.instance.PlayBGM(0);
        SwitchTo(startScreenUI);
    }

    // Update is called once per frame
    void Update()
    {
        if(startScreenUI.activeInHierarchy)
        {
            if(Input.anyKeyDown)
            {
                SwitchTo(mainMenuUI);
            }
        }
    }

    public void SwitchTo(GameObject _menu)
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            bool darkScreen = transform.GetChild(i).GetComponent<UI_DarkScreen>() != null; 

            if (darkScreen == false)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        if (_menu != null)
        {
            AudioManager.instance.PlaySFXUI(1); //menu sounds
            _menu.SetActive(true);
        }        
    }

    IEnumerator LoadSceneWithFadeEffect(string _sceneName, float _delay)
    {
        sceneName = _sceneName;
        darkScreen.FadeOut();

        yield return new WaitForSeconds(_delay);

        SceneManager.LoadScene(sceneName);
    }

    public void Continue()
    {
        StartCoroutine(LoadSceneWithFadeEffect(chapterOne, 2f));        
    }

    public void NewGame()
    {
        SaveManager.instance.DeleteSavedData();

        StartCoroutine(LoadSceneWithFadeEffect("Prologue", 2f));
    }

    public void Credit()
    {
        StartCoroutine(LoadSceneWithFadeEffect("Credit", 2f));
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
