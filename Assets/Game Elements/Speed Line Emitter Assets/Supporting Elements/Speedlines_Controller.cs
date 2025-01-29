using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//This component should be attached to the prefab for the speed lines emitter
//Said prefab should be positions +5 units on the z axis as a child of the camera that is following the ball "position = (0, 0, 5)"
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
public class Speedlines_Controller : MonoBehaviour
{
    [SerializeField] Rigidbody playerRigidBody; // ~~~~~~~~PLACEHOLDER UNTIL WE FINALIZE A PLAYER PREFAB. I WILL CHANGE IT TO USE FIND ONCE THAT IS DONE~~~~~~~~
    [SerializeField] float speedThreshold;      // Speed thershold before the speed lines appear
    [SerializeField] float maxSpeed;            // Speed at which the speed lines will be the most intense
    [SerializeField] float maxEmissionSpeed;    // Maximum speed for the emission of the speed lines
    [SerializeField] float minRadius;           // Tightest radius for the speed lines to eminante from

    ParticleSystem speedLineEmitter;            // Particle system attached to the prefab that has its values changed
    

    public float curSpeedVal = 0;

    private void Start()
    {
        speedLineEmitter = gameObject.GetComponent<ParticleSystem>();
        var em = speedLineEmitter.emission;
        em.rateOverTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (curSpeedVal != playerRigidBody.velocity.magnitude)
        {
            curSpeedVal = playerRigidBody.velocity.magnitude;
            if (curSpeedVal > speedThreshold) {
                //Find the scale the speeds lines should be within based on the speed of the player
                float scaler = curSpeedVal / maxSpeed;

                //Get the components for emission and shape of the emitter
                var em = speedLineEmitter.emission;
                var sh = speedLineEmitter.shape;

                //Actually setting the values
                em.rateOverTime = Mathf.Clamp(scaler * maxEmissionSpeed, 0, maxEmissionSpeed);
                sh.radius = Mathf.Clamp((1 / scaler) * minRadius, minRadius, 100);

            } 
            else
            {
                //If below the threshold emission rate is set to 0 so that nothing is shown
                var em = speedLineEmitter.emission;
                em.rateOverTime = 0;
            }
        }
    }
}
