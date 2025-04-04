using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseManager : MonoBehaviour
{
    //This class will be responsible for calling the methods related to pausing the game
    
    //Pausing game assets
    public delegate void PauseGame();
    public event PauseGame onPauseGame;
    //Unpausing game assets
    public delegate void UnpauseGame();
    public event PauseGame onUnpauseGame;
    //In-Class Data members
    private bool gamePaused = false; //tracking game state
    public PauseMenu pauseMenu; //manipulating the pause menu
    
    private void Awake()
    {
        pauseMenu = GameObject.Find("PauseMenu").GetComponent<PauseMenu>(); //PauseMenu is the only item with this component
        GameManager.Instance.SetPauseManager(this);
    }
    private void OnEnable()
    {
        pauseMenu.OnContinue += Unpause;
    }

    private void Start()
    {
        
        pauseMenu.gameObject.SetActive(false);
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
        onPauseGame?.Invoke();
        gamePaused = true;
        pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0.001f;
    }
    private void Unpause()
    {
        onUnpauseGame?.Invoke();
        gamePaused = false;
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }
    
}
