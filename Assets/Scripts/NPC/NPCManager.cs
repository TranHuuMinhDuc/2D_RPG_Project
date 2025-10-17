using System.Collections;
using System.Collections.Generic;
using Snorx.Enum;
using UnityEngine;


public class NPCManager : MonoBehaviour
{  
    public NPCState currentState;
    public NPC_Wander wander;
    public NPC_Talk talk;

    private void Start()
    {
        SwitchState(NPCState.Wander);
    }
    public void SwitchState(NPCState newState)
    {
        currentState = newState;
        switch (newState)
        {
            case NPCState.Wander:
                wander. enabled = true;
                talk.enabled = false;
                break;
            case NPCState.Talk:
                talk.enabled = true;
                wander.enabled = false;
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            SwitchState(NPCState.Talk);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SwitchState(NPCState.Wander);
        }
    }
}
