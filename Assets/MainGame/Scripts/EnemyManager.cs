using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Level
{
    Starter,
    Village,
    Final
}

public class EnemyManager : MonoBehaviour
{
    public UnityEvent EnemyDeath;

    public Level currentLevel;

    public Dictionary<Level, GameObject> walls = new Dictionary<Level, GameObject>();

    public Dictionary<Level, List<GameObject>> enemies = new Dictionary<Level, List<GameObject>>(); 

    public List<GameObject> enemiesLevel1;

public GameObject wallsLevel1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PopulateDictionaries();
        EventSetup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckAreaCompletion()
    {
        foreach (GameObject enemy in enemies[currentLevel])
        {
            if(enemy.activeInHierarchy)
            {
                return;
            }   
        }

        walls[currentLevel].SetActive(false);

        currentLevel ++;
    }


    void EventSetup()
    {
        if(EnemyDeath == null)
        {
            EnemyDeath = new UnityEvent();
        }

        EnemyDeath.AddListener(CheckAreaCompletion);
    }

    void PopulateDictionaries()
    {
        walls.Add(Level.Starter, wallsLevel1);

        enemies.Add(Level.Starter, enemiesLevel1);
    }

    
}
