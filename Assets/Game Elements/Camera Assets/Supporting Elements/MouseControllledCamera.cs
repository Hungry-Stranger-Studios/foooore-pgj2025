using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControlledCamera : MonoBehaviour
{
    [Header("Camera Functionality Values")]
    [SerializeField] private float distance = 10f;     // Distance from the ball
    [SerializeField] private float pivotHeight = 2f;   // Pivot point height above the ball
    [SerializeField] private float heightOffset = 1.5f;// Minimum height above the floor
    [SerializeField] private float sensitivity = 0.5f; // Sensitivity for mouse movement
    [SerializeField] private float maxTilt = 120f;     // Maximum upward tilt
    [SerializeField] private float minTilt = -80f;     // Maximum downward tilt

    private Transform ball;     // Reference to the golf ball
    private Vector2 turn;       // Keeps track of mouse input for rotation

    private bool cameraEnabled = true;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        ball = GameObject.Find("Golfball").transform;
    }
    void OnEnable()
    {
        GameManager.Instance.GetPauseManager().onPauseGame += pauseCamera;
        GameManager.Instance.GetPauseManager().onUnpauseGame += unpauseCamera;
    }

    void OnDisable()
    {
        GameManager.Instance.GetPauseManager().onPauseGame   -= pauseCamera;
        GameManager.Instance.GetPauseManager().onUnpauseGame -= unpauseCamera;
    }

    void LateUpdate()
    {
        // if the camera cannot be moved dont even try
        if (!cameraEnabled)
            return;
        if (ball == null)
        {
            Debug.LogWarning("Ball reference is missing!");
            return;
        }

        turn.x += Input.GetAxis("Mouse X") * sensitivity;
        turn.y -= Input.GetAxis("Mouse Y") * sensitivity;

        // clamp vertical rotation for dramatic angles
        turn.y = Mathf.Clamp(turn.y, minTilt, maxTilt);

        Quaternion rotation = Quaternion.Euler(turn.y, turn.x, 0);

        // Shift pivot point above the ball to allow looking "under" it
        Vector3 pivotPoint = ball.position + Vector3.up * pivotHeight + (transform.right / 3);

        // offset for the camera based on rotation
        Vector3 offset = rotation * new Vector3(0, 0, -distance);

        Vector3 desiredPosition = pivotPoint + offset;

        // Ensure the camera stays above the floor dynamically, i.e. if looking way up 
        desiredPosition.y = Mathf.Max(ball.position.y + heightOffset, desiredPosition.y);

        transform.position = desiredPosition;

        // makes camera look at the pivot point
        transform.LookAt(pivotPoint);
    }

    private void pauseCamera()
    {
        cameraEnabled = false;
        Cursor.lockState = CursorLockMode.None;
    }

    private void unpauseCamera()
    {
        cameraEnabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
