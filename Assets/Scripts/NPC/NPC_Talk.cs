using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Talk : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public Animator interactAnim;
    public DialogueSO dialogueSO;
    public bool isTalking;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }
    private void OnEnable()
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        anim.Play("Pawn_Purple_Idle");
        interactAnim.Play("OpenIcon");
    }
    private void OnDisable()
    {
        interactAnim.Play("CloseIcon");
        rb.isKinematic = false;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if (DialogueManager.instance.isDialogueActive)
            {
                DialogueManager.instance.advanceDialogue();
            }
            else
            {
                DialogueManager.instance.startDialogue(dialogueSO);
            }
        }
    }
}
