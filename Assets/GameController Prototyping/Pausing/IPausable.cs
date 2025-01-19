using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public interface IPausable
{
    /*
     * This Interface when implemented will allow an object to be paused.
     * NOTE: This also requires the monobehavior methods OnEnable and OnDisable. View the script PausableTest.cs
     * To view how that behavior MUST be implemented.
     */
    public void Pause();
    public void Unpause();
}
