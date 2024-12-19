using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_OpeningChapter : MonoBehaviour
{
    public string sceneChapterName;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI chapterText;
    [SerializeField] private Animator animChapterText;
    [Space]
    [SerializeField] private TextMeshProUGUI chapterNameText;
    [SerializeField] private Animator animChapterNameText;

    // Start is called before the first frame update
    void Start()
    {
        chapterText.gameObject.SetActive(false);
        chapterNameText.gameObject.SetActive(false);

        StartCoroutine(OpeningChapterCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator OpeningChapterCoroutine()
    {
        yield return new WaitForSeconds(1);

        AudioManager.instance.PlayBGM(0);
        chapterText.gameObject.SetActive(true);
        chapterNameText.gameObject.SetActive(true);

        yield return new WaitForSeconds(5);
        animChapterText.SetTrigger("fadeOut");
        animChapterNameText.SetTrigger("fadeOut");

        yield return new WaitForSeconds(1.9f);

        AudioManager.instance.StopAllBGM();

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(sceneChapterName);
    }
}
