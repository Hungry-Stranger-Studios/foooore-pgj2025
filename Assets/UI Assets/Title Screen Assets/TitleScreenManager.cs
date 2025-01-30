using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class TitleScreenManager : MonoBehaviour
{
    private UIDocument _titleScreen;
    //For unpausing from the continue button
    [SerializeField] private int nextSceneID = 1;
    [SerializeField] private float transitionDuration = 1;
    [SerializeField] private float transitionWaitTime = 1;

    //Things to do when the title screen loads
    private void OnEnable()
    {
        _initScreen();
        // Ensure timescale is set to 1 and cursor is not locked
        Time.timeScale = 1.0f;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
    }

    //things to do when the title screen is being unloaded
    private void OnDisable()
    {

    }
    // Function to add functionality to titlescreen buttons
    private void _initScreen()
    {
        //Retrieve UI Elements
        _titleScreen = GetComponent<UIDocument>();
        var root = _titleScreen.rootVisualElement;
        //Provide functions that will listen to events
        root.Q<Button>("StartButton").clicked += OnStartClicked;
    }

    private void OnStartClicked()
    {
        GameManager.Instance.changeScene(nextSceneID, transitionDuration, transitionWaitTime);
    }

}
