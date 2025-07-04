using System.Collections;
using System.Collections.Generic;
using Snorx.Data;
using Snorx.Enum;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    public Player player;
    public Animator anim;
    public void attack()
    {
        player.changePlayerState(PlayerState.Attacking);
    }
    public void stopAttack()
    {

    }

}
