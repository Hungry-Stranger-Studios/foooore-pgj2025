using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //This is the only class that should actually be a singleton
    public static GameManager Instance { get; private set; } //this class should be a singleton

    private PauseManager pauseManager;
    private static SceneController sceneController;


    private void Awake()
    {
        //Singleton enforcing
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("GM MADE");
            //This object (and its children) should not be destroyed upon loading into a new scene
            Instance = this;
        }
    }
    public PauseManager GetPauseManager()
    {
        return pauseManager;
    }
    public void SetPauseManager(PauseManager pm)
    {
        pauseManager = pm;
    }

    public SceneController GetSceneController()
    {
        return sceneController;
    }
    public void SetSceneController(SceneController sc)
    {
        sceneController = sc;
    }

    public void changeScene(int nextSceneID, float transitionDuration, float transitionWaitTime)
    {
        sceneController?.LoadScene(nextSceneID, transitionDuration, transitionWaitTime);
    }
    public void reloadScene(float transitionDuration, float transitionWaitTime)
    {
        sceneController?.ReloadScene(transitionDuration, transitionWaitTime);
    }

}
