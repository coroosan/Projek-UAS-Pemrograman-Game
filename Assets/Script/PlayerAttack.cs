using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform daggerPoint;
    [SerializeField] private GameObject[] throwDagger;
    private Animator anim;
    private PlayerController playerController;
    private float cooldownAttack = Mathf.Infinity;
    [SerializeField] private AudioClip daggerSound;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownAttack > attackCooldown && playerController.canAttack())
            Attack();

        cooldownAttack += Time.deltaTime;
    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(daggerSound);
        anim.SetTrigger("Attack");
        cooldownAttack = 0;

        throwDagger[FindDagger()].transform.position=daggerPoint.position;
        throwDagger[FindDagger()].GetComponent<Projectile>().setDirection(Mathf.Sign(transform.localScale.x));

        
    }

    private int FindDagger()
    {
        for (int i = 0; i < throwDagger.Length; i++)
        {
            if (!throwDagger[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    
}
