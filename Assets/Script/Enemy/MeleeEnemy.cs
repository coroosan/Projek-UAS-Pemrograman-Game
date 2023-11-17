using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float damage;

    [Header("Collider parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    [Header("Enemy Sound")]
    [SerializeField] private AudioClip meleeEnemyAttackSound;

    //private Health playerHealth;
    private HealthPlayer playerHealth;
    private Animator anim;

    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        enemyPatrol=GetComponentInParent<EnemyPatrol>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInsight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("meleeAttack");
                SoundManager.instance.PlaySound(meleeEnemyAttackSound);
            }
        }

        if(enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInsight();

    }

    private bool PlayerInsight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range*transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0,Vector2.left,0,playerLayer);
        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<HealthPlayer>();

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    { 
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if (PlayerInsight())
            playerHealth.Takedamage(damage);
    }
}
