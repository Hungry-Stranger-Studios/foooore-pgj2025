using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
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

    private bool gamePaused;
    
    private void OnKeyDown(KeyCode keyCode)
    {
        if(keyCode != KeyCode.Escape)
        {
            return;
        }
        if(!gamePaused)
        {
            onPauseGame.Invoke();
            gamePaused = true;
        }
        else
        {
            onUnpauseGame.Invoke();
            gamePaused = false;
        }
    }
}
