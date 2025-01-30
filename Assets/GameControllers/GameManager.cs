using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //This is the only class that should actually be a singleton
    public static GameManager Instance { get; private set; } //this class should be a singleton
    private PauseManager pauseManager;
    private static SceneController sceneController;
    private SceneDirector sceneDirector;

    //Scene Transition Details
    private int nextSceneID;
    private float transitionDuration;
    private float transitionWaitTime;

    private void Awake()
    {
        //Singleton enforcing
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            //This object (and its children) should not be destroyed upon loading into a new scene
            Instance = this;
        }
    }
    //Will be called when an instance is loaded
    private void OnEnable()
    {
        // Finding the next scene to be loaded
        sceneDirector = GameObject.Find("SceneDirector").GetComponent<SceneDirector>();
        (int, float, float) nextScene = sceneDirector.getNextScene();
        (nextSceneID, transitionDuration, transitionWaitTime) = nextScene;
    }

    //Will be called when an instance is unloaded
    private void OnDisable()
    {

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

    public void changeScene()
    {
        sceneController?.LoadScene(nextSceneID, transitionDuration, transitionWaitTime);
    }
    public void changeScene(int sceneID, float transitionDuration, float transitionWaitTime)
    {
        sceneController?.LoadScene(sceneID, transitionDuration, transitionWaitTime);
    }

    public void reloadScene()
    {
        sceneController?.ReloadScene(0.5f, 1.0f);
    }

}
