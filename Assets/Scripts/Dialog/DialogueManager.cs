using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    [Header("UI Elements")]
    public Image portrait;
    public TMP_Text actorName;
    public TMP_Text dialogueText;
    public bool isDialogueActive;
    public Button[] buttonChoices;

    private DialogueSO dialogueSO;
    private int dialogueIndex;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
        foreach(var button in buttonChoices)
        {
            button.gameObject.SetActive(false);
        }
    }
    public void showDialog()
    {
        DialogueLine line = dialogueSO.lines[dialogueIndex];
        portrait.sprite = line.speeker.portrait;
        actorName.text = line.speeker.actorName;
        dialogueText.text = line.text;  
        dialogueIndex++;

    }
    public void startDialogue(DialogueSO newDialogueSO)
    {
        dialogueSO = newDialogueSO;
        dialogueIndex = 0;
        isDialogueActive = true;
        showDialog();
    }
    public void advanceDialogue()
    {
        if (dialogueIndex < dialogueSO.lines.Length)
        {
            showDialog();
        }
        else
        {
            showChoiceDialogue();
        }
        
    }
    private  void showChoiceDialogue()
    {
        clearOptions();
        if (dialogueSO.options.Length > 0)
        {
            for (int i = 0; i < dialogueSO.options.Length; i++)
            {
                var option = dialogueSO.options[i];
                buttonChoices[i].GetComponentInChildren<TMP_Text>().text = option.optionText;
                buttonChoices[i].gameObject.SetActive(true);
                buttonChoices[i].onClick.AddListener(() => chooseOptions(option.nextDialogue));
            }
        }
        else
        {
            buttonChoices[0].GetComponentInChildren<TMP_Text>().text = "End";
            buttonChoices[0].onClick.AddListener(endDialogue);
            buttonChoices[0].gameObject.SetActive(true);
        }
    }
    public void chooseOptions(DialogueSO dialogueSO)
    {
        if(dialogueSO == null)
        {
            endDialogue();
        }
        else
        {
            clearOptions();
            startDialogue(dialogueSO);
        }
    }
    private void endDialogue()
    {
        isDialogueActive = false;
        dialogueIndex = 0;
        dialogueSO = null;
        clearOptions();
    }
    private void clearOptions()
    {
        foreach(var button in buttonChoices)
        {
            button.gameObject.SetActive(false);
            button.onClick.RemoveAllListeners();
        }
    }
}
