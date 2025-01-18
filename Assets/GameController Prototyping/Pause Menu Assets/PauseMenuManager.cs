using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


public class PauseMenuManager : MonoBehaviour
{
    private UIDocument _pauseMenu;

    private void Awake()
    {
        _pauseMenu = GetComponent<UIDocument>();
        var root = _pauseMenu.rootVisualElement;

        root.Query<Button>("ContinueButton").First().RegisterCallback<ClickEvent>(OnContinueClicked);
        root.Query<Button>("QuitButton").First().RegisterCallback<ClickEvent>(OnQuitClicked);
    }

    private void OnContinueClicked(ClickEvent evt)
    {
        ;
    }

    private void OnQuitClicked(ClickEvent evt)
    {
        ;
    }
}
