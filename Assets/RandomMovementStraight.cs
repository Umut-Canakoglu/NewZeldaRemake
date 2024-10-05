using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Tilemaps;
using UnityEngine;

public class RandomMovementStraight : MonoBehaviour
{
    private float speed = 2f;
    private Vector2 targetPos;
    private float xRange = 4f;
    private float yRange = 4f;
    private Rigidbody2D rb;
    private float xDistance;
    private float yDistance;
    private float nextWayPoint = 0.5f;
    private RaycastHit2D hitInfo;
    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        targetPos = RandomizeTarget();
    } 
    private Vector2 RandomizeTarget()
    {
        float randX = Random.Range(-xRange, xRange);
        float randY = Random.Range(-yRange, yRange);
        return new Vector2(randX, randY);
    }
    void Update()
    {
        xDistance = targetPos.x - rb.position.x;
        yDistance = targetPos.y - rb.position.y;
        if (Mathf.Abs(xDistance) > nextWayPoint)
        {
            float xDirection = xDistance / Mathf.Abs(xDistance);
            rb.velocity = new Vector2(speed*xDirection, 0);
        } else if (Mathf.Abs(yDistance) > nextWayPoint && Mathf.Abs(xDistance) < nextWayPoint)
        {
            float yDirection = yDistance / Mathf.Abs(yDistance);
            rb.velocity = new Vector2(0, speed*yDirection);
        } else if (Mathf.Abs(xDistance) < nextWayPoint && Mathf.Abs(yDistance) < nextWayPoint)
        {
            targetPos = RandomizeTarget();
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("InvisiLayer"))
        {
            targetPos = RandomizeTarget();
        }
    }
}
