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

    private void OnEnable()
    {
        //Retrieve UI Elements
        _titleScreen = GetComponent<UIDocument>();
        var root = _titleScreen.rootVisualElement;
        //Provide functions that will listen to events
        root.Q<Button>("StartButton").clicked += OnStartClicked;
        // Ensure timescale is set to 1
        Time.timeScale = 1.0f;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
    }

    private void OnStartClicked()
    {
        SceneController.LoadScene(nextSceneID, transitionDuration, transitionWaitTime);
    }

}
