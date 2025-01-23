using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControlledCamera : MonoBehaviour
{
    public Transform ball; // Reference to the golf ball
    public float distance = 5f; // Distance from the ball
    public float height = 2f; // Height offset above the ball
    public float sensitivity = 0.5f; // Sensitivity for mouse movement
    public Vector2 turn; // Keeps track of mouse input for rotation

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the screen
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

        // Clamp the vertical rotation to prevent the camera from flipping
        turn.y = Mathf.Clamp(turn.y, -30f, 60f);

        Quaternion rotation = Quaternion.Euler(turn.y, turn.x, 0);
        Vector3 offset = rotation * new Vector3(0, height, -distance);

        // set camera position relative to the ball
        transform.position = ball.position + offset;

        // make the camera look at ball
        transform.LookAt(ball.position + Vector3.up * 0.5f); // looking slightly above the ball - maybe adjust to be slightly left of ball like over shoulder POV
    }
}
