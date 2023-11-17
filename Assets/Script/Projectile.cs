using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{

    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifetime;

    private Animator anim;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed,0,0);

        lifetime += Time.deltaTime;
        if(lifetime > 0.7) gameObject.SetActive(false);

    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            hit = true;
            boxCollider.enabled = false;
            anim.SetTrigger("Hit");
        }

        else if (collision.gameObject.CompareTag("Wall"))
        {
            hit = true;
            boxCollider.enabled = false;
            anim.SetTrigger("Hit");
        }

        else if (collision.gameObject.CompareTag("Enemy"))
        {
            hit = true;
            boxCollider.enabled = false;
            anim.SetTrigger("Hit");
            collision.GetComponent<Health>().Takedamage(1);
        }

        /*hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("Hit");*/
    }

    public void setDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX=transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX,transform.localScale.y,transform.localScale.z);
    }

    private void Deactive()
    {
        gameObject.SetActive(false);
    }

}
