using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseManager : MonoBehaviour
{
    //This class will be responsible for calling the methods related to pausing the game
    public static PauseManager instance; //this class should be a singleton
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
        if(!Input.GetKeyDown(KeyCode.Escape))
        {
            return;
        }
        if(!gamePaused)
        {
            onPauseGame.Invoke();
            gamePaused = true;
            pauseMenu.SetActive(false);
        }
        else
        {
            //onUnpauseGame.Invoke();
            gamePaused = false;
            pauseMenu.SetActive(true);
        }
    }

    private void Awake()
    {
        //begin with the pause menu turned off
        pauseMenu = transform.GetChild(0).gameObject;
        pauseMenu.SetActive(false);
    }
}
