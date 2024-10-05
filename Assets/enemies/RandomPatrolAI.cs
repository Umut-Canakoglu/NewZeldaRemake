using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class RandomPatrolAI : MonoBehaviour
{
    private float speed = 400f;
    private float pickNextWayPointDistance = 2f;
    private Vector2 targetPos;
    private Path path;
    private Seeker seeker;
    private Rigidbody2D rb;
    private int currentWayPoint = 1;
    private float xRange = 5f;
    private float yRange = 5f;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        targetPos = RandomizeTarget();
        seeker.StartPath(rb.position, targetPos, OnPathComplete);
    }
    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 1;
        }
    }
    void Update() 
    {
        if (path == null)
        {
            return;
        }
        if (currentWayPoint >= path.vectorPath.Count)
        {
            targetPos = RandomizeTarget();
            seeker.StartPath(rb.position, targetPos, OnPathComplete);
            return;
        }
        Vector2 direction = (new Vector2(path.vectorPath[currentWayPoint].x, path.vectorPath[currentWayPoint].y) - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        rb.AddForce(force);
        float distance = Vector2.Distance(rb.position, new Vector2(path.vectorPath[currentWayPoint].x, path.vectorPath[currentWayPoint].y));
        if (distance < pickNextWayPointDistance)
        {
            currentWayPoint++;
        }
    }
    private Vector2 RandomizeTarget()
    {
        float randX = Random.Range(-xRange, xRange);
        float randY = Random.Range(-yRange, yRange);
        return new Vector2(randX, randY);
    }
}
