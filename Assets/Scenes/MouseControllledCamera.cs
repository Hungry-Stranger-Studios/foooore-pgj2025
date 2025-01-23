using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControlledCamera : MonoBehaviour
{
    public Vector2 turn;
    public float sensitivity = 0.5f; // Sensitivity for mouse movement

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        turn.x += Input.GetAxis("Mouse X") * sensitivity;
        turn.y += Input.GetAxis("Mouse Y") * sensitivity;
        transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);

    }
}

{/*
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MouseControlledCamera : MonoBehaviour
    {
        public Transform ball; 
        public Vector3 offset = new Vector3(0, 2, -5); // Default offset from the ball
        public float sensitivity = 0.5f; 
        public Vector2 turn;

        private Vector3 aimingDirection;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked; // Locks the cursor to the screen
        }

        void LateUpdate()
        {
            if (ball == null)
            {
                Debug.LogWarning("Ball reference is missing!");
                return;
            }

            // Get mouse input for rotation
            turn.x += Input.GetAxis("Mouse X") * sensitivity;
            turn.y -= Input.GetAxis("Mouse Y") * sensitivity; 
            turn.y = Mathf.Clamp(turn.y, -30f, 60f); 

            // Calculate camera rotation
            Quaternion rotation = Quaternion.Euler(turn.y, turn.x, 0);

            // Update the camera position relative to ball
            Vector3 desiredPosition = ball.position + rotation * offset;
            transform.position = desiredPosition;

            // prevent rolling
            Vector3 lookAtPosition = ball.position + Vector3.up * 0.5f; // adjust target height
            transform.rotation = Quaternion.LookRotation(lookAtPosition - transform.position, Vector3.up);

            aimingDirection = transform.forward;
        }

        public Vector3 GetAimingDirection()
        {
            return aimingDirection;
        }
    }


*/}
