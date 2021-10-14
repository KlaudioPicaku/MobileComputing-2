
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
   // public Vector3 minValues, maxValues;
    void LateUpdate(){

        Vector3 offsetPosition = target.position + offset;

        //verify target position out of bounds or not

        Vector3 smoothPosition = Vector3.Lerp(transform.position, offsetPosition, smoothSpeed);
        transform.position = smoothPosition;
        }
    
}
