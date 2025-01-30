using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is a bit yucky bit its a game jam and we dont have time damnit!!
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
        if (HoleController != null)
        {
            HoleController.onWin += winGame;
        }
    }
    private void OnDisable()
    {
        if (HoleController != null)
        {
            HoleController.onWin -= winGame;
        }
    }

    private void winGame()
    {
        GameManager.Instance.changeScene(nextSceneID, transitionDuration, transitionWaitTime);
    }
}
