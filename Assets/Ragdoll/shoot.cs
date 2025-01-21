using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
            gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 200, ForceMode.Impulse); 
    }


}
