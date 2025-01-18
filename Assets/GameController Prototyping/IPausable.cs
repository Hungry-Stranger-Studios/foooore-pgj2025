using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPausable
{
    void Pause();
    void Unpause();
    void Enable();
    void Disable();
}
