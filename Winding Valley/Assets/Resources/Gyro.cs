
using UnityEngine;

public class Gyro : MonoBehaviour
{
    [SerializeField]
    private float shiftModofier = 1f;

    private Gyroscope gyro;

    // Start is called before the first frame update
    void Start()
    {
        gyro = Input.gyro;
        gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(gyro.rotationRateUnbiased.y);
        if (gyro.rotationRateUnbiased.y >= -0.0001f && gyro.rotationRateUnbiased.y <= 0.0003f)
        {
            transform.Translate((float)System.Math.Round(gyro.rotationRateUnbiased.y, 1) * shiftModofier, 0f, 0f);
        }
    }
}
