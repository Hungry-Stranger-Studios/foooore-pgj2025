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
    void OnTriggerEnter(Collider collider){
    if(collider.CompareTag("Player")){
        Debug.Log("Hole in one!");
        holeLight.material.color = winColor;

    }
   }
}
