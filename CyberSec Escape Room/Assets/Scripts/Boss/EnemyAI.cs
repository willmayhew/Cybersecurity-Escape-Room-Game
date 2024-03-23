using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Unity.Collections.LowLevel.Unsafe;

public class EnemyAI : MonoBehaviour
{

    private LogicManager logic;

    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    private Path path;
    private int currentWayPoint = 0;
    private bool reachedEndOfPath = false;

    private Seeker seeker;
    private Rigidbody2D rb;

    public Boss boss;

    public float attackInterval = 3f; // Interval between attacks in seconds
    private float attackTimer = 0f; // Timer to track attack intervals

    public bool IsAIStarted { get; private set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        logic = LogicManager.Instance;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        boss.AttackFinished += OnAttackFinished;
        
    }

    public void StartAI()
    {
        if (!IsAIStarted)
        {
            IsAIStarted = true;
            InvokeRepeating("UpdatePath", 0f, 1f);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (!IsAIStarted)
            return;

        attackTimer += Time.fixedDeltaTime;

        if(attackTimer >= attackInterval)
        {
            Attack();
            attackTimer = 0f;
 
        }

        if(boss.IsAttacking || path == null)
        {
            return;
        }

        MoveAlongPath();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            logic.decrementLife();
        }
    }

    private void UpdatePath()
    {
        if(!boss.IsAttacking && seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;          
        }
    }
    void MoveAlongPath()
    {
        if (currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            boss.SetMoving(false);
            return;
        }
        else
        {
            reachedEndOfPath = false;
            boss.SetMoving(true);
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;

        Vector2 force = direction * speed * Time.fixedDeltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if (distance < nextWaypointDistance)
        {
            currentWayPoint++;
        }

        boss.SetSpriteDirection(force);

    }

    void Attack()
    {
        if (!IsAIStarted)
        {
            return;
        }

        boss.TriggerAttackAnimation();
        rb.velocity = Vector2.zero;
    }

    private void OnAttackFinished()
    {

    }

    public void ToggleMovement(bool enableMovement)
    {
        if (enableMovement)
        {
            // Resume AI movement
            if (!IsAIStarted)
            {
                IsAIStarted = true;
                InvokeRepeating("UpdatePath", 0f, 1f);
            }
        }
        else
        {
            // Stop AI movement
            IsAIStarted = false;
            CancelInvoke("UpdatePath");
            rb.velocity = Vector2.zero;
        }
    }

    public void HideObject()
    {
        gameObject.SetActive(false);
        logic.GameComplete();
    }

}
