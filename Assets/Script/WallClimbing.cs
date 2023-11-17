using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallClimbing : MonoBehaviour
{
    private float vertical;
    private float speed = 3f;
    private bool isLadder;
    private bool isClimbing;
    private Animator anim;
    [SerializeField] private Rigidbody2D rb;


    void Start()
    {

    }

    void Update()
    {
        vertical = Input.GetAxis("Vertical");

        if (isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
            anim.SetTrigger("Jump");
        }
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
        }
        else
        {
            rb.gravityScale = 1.5f;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            isLadder = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }
}
