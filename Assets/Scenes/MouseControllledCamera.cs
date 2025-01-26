using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControlledCamera : MonoBehaviour
{
    public Transform ball; // Reference to the golf ball
    public float distance = 10f; // Distance from the ball
    public float pivotHeight = 2f; // Pivot point height above the ball
    public float heightOffset = 1.5f; // Minimum height above the floor
    public float sensitivity = 0.5f; // Sensitivity for mouse movement
    public Vector2 turn; // Keeps track of mouse input for rotation
    public float maxTilt = 120f; // Maximum upward tilt
    public float minTilt = -80f; // Maximum downward tilt

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
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
        Vector3 pivotPoint = ball.position + Vector3.up * pivotHeight;

        // offset for the camera based on rotation
        Vector3 offset = rotation * new Vector3(0, 0, -distance);

        Vector3 desiredPosition = pivotPoint + offset;

        // Ensure the camera stays above the floor dynamically, i.e. if looking way up 
        desiredPosition.y = Mathf.Max(ball.position.y + heightOffset, desiredPosition.y);

        transform.position = desiredPosition;

        // makes camera look at the pivot point
        transform.LookAt(pivotPoint);
    }
}
