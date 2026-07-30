using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LetMeLeaveNowPlease : MonoBehaviour
{
    public enum Scene
    {
        PermaLoaded,
        MainMenu,
        Tutorial,
        Town,
        WiltedForest
    }
    public Scene sceneToLoad;

    void Start()
    {
        SceneManager.LoadScene((int)sceneToLoad);
    }
}
