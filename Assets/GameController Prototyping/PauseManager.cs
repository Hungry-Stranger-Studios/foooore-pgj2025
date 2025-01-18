using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseManager : MonoBehaviour
{
    //This class will be responsible for calling the methods related to pausing the game
    public static PauseManager Instance { get; private set; } //this class should be a singleton
    //Pausing the game
    public delegate void PauseGame();
    public event PauseGame onPauseGame;
    //Unpausing the game
    public delegate void UnpauseGame();
    public event PauseGame onUnpauseGame;

    private bool gamePaused = false;
    private GameObject pauseMenu;


    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape))
        {
            return;
        }
        if (!gamePaused)
        {
            if (onPauseGame != null) { onPauseGame.Invoke(); }
            gamePaused = true;
            pauseMenu.SetActive(false);
        }
        else
        {
            if (onUnpauseGame != null) { onUnpauseGame.Invoke(); }
            gamePaused = false;
            pauseMenu.SetActive(true);
        }
    }

    private void Awake()
    {
        //Singleton enforcing
        if(Instance != null && Instance != this)
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

}
