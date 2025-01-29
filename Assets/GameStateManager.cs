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
    [SerializeField] private BallDetection BallDetection;
    [SerializeField] private PauseMenu PauseMenuManager;
    [SerializeField] private PauseManager PauseManager;

    
    public static GameStateManager Instance { get; private set; }
    //delegate for a win con
    public delegate void winConditionDelegate();
    public event winConditionDelegate onWin;

}
