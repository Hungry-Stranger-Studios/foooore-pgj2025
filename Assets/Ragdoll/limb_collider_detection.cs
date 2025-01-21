using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class limb_collider_detection : MonoBehaviour
{
    private ragdoll_controller rc;  //Parent ragdoll controller

    void Start()
    {
        //Get ragdoll controller
        rc = transform.root.gameObject.GetComponent<ragdoll_controller>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ragdoll Test")
            rc.DetectCollision(collision, gameObject.GetComponent<Rigidbody>());
    }
}
