using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{   
    public UnityEvent died;
    public int maxHealth;
    private int currentHealth;

    public HealthUI healthUI;

    private bool hasBeenLoaded;

    void Start()
    {
        hasBeenLoaded = false;
        SceneManager.sceneLoaded += Initialize;
        died.AddListener(ResetHearts);
    }

    void Initialize(Scene loadedScene, LoadSceneMode whatIsThis)
    {
        if(loadedScene.buildIndex > 1 && !hasBeenLoaded) // not perma load not main menu
        {
            ResetHearts();
            hasBeenLoaded = true;
        }


    }

    void ResetHearts()
    {
            maxHealth = 5;
            currentHealth = maxHealth;
            healthUI.SetMaxHearts(currentHealth);
    }

    void Update()
    {
        
    }

    public void BePoked(int damage)
    {
        currentHealth -= damage;
        healthUI.UpdateHearts(currentHealth);

        if(currentHealth <= 0)
        {
            died.Invoke();
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
