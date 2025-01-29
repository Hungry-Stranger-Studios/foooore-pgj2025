using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    public delegate void Win();
    public event Win onWin;

    [SerializeField] Color winColor;    //upon the ball hitting the hole, its beacon will change to this color

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponentInChildren<Renderer>().material.color = winColor;
            onWin?.Invoke();
        }
    }
}
