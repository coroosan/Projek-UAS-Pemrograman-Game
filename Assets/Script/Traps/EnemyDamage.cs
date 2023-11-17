using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            //collision.GetComponent<Health>().Takedamage(damage);
            collision.GetComponent<HealthPlayer>().Takedamage(damage);

        
    }
}
