using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    [Header("Swing Settings")]
    [SerializeField] private float maxSwingForce = 100f;        // Swing force for how hard you hit the ball
    [SerializeField] private float chargeRate = 10f;            // Rate at which the swing charges
    
    [Header("Color Settings")]
    [SerializeField] private Color startColor = Color.white;    // Colour of the ball before a swing is charged...
    [SerializeField] private Color midColor = Color.green;      // During the charge...
    [SerializeField] private Color maxForceColor = Color.red;   // And when its fully charged

    [Header("Trajectory Settings")]
    [SerializeField] private int trajectoryPoints = 30;         // Number of points to plot
    [SerializeField] private float timeStep = 0.1f;             // Time between points
    [SerializeField] private float gravityMultiplier = 1f;      // Simulate gravity

    [Header("Air Movement Settings")]
    [SerializeField] private float airSpeed = 10f;              // The scaler for the force applied in any direction while moving in the air

    private Camera playerCamera;            // The camera following the ball
    private Rigidbody rb;                   // The rigidbody attatched to the ball
    private Renderer ballRenderer;          
    private LineRenderer lineRenderer;

    // Internal compitation values
    private float currentSwingForce = 0f;
    private bool isCharging = false;
    private bool isGrounded = false;
    public bool canHit = true;
    private bool movementDisabled = false;


    void Start()
    {
        
        rb = GetComponent<Rigidbody>();

        playerCamera = GameObject.Find("Main Camera").GetComponent<Camera>();   // Dynamically finds the camera in the scene

        ballRenderer = GetComponent<Renderer>(); 
        if (ballRenderer != null){
            ballRenderer.material.color = startColor;
        }

        lineRenderer = GetComponent<LineRenderer>();
        if(lineRenderer == null){
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        lineRenderer.positionCount = trajectoryPoints;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.green;
        lineRenderer.endColor = Color.red;
        lineRenderer.enabled = false; // Hide initially
    }

    void OnEnable()
    {
        GameManager.Instance.GetPauseManager().onPauseGame += pausePlayer;
        GameManager.Instance.GetPauseManager().onUnpauseGame += unpausePlayer;
    }

    void OnDisable()
    {
        GameManager.Instance.GetPauseManager().onPauseGame  -= pausePlayer;
        GameManager.Instance.GetPauseManager().onUnpauseGame -= unpausePlayer;
    }

    // Update is called once per frame
    void Update()
    {
        if(!movementDisabled)
            HandleSwingInput();
    }

    private void FixedUpdate()
    {
        if(!movementDisabled)
            AirMovement();
    }

    //Air movement controls. WASD to add forces in the respective direction relative to the cameras forward facing direction
    private void AirMovement(){
        if(!isGrounded){
            if (Input.GetKey(KeyCode.A)) {
                rb.AddForce(playerCamera.transform.right * airSpeed * -1);
            }
            if (Input.GetKey(KeyCode.D)) {
                rb.AddForce(playerCamera.transform.right * airSpeed);
            }
            if (Input.GetKey(KeyCode.W))
            {
                rb.AddForce(playerCamera.transform.forward * airSpeed);
            }
            if (Input.GetKey(KeyCode.S))
            {
                rb.AddForce(playerCamera.transform.forward * airSpeed * -1);
            }
        }
    }

    
    private void HandleSwingInput(){
        if(canHit){  
            if(Input.GetKeyDown(KeyCode.Space)){
                isCharging=true;
                currentSwingForce=0f;
                lineRenderer.enabled = true; 
            }

            if(Input.GetKey(KeyCode.Space) && isCharging){
                currentSwingForce += chargeRate*Time.deltaTime;
                currentSwingForce = Mathf.Clamp(currentSwingForce,0,maxSwingForce); // this is creating a ceiling for max swing force. if user exceeds it will auto go to maxSwing
                UpdateTrajectory();
                UpdateBallColor();
            }
            if(Input.GetKeyUp(KeyCode.Space) && isCharging){
                isCharging = false;
                SwingBall();
                lineRenderer.enabled = false; // Hide trajectory arrow
            }
        }
    }
    private void UpdateTrajectory()
    {
        // Initial position and velocity
        UnityEngine.Vector3 startPosition = transform.position;
        UnityEngine.Vector3 startVelocity = playerCamera.transform.forward * currentSwingForce / rb.mass;
        startVelocity += UnityEngine.Vector3.up * (currentSwingForce * 0.5f); // Adjust 0.5f for more or less arc height

        // Simulate the trajectory points
        UnityEngine.Vector3 currentPosition = startPosition;
        UnityEngine.Vector3 currentVelocity = startVelocity;

        for (int i = 0; i < trajectoryPoints; i++)
        {
            lineRenderer.SetPosition(i, currentPosition);

            // Update position and velocity based on physics
            currentPosition += currentVelocity * timeStep;
            currentVelocity += Physics.gravity * gravityMultiplier * timeStep;
        }
    }
    private void UpdateBallColor(){
         float t = currentSwingForce / maxSwingForce;

        // goes white to green (0–75% power)
        if (t <= 0.75f)
        {
            float subT = t / 0.75f; // Normalize t for the 0–0.75 range
            ballRenderer.material.color = Color.Lerp(startColor, midColor, subT);
        }
        // green to red (75–100% power)
        else
        {
            float subT = (t - 0.75f) / 0.25f; // Normalize t for the 0.75–1 range
            ballRenderer.material.color = Color.Lerp(midColor, maxForceColor, subT);
        }
    }

    private void SwingBall(){
        if (playerCamera != null)
        {
            UnityEngine.Vector3 forwardForce = playerCamera.transform.forward * (currentSwingForce / rb.mass);
            UnityEngine.Vector3 upwardForce = UnityEngine.Vector3.up * (currentSwingForce * 0.5f / rb.mass);
            UnityEngine.Vector3 combinedForce = forwardForce + upwardForce;

            rb.velocity = UnityEngine.Vector3.zero;
            rb.AddForce(combinedForce, ForceMode.Impulse);
        }
        ballRenderer.material.color = startColor;
        canHit = false;
    }
    private void OnCollisionEnter(Collision collision) {
        // Check if the ball collides with the ground layer
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Ragdoll"))
        {
            canHit = true;
            Debug.Log("hit ragdoll");
        }
    }

    private void OnCollisionExit(Collision collision){
        // Check if the ball leaves the ground layer
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void playerWin()
    {

    }

    private void pausePlayer()
    {
        movementDisabled = true;
    }

    private void unpausePlayer()
    {
        movementDisabled = false;
    }
}