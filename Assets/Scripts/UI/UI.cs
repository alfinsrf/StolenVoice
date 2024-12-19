using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] private UI_DarkScreen darkScreen;


    [Header("UI Gameobject")]
    [SerializeField] private GameObject inGameUI;    
    [SerializeField] private GameObject optionsUI;
    [Space]
    [SerializeField] private GameObject pauseUI;
    [Space]    
    [SerializeField] private GameObject dialogUI;
    [SerializeField] private GameObject paperSignUI;    

    [Header("Dead Screen")]
    [SerializeField] private GameObject endText;
    [SerializeField] private GameObject restartButton;    

    [Header("Volume Settings")]
    [SerializeField] private UI_VolumeController[] volumeController;

    private void Awake()
    {        
        darkScreen.gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        //volume
        for (int i = 0; i < volumeController.Length; i++)
        {
            volumeController[i].GetComponent<UI_VolumeController>().SetupVolumeSlider();
        }
        
        SwitchTo(inGameUI);        
    }

    // Update is called once per frame
    void Update()
    {              
        if (DialogManager.instance.isDialogActive == true)
        {
            inGameUI.SetActive(false);
            dialogUI.SetActive(true);
        }
        else if (DialogManager.instance.isDialogActive == false && dialogUI.activeInHierarchy)
        {
            SwitchTo(inGameUI);
        }        
        
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {                                   
            if(optionsUI.activeInHierarchy)
            {
                SwitchWithKeyTo(pauseUI);
            }
            else if(inGameUI.activeInHierarchy)
            {
                SwitchWithKeyTo(pauseUI);
            }
            else if(pauseUI.activeInHierarchy)
            {
                SwitchWithKeyTo(inGameUI);
            }
            else if(dialogUI.activeInHierarchy)
            {
                SwitchWithKeyTo(pauseUI);
            }
            else if(paperSignUI.activeInHierarchy)
            {
                SwitchWithKeyTo(inGameUI);
            }
        }

        if(Input.GetKeyDown(KeyCode.Tab) && PlayerManager.instance.playerProgress >= 2)
        {
            SwitchWithKeyTo(paperSignUI);
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
            AudioManager.instance.PlaySFXUI(1); 
            _menu.SetActive(true);
        }

        if (GameManager.instance != null)
        {
            if (_menu == inGameUI)
            {
                GameManager.instance.PauseGame(false);
            }
            else if(_menu == dialogUI && DialogManager.instance.isDialogActive) 
            {
                GameManager.instance.PauseGame(false);
            }
            else if(_menu == paperSignUI)
            {
                GameManager.instance.PauseGame(true);
            }
            else
            {
                GameManager.instance.PauseGame(true);
            }
        }
    }

    public void SwitchWithKeyTo(GameObject _menu)
    {
        if (_menu != null && _menu.activeSelf)
        {
            _menu.SetActive(false);
            CheckForInGameUI();
            return;
        }

        SwitchTo(_menu);
    }

    private void CheckForInGameUI()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf && transform.GetChild(i).GetComponent<UI_DarkScreen>() == null)
            {
                return;
            }
        }

        SwitchTo(inGameUI);
    }

    public void SwitchOnEndScreen()
    {
        darkScreen.FadeOut();
        StartCoroutine(EndScreenCoroutine());
    }

    IEnumerator EndScreenCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        if(endText != null)
        {
            endText.SetActive(true);
        }

        yield return new WaitForSeconds(5);
        RestartGameButton();        
    }

    public void RestartGameButton() => GameManager.instance.RestartScene();        

    #region Back to Main Menu
    public void BackToMainMenu()
    {
        GameManager.instance.PauseGame(false);
        PlayerManager.instance.MakePlayerInvincible();

        darkScreen.FadeOut();
        StartCoroutine(BackToMainMenuCoroutine());
    }

    IEnumerator BackToMainMenuCoroutine()
    {
        yield return new WaitForSeconds(1);
        SaveManager.instance.SaveGame();

        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("MainMenu");
    }
    #endregion
}
