using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private Rigidbody2D rb;
    private float horizontal;
    private float vertical;

    public Animator animator;
    
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
        
    }

    void FixedUpdate()
    {
        float beforeHorizontal = rb.velocity.x / speed;
        float beforeVertical = rb.velocity.y / speed;
        rb.velocity = new Vector2(horizontal * speed, vertical * speed);
        if (beforeHorizontal != horizontal)
        {
            animator.SetFloat("xSpeed", horizontal);
        } 
        if (beforeVertical != vertical)
        {
            animator.SetFloat("ySpeed", vertical);
        }
    }
}
