using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseManager : MonoBehaviour
{
    //This class will be responsible for calling the methods related to pausing the game
    public static PauseManager Instance { get; private set; } //this class should be a singleton
    //Pausing game assets
    public delegate void PauseGame();
    public event PauseGame onPauseGame;
    //Unpausing game assets
    public delegate void UnpauseGame();
    public event PauseGame onUnpauseGame;
    //In-Class Data members
    private bool gamePaused = false; //tracking game state
    private GameObject pauseMenu; //manipulating the pause menu


    private void Awake()
    {
        //Singleton enforcing
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        

        //begin with the pause menu turned off
        pauseMenu = transform.GetComponentInChildren<PauseMenuManager>().gameObject; //PauseMenu is the only item with this component
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape))
        {
            return;
        }
        if (!gamePaused)
        {
            Pause();
        }
        else
        {
            Unpause();
        }
    }

    private void Pause()
    {
        if (onPauseGame != null) { onPauseGame.Invoke(); }
        gamePaused = true;
        pauseMenu.SetActive(true);
    }
    private void Unpause()
    {
        if (onUnpauseGame != null) { onUnpauseGame.Invoke(); }
        gamePaused = false;
        pauseMenu.SetActive(false);
    }
    private void OnEnable()
    {
        PauseMenuManager.Instance.OnContinue += Unpause;
    }

    private void OnDisable()
    {
        PauseMenuManager.Instance.OnContinue -= Unpause;
    }
}
