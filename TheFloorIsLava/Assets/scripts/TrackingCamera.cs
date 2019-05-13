
using UnityEngine;

public class TrackingCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    private void Start()
    {
        transform.position = target.position + offset;
        
    }

    private void LateUpdate()
    {
        transform.position = target.position + offset;
        transform.rotation = Quaternion.LookRotation( target.position - transform.position);

    }
}
