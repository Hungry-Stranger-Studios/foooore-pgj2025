using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// My idea with this super superclass is that it will handle the games state
/// Many different managers will send events directly to this class. This class with then send events
/// to the classes that are effected by those events.
/// This way, not every class needs to be a singleton. We are able to create a bit more
/// encapsulation
/// </summary>
public class GameStateManager : MonoBehaviour
{
    [Header("Level Elements")]
    [SerializeField] private HoleController HoleController;

    [Header("Scene Transition Properties")]
    [SerializeField] private int nextSceneID = 1;
    [SerializeField] private float transitionDuration = 1;
    [SerializeField] private float transitionWaitTime = 1;


    public static GameStateManager Instance { get; private set; }
    //delegate for a win con
    public delegate void winConditionDelegate();
    public event winConditionDelegate onWin;

    private void OnEnable()
    {
        HoleController.onWin += winGame;
    }
    private void OnDisable()
    {
        HoleController.onWin -= winGame;
    }

    private void winGame()
    {
        onWin?.Invoke();
        GameManager.Instance.GetSceneController().LoadScene(nextSceneID, transitionDuration, transitionWaitTime);
    }
}
