using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulateSpaceClick : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] CameraFade script;
    private void Awake()
    {
        mainCamera = GameObject.Find("Main Camera");
        script = mainCamera.GetComponent<CameraFade>();
    }
    public void simulate()
    {
        //script.fade = true;
        //script.fade = false;
    }
}