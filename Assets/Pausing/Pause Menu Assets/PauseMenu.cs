using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UIElements;


public class PauseMenu : MonoBehaviour
{
    private UIDocument _pauseMenu;
    //For unpausing from the continue button
    public delegate void PauseMenuDelegate();
    public event PauseMenuDelegate OnContinue;
    
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
        SceneController.ReloadScene(0.5f, 0.5f);
        OnContinue?.Invoke();
    }
    private void OnQuitClicked()
    {
        Time.timeScale = 1.0f;
        SceneController.LoadScene(0, 1, 1);       
    }
}
