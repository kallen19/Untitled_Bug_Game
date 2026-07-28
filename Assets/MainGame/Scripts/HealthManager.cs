using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;

    public HealthUI healthUI;

    void Start()
    {
        maxHealth = 5;
        currentHealth = maxHealth;
        healthUI.SetMaxHearts(currentHealth);
    }

    public void BePoked(int damage)
    {
        currentHealth -= damage;
        healthUI.UpdateHearts(currentHealth);

        if(currentHealth <= 0)
        {
            // die
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void BeHealed(int healthIncrease)
    {
        BeHealed(healthIncrease, false);
    }

    public void BeHealed(int healthIncrease, bool canIncreaseMax)
    {
        if(currentHealth + healthIncrease > maxHealth && canIncreaseMax)
        {
            maxHealth = currentHealth = currentHealth + healthIncrease;
            healthUI.SetMaxHearts(currentHealth);
        } 
        else
        {
            currentHealth = Math.Min(maxHealth, currentHealth + healthIncrease);
        }
        
        healthUI.UpdateHearts(currentHealth);
    }
}
