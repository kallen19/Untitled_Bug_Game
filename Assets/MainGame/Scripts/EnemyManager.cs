using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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
    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        EventSetup();
        currentLevel = Level.Starter;
    }

    void OnSceneLoaded(Scene sceneLoaded, LoadSceneMode what)
    {
        if(sceneLoaded.name != "PermanentlyLoadedScene")
        {
            
        PopulateDictionaries();
        }
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
        if(!walls.ContainsKey(currentLevel))
        {
                    walls.Add(currentLevel, GameObject.FindWithTag("DropWhenCleared"));
                            enemies.Add(currentLevel, new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy")));


        }


        Debug.Log("walls " + (walls[currentLevel] != null ? "yeh" : "no") + " enemies " + (enemies[currentLevel].Count > 0 ? "yes" : "no"));
    }

    
}
