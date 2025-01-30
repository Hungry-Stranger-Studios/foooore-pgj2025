using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelSelectController : MonoBehaviour
{
    private UIDocument _pauseMenu;

    [Header("Scene IDs")]
    [SerializeField] private int levelOneID;
    [SerializeField] private int levelTwoID;
    [SerializeField] private int levelThreeID;

    [Header("Scene Transition Values")]
    [SerializeField] private float sceneTransitionTime;
    [SerializeField] private float sceneWaitTime;

    private void OnEnable()
    {
        //Retrieve UI Elements
        _pauseMenu = GetComponent<UIDocument>();
        var root = _pauseMenu.rootVisualElement;
        //Provide functions that will listen to events
        root.Query<VisualElement>("level-one-container").Children<Button>("level-start").First().clicked += buttonOne;
        root.Query<VisualElement>("level-two-container").Children<Button>("level-start").First().clicked += buttonTwo;
        root.Query<VisualElement>("level-three-container").Children<Button>("level-start").First().clicked += buttonThree;
    }
    /*
     * Yuck yuck yucky code
     * But it works (poorly)
     */
    private void buttonOne()
    {
        GameManager.Instance.changeScene(levelOneID, sceneTransitionTime, sceneWaitTime);
    }                                                
    private void buttonTwo()
    {
        GameManager.Instance.changeScene(levelTwoID, sceneTransitionTime, sceneWaitTime);
    }
    private void buttonThree() 
    {
        GameManager.Instance.changeScene(levelThreeID, sceneTransitionTime, sceneWaitTime);
    }
}
