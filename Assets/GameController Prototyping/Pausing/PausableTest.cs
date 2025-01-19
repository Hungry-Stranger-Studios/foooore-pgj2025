using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausableTest : MonoBehaviour, IPausable
{
    //OnEnable is called right when the object is activated
    //It causes the PauseManager's pausing method to also call your custom pause message
    private void OnEnable()
    {
        PauseManager.Instance.onPauseGame += Pause;
        PauseManager.Instance.onUnpauseGame += Unpause;
    }
    //Similar to OnEnable except with deregistering these events
    //If not implemented, these methods will never leave PauseManager's delegates, leading to errors
    private void OnDisable()
    {
        PauseManager.Instance.onPauseGame -= Pause;
        PauseManager.Instance.onUnpauseGame -= Unpause;
    }
    //These are custom pause and unpause methods where you can implement your own logic
    public void Pause()
    {
        Debug.Log("I Paused!");
    }

    public void Unpause()
    {
        Debug.Log("I Unpaused!");
    }
}
