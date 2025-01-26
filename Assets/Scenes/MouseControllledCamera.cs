using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControlledCamera : MonoBehaviour
{
    public Transform ball; // Reference to the golf ball
    public float distance = 10f; // Distance from the ball
    public float pivotHeight = 2f; // Pivot point height above the ball
    public float sensitivity = 0.5f; // Sensitivity for mouse movement
    public Vector2 turn; // Keeps track of mouse input for rotation
    public float maxTilt = 120f; // Maximum upward tilt
    public float minTilt = -80f; // Maximum downward tilt

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the screen
    }

    void LateUpdate()
    {
        if (ball == null)
        {
            Debug.LogWarning("Ball reference is missing!");
            return;
        }

        // Capture mouse input
        turn.x += Input.GetAxis("Mouse X") * sensitivity;
        turn.y -= Input.GetAxis("Mouse Y") * sensitivity;

        // Clamp vertical rotation for dramatic angles
        turn.y = Mathf.Clamp(turn.y, minTilt, maxTilt);

        // Calculate rotation based on input
        Quaternion rotation = Quaternion.Euler(turn.y, turn.x, 0);

        // Shift the pivot point above the ball to allow looking "under" it
        Vector3 pivotPoint = ball.position + Vector3.up * pivotHeight;

        // Calculate the offset for the camera based on rotation
        Vector3 offset = rotation * new Vector3(0, 0, -distance);

        // Set the camera position relative to the shifted pivot point
        transform.position = pivotPoint + offset;

        // Make the camera look at the pivot point
        transform.LookAt(pivotPoint);
    }
}
