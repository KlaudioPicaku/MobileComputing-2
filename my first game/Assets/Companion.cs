
using UnityEngine;

public class Companion : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public bool setLeft = false;
    public bool setRight = false;
    public bool deactivate = false;
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // public Vector3 minValues, maxValues;
    //private void Start()
    //{
    //    leftLimit = -34f;
    //}
    void Update()
    {
        //if (target.transform.position.x > leftLimit)
        //{
        //while (target.transform.position.x >= leftLimit &&
        //    target.transform.position.y <= topLimit &&
        //    target.transform.position.x <= rightLimit
        //    && target.position.y >= bottomLimit)
        //{
        if(target.localScale.x == -1 && !setLeft)
        {
            setLeft = true;
            setRight = false;
            offset.x = offset.x * (-1);
            Vector3 newscale = new Vector3(1f, 1f, 1f);
            this.gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        if(target.localScale.x == 1 && !setRight)
        {
            setRight = true;
            setLeft = false;
            offset.x = 1f;
            Vector3 newscale = new Vector3(1f, 1f, 1f);
            this.gameObject.transform.localScale = new Vector3(1f,1f,1f);
        }
        if (deactivate)
        {
            deactivate = false;
            this.GetComponent<Animator>().Play("Despawn");
        }
        
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
   public void deactivateComp()
    {
        this.gameObject.SetActive(false);
    }
}
