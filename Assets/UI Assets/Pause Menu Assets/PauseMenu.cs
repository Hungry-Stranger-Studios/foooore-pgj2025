using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    private UIDocument _pauseMenu;
    //For unpausing from the continue button
    public delegate void PauseMenuDelegate();
    public event PauseMenuDelegate OnContinue;
    public event PauseMenuDelegate OnQuit;

    private int _titleScreenID = 0;
    
    private void OnEnable()
    {
        //Retrieve UI Elements
        _pauseMenu = GetComponent<UIDocument>();
        var root = _pauseMenu.rootVisualElement;
        //Provide functions that will listen to events
        root.Q<Button>("ContinueButton").clicked += OnContinueClicked;
        root.Q<Button>("RetryButton").clicked += OnRetryClicked;
        root.Q<Button>("QuitButton").clicked += OnQuitClicked;
    }

    private void OnContinueClicked()
    {
        //alternate method of if(OnContinue!=null){OnContinue.Invoke();}
        OnContinue?.Invoke();
    }
    private void OnRetryClicked()
    {
        Time.timeScale = 1.0f;
        GameManager.Instance.reloadScene();
        OnContinue?.Invoke();
    }
    private void OnQuitClicked()
    {
        OnQuit?.Invoke();
        GameManager.Instance.changeScene(_titleScreenID, 0.5f, 0f);
        Time.timeScale = 1.0f;

    }
}
