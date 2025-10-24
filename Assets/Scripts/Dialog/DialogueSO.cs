using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/Dialog/DialogNode")]
public class DialogueSO : ScriptableObject
{
    public DialogueLine[] lines;
    public DialogueOptions[] options;
}
[System.Serializable]
public class DialogueLine
{
    public ActorSO speeker;
    [TextArea] public string text;
}
[System.Serializable]
public class DialogueOptions
{
    public string optionText;
    public DialogueSO nextDialogue;
}

