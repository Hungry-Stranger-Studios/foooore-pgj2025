using UnityEngine;

public class FollowWithoutRotation : MonoBehaviour
{
    public Transform target; // The golf ball to follow
    public Vector3 offset;   // Position offset relative to the target

    public bool useWorldSpace = true; // Toggle to apply the offset in world space or local space

    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the desired position with the offset
            Vector3 desiredPosition = useWorldSpace 
                ? target.position + offset 
                : target.TransformPoint(offset);

            // Update the position of this GameObject
            transform.position = desiredPosition;
        }
    }
}
