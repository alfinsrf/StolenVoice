using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ISaveManager
{
    public static GameManager instance;

    private Transform player;
    
    [SerializeField] private Checkpoint[] checkpoints;
    [SerializeField] private string closestCheckpointId;    

    [Header("Starter Opening")]
    public DialogData dialogOpening;
    [SerializeField] private TextMeshProUGUI textMoveInstruct;
    [SerializeField] private Animator animTextMoveInstruct;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        checkpoints = FindObjectsOfType<Checkpoint>();

        player = PlayerManager.instance.player.transform;
        
        textMoveInstruct.gameObject.SetActive(false);

        if(PlayerManager.instance.playerProgress <= 1)
        {
            StartCoroutine(OpeningCoroutine());
        }
    }    

    public void RestartScene()
    {
        SaveManager.instance.SaveGame();

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void LoadData(GameData _data)
    {
        StartCoroutine(LoadWithDelay(_data));
    }

    private void LoadCheckpoints(GameData _data)
    {
        foreach (KeyValuePair<string, bool> pair in _data.checkpoints)
        {
            foreach (Checkpoint checkpoint in checkpoints)
            {
                if (checkpoint.id == pair.Key && pair.Value == true)
                {
                    checkpoint.ActivateCheckpoint();
                }
            }
        }
    }    

    private IEnumerator LoadWithDelay(GameData _data)
    {
        yield return new WaitForSeconds(.1f);

        LoadCheckpoints(_data);
        LoadClosestCheckpoint(_data);        
    }

    public void SaveData(ref GameData _data)
    {                
        if (FindClosestCheckpoint() != null)
        {
            _data.closestCheckpointId = FindClosestCheckpoint().id;
        }
        _data.checkpoints.Clear();

        foreach (Checkpoint checkpoint in checkpoints)
        {
            _data.checkpoints.Add(checkpoint.id, checkpoint.activationStatus);
        }
    }
    private void LoadClosestCheckpoint(GameData _data)
    {
        if (_data.closestCheckpointId == null)
        {
            return;
        }

        closestCheckpointId = _data.closestCheckpointId;

        foreach (Checkpoint checkpoint in checkpoints)
        {
            if (closestCheckpointId == checkpoint.id)
            {
                player.position = checkpoint.transform.position;
            }
        }
    }

    private Checkpoint FindClosestCheckpoint()
    {
        float closestDistance = Mathf.Infinity;
        Checkpoint closestCheckpoint = null;

        foreach (var checkpoint in checkpoints)
        {
            float distanceToCheckpoint = Vector2.Distance(player.position, checkpoint.transform.position);

            if (distanceToCheckpoint < closestDistance && checkpoint.activationStatus == true)
            {
                closestDistance = distanceToCheckpoint;
                closestCheckpoint = checkpoint;
            }
        }

        return closestCheckpoint;
    }

    public void PauseGame(bool _pause)
    {
        if (_pause)
        {
            Time.timeScale = 0;

            //TEST
            AudioManager.instance.StopSFX(3);
            AudioManager.instance.StopSFX(5);
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    
    IEnumerator OpeningCoroutine()
    {        
        yield return new WaitForSeconds(2);
        DialogManager.instance.StartDialog(dialogOpening);
        textMoveInstruct.text = "Press A or D to move";

        yield return new WaitForSeconds(10);
        textMoveInstruct.gameObject.SetActive(true);

        yield return new WaitForSeconds(2.5f);
        animTextMoveInstruct.SetTrigger("fadeOut");
        PlayerManager.instance.player.canMove = true;

    }
}
