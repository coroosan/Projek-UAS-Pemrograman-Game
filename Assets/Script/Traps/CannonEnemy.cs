using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonEnemy : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireBalls;
    private float cooldownTimer;

    //[Header("SFX")]
    //[SerializeField] private AudioClip cannonSound;


    private void Attack()
    {
        cooldownTimer = 0;

        //SoundManager.instance.PlaySound(cannonSound);
        fireBalls[FindFireball()].transform.position = firePoint.position;
        fireBalls[FindFireball()].GetComponent<EnemyProjectile>().ActiveProjectile();
    }

    private int FindFireball()
    {
        for(int i = 0; i < fireBalls.Length; i++)
        {
            if (!fireBalls[i].activeInHierarchy) return i;
        }
        return 0;
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown)
            Attack();
        
    }
}
