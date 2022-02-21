using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    [SerializeField] GameObject camera;
    [SerializeField] Transform lowerCamera;
    private void Awake()
    {
        camera = GameObject.Find("Main Camera");
        camera.GetComponent<CameraFollow>().enabled = false;
    }
    void Start()
    {
        Vector3 lowerCameraPosition = new Vector3(lowerCamera.position.x,lowerCamera.position.y,lowerCamera.position.z);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
