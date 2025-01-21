using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ragdoll_controller : MonoBehaviour
{
    [SerializeField] float power;
    
    bool ragdoll_hit = false;

    public void DetectCollision(Collision col, Rigidbody limb_rb)
    {
        if (!ragdoll_hit) { 
            ragdoll_hit = true; // Allows a ragdoll to only be hit a single time
            GetComponent<rd_anim_control>().disableAnimator();  //Turn off animations to allow for ragdoll


            Rigidbody[] child_rigidbodies = GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody child in child_rigidbodies)
            {
                child.useGravity = true;
            }

            Debug.Log(limb_rb); // What limb was hit
            Vector3 hitVel = col.rigidbody.velocity; // Find the velocity of the ball
            Debug.Log(hitVel.normalized * power);
            limb_rb.AddForce(hitVel.normalized * power, ForceMode.Impulse);   // Apply force in the direction of the velocity, multiplied by the power
        }
    }
}
