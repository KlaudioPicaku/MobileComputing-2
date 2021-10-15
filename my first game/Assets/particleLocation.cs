using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleLocation : MonoBehaviour
   
{ 
    [SerializeField] Transform location;

    void Update()
    {
        //player location
        Vector3 playerLocation = new Vector3(location.position.x +0.50804f,location.position.y+0.31347f,0);
        transform.position = playerLocation;

    }
}
