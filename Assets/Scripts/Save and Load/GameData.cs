using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int currency;
    public int playerProgress;
    public int playerLevel;           

    public SerializableDictionary<string, bool> checkpoints;
    public string closestCheckpointId;    

    public GameData()
    {       
        this.currency = 0;
        this.playerProgress = 0;
        this.playerLevel = 0;        

        closestCheckpointId = string.Empty;
        checkpoints = new SerializableDictionary<string, bool>();
    }
}
