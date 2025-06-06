using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Snorx.Data;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth = 20;
    
    public void changeHealth(int amount)
    {
        currentHealth += amount;
        if(currentHealth <= 0f)
        {
            gameObject.SetActive(false);
        }
    }
}
