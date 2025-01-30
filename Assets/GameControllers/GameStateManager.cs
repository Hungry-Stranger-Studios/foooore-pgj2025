using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is a bit yucky but its a game jam and we dont have time damnit!!
/// </summary>
public class GameStateManager : MonoBehaviour
{
    [Header("Level Elements")]
    [SerializeField] private HoleController HoleController;

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
        GameManager.Instance.changeScene();
    }
}
