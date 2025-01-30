using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneDirector : MonoBehaviour
{
    [SerializeField] private int nextSceneID;
    [SerializeField] private float transitionDuration;
    [SerializeField] private float transitionWaitTime;

    public (int, float, float) getNextScene()
    {
        if (nextSceneID.IsUnityNull() || transitionDuration.IsUnityNull() || transitionWaitTime.IsUnityNull()) {
            Debug.LogWarning("No scene details have been changed!");
        }
        return (nextSceneID, transitionDuration, transitionWaitTime);
    }
}
