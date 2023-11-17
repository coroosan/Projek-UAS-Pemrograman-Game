using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Spike : MonoBehaviour
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
