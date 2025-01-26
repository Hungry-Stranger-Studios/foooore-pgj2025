using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class limb_collider_detection : MonoBehaviour
{
    private ragdoll_controller rc;  //Parent ragdoll controller
    private PlayerController playerController;

    void Start()
    {
        //Get ragdoll controller
        rc = transform.root.gameObject.GetComponentInChildren<ragdoll_controller>();
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player"){

            rc.DetectCollision(collision, gameObject.GetComponent<Rigidbody>());
            playerController.canHit = true;
        }
    }
}
