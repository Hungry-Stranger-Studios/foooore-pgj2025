using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class TitleScreenManager : MonoBehaviour
{
    private UIDocument _titleScreen;
    //For unpausing from the continue button


    private void OnEnable()
    {
        //Retrieve UI Elements
        _titleScreen = GetComponent<UIDocument>();
        var root = _titleScreen.rootVisualElement;
        //Provide functions that will listen to events
        root.Query<Button>("StartButton").First().RegisterCallback<ClickEvent>(OnStartClicked);
    }

    private void OnStartClicked(ClickEvent evt)
    {
        if (evt.propagationPhase != PropagationPhase.AtTarget)
            return;
        SceneController.LoadScene(1, 1, 1);
    }

}
