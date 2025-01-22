using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleInOne : MonoBehaviour
{
    [SerializeField] Color winColor;
   void OnTriggerEnter(Collider collider){
    if(collider.CompareTag("Player")){
        Debug.Log("Hole in one!");
        GetComponent<Renderer>().material.color = winColor;

    }
   }
}
