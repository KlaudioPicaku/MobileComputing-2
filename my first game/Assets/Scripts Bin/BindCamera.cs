using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindCamera : MonoBehaviour
{
    [SerializeField] float leftBound;
    [SerializeField] CameraFollow cameraScript;
    [SerializeField] Transform player;

    // Update is called once per frame
    void Update()
    {
        if (player.position.x>leftBound)
        {

            cameraScript.enabled = true;
        }
        else
        {
            cameraScript.enabled = false;
        }
    }
    public void bindThisCamera(float newLeft)
    {
        this.leftBound = newLeft;
    }
}
