using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ragdoll_controller : MonoBehaviour
{
    [SerializeField] float ragdollPushForce;        // Force applied to the ragdoll upon impact
    [SerializeField] float upwardsRicochetVelocity; // Vertical velocity given to the ball after hitting the ragdoll

    bool ragdoll_hit = false;   // Whether or not the ragdoll has been struck

    // Method called by the limbs of the ragdoll when a collision is detected. 
    // Passes the collision information to get the info about the ball, and the rigidbody of the limb struck
    public void DetectCollision(Collision col, Rigidbody limb_rb)
    {
        //If they haven't yet been hit...
        if (!ragdoll_hit) { 

            ragdoll_hit = true;                                                     // Allows a ragdoll to only be hit a single time
            Vector3 hitVel = col.rigidbody.velocity;                                // Find the velocity of the ball
            GetComponent<rd_anim_control>().disableAnimator();                      //Turn off animations to allow for ragdoll
            Rigidbody[] child_rigidbodies = GetComponentsInChildren<Rigidbody>();   // Get all the rigidbodies of the limbs such that each can have a force applied to it

            foreach (Rigidbody child in child_rigidbodies)
            {
                child.useGravity = true;                                                    // Turn on gravity (its off so they don't accelerate while animated)
                limb_rb.AddForce(hitVel.normalized * ragdollPushForce, ForceMode.Impulse);  // Apply force in the direction of the velocity, multiplied by the power
            }
            
            col.rigidbody.velocity = new Vector3(hitVel.x, upwardsRicochetVelocity, hitVel.z);  // Apply an upwards velocity to the ball

            // Get the ball's collider, turn it off, wait 35ms, then turn it back on
            // Ensures the ball is not interfered with by the ragdoll right after hitting it
            SphereCollider ballSpCol = col.gameObject.GetComponent<SphereCollider>();   
            ballSpCol.isTrigger = true; 
            StartCoroutine(solidifyCollider(ballSpCol));  
        }
    }

    IEnumerator solidifyCollider(SphereCollider ball)
    {
        yield return new WaitForSeconds(0.35f);  
        ball.isTrigger = false;
        Debug.Log("Solidified Collider");
    }
}
