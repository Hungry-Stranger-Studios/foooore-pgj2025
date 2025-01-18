using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   private Rigidbody rb;
   [Header("Swing Settings")]
   [SerializeField] private float speed = 5f;
   [SerializeField] private float maxSwingForce = 100f;
   [SerializeField] private float chargeRate = 10f;

    private float currentSwingForce = 0f;
    private bool isCharging = false;
    private bool isGrounded = true;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleSwingInput();
    
    }
    
    private void HandleSwingInput(){
        if(isGrounded){
            if(Input.GetKeyDown(KeyCode.Space)){
                isCharging=true;
                currentSwingForce=0f;
            }

            if(Input.GetKey(KeyCode.Space) && isCharging){
                currentSwingForce += chargeRate*Time.deltaTime;
                currentSwingForce = Mathf.Clamp(currentSwingForce,0,maxSwingForce); // this is creating a ceiling for max swing force. if user exceeds it will auto go to maxSwing
            }
            if(Input.GetKeyUp(KeyCode.Space) && isCharging){
                isCharging = false;
                SwingBall();
            }
        }
    }

    private void SwingBall(){
        rb.AddForce(transform.forward *currentSwingForce,ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision collision){
    // Check if the ball collides with the ground layer
    if (collision.gameObject.CompareTag("Ground"))
    {
        isGrounded = true;
    }
}

    private void OnCollisionExit(Collision collision){
        // Check if the ball leaves the ground layer
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}