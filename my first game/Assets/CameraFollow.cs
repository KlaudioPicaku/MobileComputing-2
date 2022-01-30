
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    // public Vector3 minValues, maxValues;
    //private void Start()
    //{
    //    leftLimit = -34f;
    //}
    void Update(){
        //if (target.transform.position.x > leftLimit)
        //{
            //while (target.transform.position.x >= leftLimit &&
            //    target.transform.position.y <= topLimit &&
            //    target.transform.position.x <= rightLimit
            //    && target.position.y >= bottomLimit)
            //{
            Vector3 offsetPosition = target.position + offset;

        //verify target position out of bounds or not
            Vector3 smoothPosition = Vector3.Lerp(transform.position, offsetPosition, smoothSpeed);
            transform.position = smoothPosition;
        //}
            //transform.position = new Vector3(
            //    Mathf.Clamp(transform.position.x,leftLimit,rightLimit),
            //    Mathf.Clamp(transform.position.y,bottomLimit,topLimit),
            //    transform.position.z
            //    );

        }
    
}
