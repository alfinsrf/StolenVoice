using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeChapterTrigger : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [Space]
    [SerializeField] private int progressForPlayer;

    private bool isPlayerInRange = false;
    public GameObject buttonE;
    [Space]
    [SerializeField] private UI_DarkScreen darkScreen;

    // Start is called before the first frame update
    void Start()
    {
        buttonE.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            PlayerManager.instance.playerProgress = progressForPlayer;
            SaveManager.instance.SaveGame();

            StartCoroutine(LoadSceneWithFadeEffect(sceneName, 4f));
            isPlayerInRange = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            isPlayerInRange = true;
            buttonE.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            isPlayerInRange = false;
            buttonE.SetActive(false);
        }
    }

    IEnumerator LoadSceneWithFadeEffect(string _sceneName, float _delay)
    {
        sceneName = _sceneName;
        darkScreen.FadeOut();

        yield return new WaitForSeconds(_delay);

        SceneManager.LoadScene(sceneName);
    }
}
