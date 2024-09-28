using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private Rigidbody2D rb;
    private float horizaontal;
    private float vertical;

    public Animator animator;
    
    void Update()
    {
        horizaontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if (horizaontal != 0 && vertical != 0){
            if(rb.velocity.x != 0){
                vertical = 0;
            }
            else if(rb.velocity.y != 0){
                horizaontal = 0;
            }
        }
        FixedUpdate();
        
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizaontal * speed, vertical * speed);

        animator.SetFloat("xSpeed", horizaontal);
        animator.SetFloat("ySpeed", vertical);

    }
}
