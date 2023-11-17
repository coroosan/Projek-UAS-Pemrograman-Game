using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSpikeEnemy : MonoBehaviour
{

    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //collision.GetComponent<Health>().Takedamage(damage);
            collision.GetComponent<HealthPlayer>().Takedamage(damage);
        }
    }
}
