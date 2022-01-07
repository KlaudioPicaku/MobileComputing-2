using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemyAI : MonoBehaviour
{


    [SerializeField] List<Transform> points;
    [SerializeField] int nextID;
    int idChangeValue = 1;
    Animator animator;
    float minAttackDistance = 0.5f;
    public float speed = 0.5f;
     string path;
    private void Awake()
    {
        path = Application.persistentDataPath + "/TEMP";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
       string path2 = Application.persistentDataPath + "/TEMP" + "/" + this.gameObject.name + ".enemy";
        if (File.Exists(path2))
        {
            this.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
    private void Start()
    {
       
        //Vector2 position = GameObject.FindWithTag("Player").transform.position;
        animator = GetComponent<Animator>();
    }
    private void Reset()
    {
        animator.SetBool("IsMoving", false);
        Init();

    }
    void Init()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        GameObject root = new GameObject(name + " root");
        transform.SetParent(root.transform);
        GameObject waypoints = new GameObject("Waypoints");
        waypoints.transform.SetParent(root.transform);
        waypoints.transform.position = root.transform.position;
        GameObject p1 = new GameObject("Point1");
        p1.transform.SetParent(waypoints.transform);
        p1.transform.position = root.transform.position;
        GameObject p2 = new GameObject("Point2");
        p2.transform.SetParent(waypoints.transform);
        p2.transform.position = root.transform.position;
        points = new List<Transform>();
        points.Add(p1.transform);
        points.Add(p2.transform);
        

    }
    private void Update()
    {
        // CheckEnemyRangePeace();
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Walk"))
        {
            MoveToNextPoint();
        }
    }
   
    void MoveToNextPoint()
    {
        Transform goalPoint = points[nextID];
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Walk"))
        {
            if (goalPoint.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
                transform.localScale = new Vector3(1, 1, 1);
            transform.position = Vector2.MoveTowards(transform.position, goalPoint.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, goalPoint.position) < 0.2f)
            {
                if (nextID == points.Count - 1)
                    idChangeValue = -1;
                if (nextID == 0)
                    idChangeValue = 1;
                nextID += idChangeValue;
            }
        }
    }
    private void stopMovement()
    {
        animator.SetBool("IsMoving",false);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, 3f * minAttackDistance);
        Gizmos.DrawWireSphere(transform.position, minAttackDistance);
    }
}
