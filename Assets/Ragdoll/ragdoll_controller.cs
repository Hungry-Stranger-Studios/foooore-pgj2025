using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ragdoll_controller : MonoBehaviour
{
    [SerializeField] float power;

    public void DetectCollision(Collision col, Rigidbody limb_rb)
    {
        Debug.Log(limb_rb);
        Vector3 relVelocity = col.relativeVelocity;
        //limb_rb.AddForce(relVelocity * power, ForceMode.Impulse);
    }
}
