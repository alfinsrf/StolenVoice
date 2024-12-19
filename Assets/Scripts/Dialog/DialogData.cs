using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialog", menuName = "Dialog/DialogData")]
public class DialogData : ScriptableObject
{
    [System.Serializable]
    public class DialogLine
    {
        public string speaker;
        [TextArea(3, 10)]
        public string sentence;
    }

    public DialogLine[] dialogLines;
}
