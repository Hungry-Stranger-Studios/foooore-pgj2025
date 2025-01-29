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
            
            col.rigidbody.velocity = new Vector3(hitVel.x, upwardsRicochetForce, hitVel.z);  //Apply an upwards velocity to the ball

            SphereCollider ballSpCol = col.gameObject.GetComponent<SphereCollider>();   //Get balls collider
            ballSpCol.isTrigger = true; //Turn off its collider
            StartCoroutine(solidifyCollider(ballSpCol));  //Turn it back on after a small delay
        }
    }

    IEnumerator solidifyCollider(SphereCollider ball)
    {
        yield return new WaitForSeconds(0.35f);  
        ball.isTrigger = false;
        Debug.Log("Solidified Collider");
    }
}
