using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDetection : MonoBehaviour
{
    public delegate void BallDetectionDelegate();
    public event BallDetectionDelegate onDetection;

    [SerializeField] Color winColor;
    private Renderer holeLight;

    private void Awake()
    {
        holeLight = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            Debug.Log("Hole in one!");
            holeLight.material.color = winColor;
            onDetection?.Invoke();
        }
    }
}
