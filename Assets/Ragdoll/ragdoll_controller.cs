using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ragdoll_controller : MonoBehaviour
{
    [SerializeField] float ragdollPushForce;
    [SerializeField] float upwardsRicochetForce;

    bool ragdoll_hit = false;

    public void DetectCollision(Collision col, Rigidbody limb_rb)
    {
        if (!ragdoll_hit) { 
            ragdoll_hit = true; // Allows a ragdoll to only be hit a single time
            GetComponent<rd_anim_control>().disableAnimator();  //Turn off animations to allow for ragdoll


            Rigidbody[] child_rigidbodies = GetComponentsInChildren<Rigidbody>();
            Vector3 hitVel = col.rigidbody.velocity; // Find the velocity of the ball

            foreach (Rigidbody child in child_rigidbodies)
            {
                child.useGravity = true;
                limb_rb.AddForce(hitVel.normalized * ragdollPushForce, ForceMode.Impulse);   // Apply force in the direction of the velocity, multiplied by the power

                
            }
            float upwardsForceScaler = 0;
            if (hitVel.y > -10)
                upwardsForceScaler = upwardsRicochetForce / 2;
            else
                upwardsForceScaler = upwardsRicochetForce;
            Debug.Log(upwardsForceScaler);
            col.rigidbody.AddForce(Vector3.up * upwardsForceScaler, ForceMode.Impulse);

            
        }
    }
}
