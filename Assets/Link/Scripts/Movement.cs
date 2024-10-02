using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private Rigidbody2D rb;
    private float horizontal;
    private float vertical;
    private float attackRange = 0.5f;
    private Vector2 attackPosition;
    public Transform attackPointUp;
    public Transform attackPointDown;
    public Transform attackPointLeft;
    public Transform attackPointRight;
    public Animator animator;
    public LayerMask enemyLayers;
    
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 && vertical != 0){
            if(rb.velocity.x != 0){
                vertical = 0;
            }
            else if(rb.velocity.y != 0){
                horizontal = 0;
            }
        }

        FixedUpdate();

        if (Input.GetKeyDown("x"))
        {
            animator.SetTrigger("isAttack");
            Attack();
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, vertical * speed);
        animator.SetFloat("xSpeed", horizontal);
        animator.SetFloat("ySpeed", vertical);
        if (isLookingRight()){
            attackPosition = attackPointRight.position;
        } else if (isLookingLeft())
        {
            attackPosition = attackPointLeft.position;
        } else if (isLookingDown())
        {
            attackPosition = attackPointDown.position;
        } else if (isLookingUp())
        {
            attackPosition = attackPointUp.position;
        }
    }
    private void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPosition, attackRange, enemyLayers);
        if (hitEnemies.Length > 0)
        {
            Debug.Log("Enemy Hit");
        }
    }
    void OnDrawGizmos() {
        DrawCircle(attackPosition, attackRange);
    }
    private void DrawCircle(Vector2 centerPoint, float radius)
    {
        float angleSteps = 360f/100;
        Vector3 previousPoint = centerPoint + Vector2.right * radius;
        for (int i = 0; i <= 100; i++)
        {
            float angle = i*angleSteps;
            float rad = Mathf.Deg2Rad*angle;
            Vector2 newPoint = centerPoint + new Vector2(Mathf.Cos(rad)*radius, Mathf.Sin(rad)*radius);
            Gizmos.DrawLine(previousPoint, newPoint);
            previousPoint = new Vector2(newPoint.x, newPoint.y);
        }
    }
    private bool isLookingRight()
    {
        if (horizontal > 0){
            return true;
        } else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Right")){
            return true;
        } else {
            return false;
        }
    }
    private bool isLookingLeft()
    {
        if (horizontal < 0){
            return true;
        } else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Left")){
            return true;
        } else {
            return false;
        }
    }
    private bool isLookingUp()
    {
        if (vertical > 0){
            return true;
        } else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Up")){
            return true;
        } else {
            return false;
        }
    }
    private bool isLookingDown()
    {
        if (vertical < 0){
            return true;
        } else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Down")){
            return true;
        } else {
            return false;
        }
    }
}
