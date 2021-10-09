using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
    float minAttackDistance = 0.65f;
    bool enemyInRange = false;
    bool isAttacking = true;
    bool flag = false;
    public float speed = 0.5f;
    Vector2 locate;
    [SerializeField] HealthBarController playerHealth;
    public float cooldown = 2f;
    private float nextFireTime=0;
    private float nextAnimationTime = 0.7f;
    private float animationCooldown = 0.3f;
    private void Start()
    {
       
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
        else if (isAttacking && !flag && enemyInRange)
        {
            if (Time.time > nextFireTime)
            {
                nextFireTime = Time.time + cooldown;
                AttackEnemy();
            }
        }
    }
    void CheckEnemyRange()
    {
        Vector2 skeleton = new Vector2(transform.position.x, transform.position.y);
        float distance = Vector2.Distance(skeleton, Goal.position);
        /*player is within attack distance, enemy can attack*/
        if (distance <= minAttackDistance)
        {
            isAttacking = true;
            enemyInRange = true;
            flag = false;
        }
        /*check if player is within twice the attack distance and react to it*/
        else if (distance <= (2f * minAttackDistance) && distance > minAttackDistance )
        {
            enemyInRange = true;
            transform.localScale = new Vector3(-1, 1, 1);
            animator.SetBool("IsMoving",false); 
            animator.SetBool("Alerted", true );
            isMoving = false;
            isAttacking = false;
            flag = true;

        }
        /*check if player is within 20% distance and move closer then attack*/
        else if (distance <= (1.2f * minAttackDistance) && distance > minAttackDistance)
        {
            enemyInRange = true;
            transform.localScale = new Vector3(-1, 1, 1);
            animator.SetBool("IsMoving", true);
            animator.SetBool("Alerted", false);
            isMoving = true;
            isAttacking = false;
            flag = true;
            MoveToPlayer();
        }
        /*player is nowhere near to be a threat continue patrolling the spots*/
        else
        {
            enemyInRange = false;
            isAttacking = false;
            flag = true;
            animator.SetBool("IsMoving", true);
            animator.SetBool("Alerted", false);
            animator.SetBool("IsAttacking", false);
            MoveToNextPoint();
        }

    }

    void AttackEnemy()
    {     
           isAttacking = true;
           MoveToPlayer();
           animator.SetBool("IsMoving", false);   
           DecreasePlayerHealth();
           flag = true;

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
    void DecreasePlayerHealth()
    {
        float randomnum = Random.Range(1f, 5f);
        animator.SetBool("IsAttacking", true);
        if (Time.time > nextAnimationTime) {
            nextAnimationTime = Time.time + animationCooldown;
            playerHealth.SetDamage(randomnum);
        }
        isAttacking = false;
        return;
    }
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
        Gizmos.DrawWireSphere(transform.position, 3f * minAttackDistance);
        Gizmos.DrawWireSphere(transform.position, minAttackDistance);
    }
}
