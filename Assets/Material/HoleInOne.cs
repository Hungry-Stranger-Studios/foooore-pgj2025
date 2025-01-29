using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleInOne : MonoBehaviour
{
    [SerializeField] Color winColor;
    private Renderer holeLight;
   
    private void Awake()
    {
        holeLight = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        //onWin += changeHoleColor;
    }
    private void OnDisable()
    {

    }
    
}
