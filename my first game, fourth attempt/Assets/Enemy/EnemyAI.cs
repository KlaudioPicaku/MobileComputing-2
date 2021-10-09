using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemyAI : MonoBehaviour
{

    public List<Transform> points;
    public Transform Goal;
    public int nextID;
    int idChangeValue = 1;
    bool isMoving = true;
    Animator animator;
    float minAttackDistance = 1f;
    bool enemyInRange = false;
    bool isAttacking = true;
    public float speed = 0.5f;
    Vector2 locate;
    HealthBar_controller playerHealth;
    private void Start()
    {
       
        playerHealth = GameObject.FindObjectOfType<HealthBar_controller>();
        Vector2 position = GameObject.FindWithTag("Player").transform.position;
        locate = position;
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
        animator.SetBool("IsMoving", true);
        CheckEnemyRange();
        if (!enemyInRange && isMoving)
        {
            MoveToNextPoint();
            Debug.Log("enemy not in range");
        }
        else if (enemyInRange && isAttacking)
        {
            AttackEnemy();
        }
    }
    void CheckEnemyRange()
    {
        Vector2 skeleton = new Vector2(transform.position.x, transform.position.y);
        float distance = Vector2.Distance(skeleton, Goal.position);
        if (distance <= minAttackDistance)
        {
            isAttacking = true;
            enemyInRange = true;
        }
        else if(distance <= (3f * minAttackDistance) && distance > minAttackDistance )
        {
            enemyInRange = true;
            transform.localScale = new Vector3(-1, 1, 1);
            animator.SetBool("IsMoving",false); 
            animator.SetBool("Alerted", true );
            isMoving = false;

        }
        else if (distance > (3f * minAttackDistance))
        {
            enemyInRange = false;
            isAttacking = false;
            animator.SetBool("IsMoving", true);
            animator.SetBool("Alerted", false);
            animator.SetBool("IsAttacking", false);
            MoveToNextPoint();
            
        }

    }
   /* void MoveToEnemy()  {
     speed = 1f;
        animator.SetBool("IsAttacking", true);
        Invoke("DecreasePlayerHealth", 0.7f);
    }*/
    void AttackEnemy()
    {
        isAttacking = true;
        MoveToPlayer();
        animator.SetBool("IsMoving", false);
        animator.SetBool("IsAttacking", true);
        //Invoke("DecreasePlayerHealth", 0.7f);      
    }
    void MoveToPlayer()
    {
        speed = 1;
        if (Goal.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
            transform.localScale = new Vector3(1, 1, 1);
        animator.SetBool("IsMoving", true);
        transform.position = Vector2.MoveTowards(transform.position,Goal.position, speed * Time.deltaTime);

    }
    /*void DecreasePlayerHealth()
    {
        playerHealth.SetDamage(Random.Range(1f, 10f));
    }*/
    void MoveToNextPoint()
    {
        Transform goalPoint = points[nextID];
        if (goalPoint.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
            transform.localScale = new Vector3(1, 1, 1);
        transform.position = Vector2.MoveTowards(transform.position,goalPoint.position,speed*Time.deltaTime);
        if (Vector2.Distance(transform.position, goalPoint.position) < 0.2f)
        {
            if (nextID == points.Count - 1)
                idChangeValue = -1;
            if (nextID == 0)
                idChangeValue = 1;
            nextID += idChangeValue;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, 4f * minAttackDistance);
        Gizmos.DrawWireSphere(transform.position, minAttackDistance);
    }
}
