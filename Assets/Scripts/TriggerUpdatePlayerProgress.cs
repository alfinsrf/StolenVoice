using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerUpdatePlayerProgress : MonoBehaviour
{
    public int progressForPlayer;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerManager.instance.playerProgress >= progressForPlayer)
        {
            Destroy(gameObject, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            PlayerManager.instance.playerProgress = progressForPlayer;
            SaveManager.instance.SaveGame();

            Destroy(gameObject, 2);
        }
    }
}
