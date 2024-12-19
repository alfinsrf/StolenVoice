using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UI_Prologue : MonoBehaviour
{
    private string sceneName;

    [Header("Dark Screen Transition")]
    [SerializeField] private UI_DarkScreen darkScreen;
    [Space]
    [SerializeField] private GameObject blackBGUI;
    [Space]
    [SerializeField] private GameObject skipProloguePanel;
    [SerializeField] private GameObject skipText;

    [Header("Conditional")]
    private bool inTheEndOfPrologue;
    private bool wantToSkip;
    private bool canSkip;

    [Header("Phase 1")]
    [SerializeField] private TextMeshProUGUI text1;
    [SerializeField] private Animator animText1;

    [Header("Phase 2")]
    [SerializeField] private TextMeshProUGUI text2;
    [SerializeField] private Animator animText2;


    [Header("Backgrounds")]
    [SerializeField] private GameObject forestBG;
    [SerializeField] private GameObject cityBG;
    [Space]
    [SerializeField] private GameObject forestStaticBG;
    [SerializeField] private GameObject groundFirstLand;
    [SerializeField] private GameObject gridGround;

    [SerializeField] private GameObject bloodKing;
    [SerializeField] private Animator bloodKingAnim;
    [Space]
    [SerializeField] private GameObject infected1;
    [SerializeField] private GameObject infected2;
    [SerializeField] private GameObject infected3;
    [SerializeField] private GameObject infected4;
    [SerializeField] private GameObject infected5;    

    private void Awake()
    {
        darkScreen.gameObject.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        //For Skip Prologue
        skipProloguePanel.SetActive(false);
        skipText.SetActive(false);
        inTheEndOfPrologue = false;
        canSkip = true;

        //STARTER PHASE
        blackBGUI.SetActive(true);
        text1.gameObject.SetActive(false);
        text2.gameObject.SetActive(false);
        forestBG.SetActive(true);
        cityBG.SetActive(false);

        forestStaticBG.SetActive(false);
        groundFirstLand.SetActive(false);
        gridGround.SetActive(false);
        bloodKing.SetActive(false);
        
        infected1.SetActive(false);
        infected2.SetActive(false);
        infected3.SetActive(false);
        infected4.SetActive(false);
        infected5.SetActive(false);



        AudioManager.instance.PlayBGM(0); //bgm prologue
        StartCoroutine(PrologueCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if(inTheEndOfPrologue == true && skipProloguePanel.activeInHierarchy)
        {
            skipProloguePanel.SetActive(false);
        }

        //INPUT
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(skipText.activeInHierarchy == false && wantToSkip == false && canSkip && inTheEndOfPrologue == false)
            {
                skipText.SetActive(true);
                wantToSkip = true;
            }
            else if(skipText.activeInHierarchy && skipProloguePanel.activeInHierarchy == false && wantToSkip && canSkip && inTheEndOfPrologue == false)
            {                
                skipText.SetActive(false);
                skipProloguePanel.SetActive(true);
            }
            else if(skipProloguePanel.activeInHierarchy && canSkip && inTheEndOfPrologue == false)
            {
                SkipPrologue();                
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(skipProloguePanel.activeInHierarchy)
            {
                wantToSkip = false;
                skipText.SetActive(false);
                skipProloguePanel.SetActive(false);
            }
        }
    }

    IEnumerator PrologueCoroutine()
    {        
        yield return new WaitForSeconds(2);
        text1.gameObject.SetActive(true);

        yield return new WaitForSeconds(3);
        animText1.SetTrigger("fadeOut");

        yield return new WaitForSeconds(0.1f);
        darkScreen.FadeOut();

        yield return new WaitForSeconds(2f);
        text1.gameObject.SetActive(false);        
        blackBGUI.SetActive(false);
     
        yield return new WaitForSeconds(0.5f);
        darkScreen.FadeIn();
        
        yield return new WaitForSeconds(1f);
        text2.text = "Everyone lives in peace";
        text2.gameObject.SetActive(true);

        yield return new WaitForSeconds(6f);
        animText2.SetTrigger("fadeOut");

        yield return new WaitForSeconds(0.1f);
        darkScreen.FadeOut();

        yield return new WaitForSeconds(2f);
        forestBG.SetActive(false);
        cityBG.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        darkScreen.FadeIn();

        yield return new WaitForSeconds(1f);
        text2.text = " ";
        animText2.SetTrigger("fadeIn");

        yield return new WaitForSeconds(6f);
        animText2.SetTrigger("fadeOut");

        yield return new WaitForSeconds(0.1f);
        darkScreen.FadeOut();

        yield return new WaitForSeconds(2f);
        text1.text = "Until One Day";
        blackBGUI.SetActive(true);

        darkScreen.FadeIn();

        yield return new WaitForSeconds(0.1f);
        text1.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(3);
        animText1.SetTrigger("fadeOut");

        yield return new WaitForSeconds(2f);
        text1.text = "He came and ruined everything";
        animText1.SetTrigger("fadeIn");

        yield return new WaitForSeconds(3);
        animText1.SetTrigger("fadeOut");

        darkScreen.FadeOut();

        yield return new WaitForSeconds(2f);
        text1.gameObject.SetActive(false);
        blackBGUI.SetActive(false);
        cityBG.SetActive(false);

        forestStaticBG.SetActive(true);
        groundFirstLand.SetActive(true);
        gridGround.SetActive(true);
        bloodKing.SetActive(true);

        yield return new WaitForSeconds(1f);
        darkScreen.FadeIn();

        yield return new WaitForSeconds(1.5f);
        text2.text = "The Dark Blood King";
        animText2.SetTrigger("fadeIn");

        yield return new WaitForSeconds(3f);
        animText2.SetTrigger("fadeOut");

        yield return new WaitForSeconds(1f);
        text2.text = "He start stoling voice, mind, and then soul of human";
        animText2.SetTrigger("fadeIn");

        yield return new WaitForSeconds(4f);
        animText2.SetTrigger("fadeOut");

        yield return new WaitForSeconds(1f);
        text2.text = "After he took all of that, he turns them into monsters, then commands them like soldiers.";
        animText2.SetTrigger("fadeIn");

        yield return new WaitForSeconds(0.5f);
        bloodKingAnim.SetTrigger("usePower");

        yield return new WaitForSeconds(0.5f);
        infected1.SetActive(true);
        infected2.SetActive(true);
        infected3.SetActive(true);
        infected4.SetActive(true);
        infected5.SetActive(true);

        yield return new WaitForSeconds(4f);
        animText2.SetTrigger("fadeOut");

        yield return new WaitForSeconds(0.1f);
        darkScreen.FadeOut();

        yield return new WaitForSeconds(2f);
        text1.text = "The Blood King conquered the world in a short period of time";
        text1.gameObject.SetActive(true);
        blackBGUI.SetActive(true);
        darkScreen.FadeIn();

        yield return new WaitForSeconds(4);
        animText1.SetTrigger("fadeOut");

        yield return new WaitForSeconds(2f);
        text1.text = "Over decades and after several generations";
        animText1.SetTrigger("fadeIn");

        yield return new WaitForSeconds(3);
        animText1.SetTrigger("fadeOut");

        yield return new WaitForSeconds(2f);
        text1.text = "The world has changed completely";
        animText1.SetTrigger("fadeIn");

        yield return new WaitForSeconds(3);
        animText1.SetTrigger("fadeOut");

        yield return new WaitForSeconds(2f);
        text1.text = "At present time";
        animText1.SetTrigger("fadeIn");

        yield return new WaitForSeconds(7);
        animText1.SetTrigger("fadeOut");

        yield return new WaitForSeconds(2f);
        text1.text = "You were caught by The Dark Blood King";
        animText1.SetTrigger("fadeIn");

        yield return new WaitForSeconds(3);
        animText1.SetTrigger("fadeOut");

        yield return new WaitForSeconds(2f);
        text1.text = "Your voice has been stolen but you managed to escape, can you survive this condition?";
        animText1.SetTrigger("fadeIn");



        yield return new WaitForSeconds(5f);
        darkScreen.FadeOut();

        PlayerManager.instance.playerProgress = 1;
        SaveManager.instance.SaveGame();

        StartCoroutine(LoadSceneWithFadeEffect("ChapterOne", 2));
    }

    public void SkipPrologue()
    {
        wantToSkip = false;
        canSkip = false;

        skipText.SetActive(false);
        skipProloguePanel.SetActive(false);        

        darkScreen.FadeOut();

        PlayerManager.instance.playerProgress = 1;
        SaveManager.instance.SaveGame();

        StartCoroutine(LoadSceneWithFadeEffect("ChapterOne", 2));
    }

    public void CancelSkip()
    {
        wantToSkip = false;
        skipText.SetActive(false);
        skipProloguePanel.SetActive(false);
    }

    IEnumerator LoadSceneWithFadeEffect(string _sceneName, float _delay)
    {
        sceneName = _sceneName;
        darkScreen.FadeOut();

        yield return new WaitForSeconds(_delay);

        SceneManager.LoadScene(sceneName);
    }
}
