using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


public class PauseMenuManager : MonoBehaviour
{
    public static PauseMenuManager Instance { get; private set; } //this class should be a singleton

    private UIDocument _pauseMenu;
    //For unpausing from the continue button
    public delegate void PauseMenuDelegate();
    public event PauseMenuDelegate OnContinue;

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
    }
    
    private void OnEnable()
    {
        //Retrieve UI Elements
        _pauseMenu = GetComponent<UIDocument>();
        var root = _pauseMenu.rootVisualElement;
        //Provide functions that will listen to events
        root.Query<Button>("ContinueButton").First().RegisterCallback<ClickEvent>(OnContinueClicked);
        root.Query<Button>("QuitButton").First().RegisterCallback<ClickEvent>(OnQuitClicked);
    }

    private void OnContinueClicked(ClickEvent evt)
    {
        if (evt.propagationPhase != PropagationPhase.AtTarget)
            return;
        //alternate method of if(OnContinue!=null){OnContinue.Invoke();}
        OnContinue?.Invoke();
        Debug.Log("Continue");
    }

    private void OnQuitClicked(ClickEvent evt)
    {
        if (evt.propagationPhase != PropagationPhase.AtTarget)
            return;
        Debug.Log("Quit");
    }
}
