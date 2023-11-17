using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikehead : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    private Vector3[] directions = new Vector3[4];
    private Vector3 destination;
    private float checkTimer;
    private bool attakcing;

    [SerializeField] private float maxX;
    [SerializeField] private float minX;
    [SerializeField] private float maxY;
    [SerializeField] private float minY;

    [Header("SFX")]
    [SerializeField] private AudioClip spikeheadSound;

    private void OnEnable()
    {
        stop();
    }

    private void Update()
    {
        if (attakcing)
            transform.Translate(destination * Time.deltaTime * speed);
        else
        {
            checkTimer += Time.deltaTime;
            if(checkTimer > checkDelay)
                CheckForPlayer();
        }

        if (transform.position.x > maxX || transform.position.x < minX || transform.position.y > maxY || transform.position.y < minY)
        {
            stop();
        }
    }

    private void CheckForPlayer()
    {
        CalculateDirection();
        for(int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit= Physics2D.Raycast(transform.position, directions[i],range, playerLayer);

            if(hit.collider != null && !attakcing)
            {
                attakcing = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }

    private void CalculateDirection()
    {
        directions[0] = transform.right * range;
        directions[1] = -transform.right * range;
        directions[2] = transform.up * range;
        directions[3] = -transform.up * range;
    }

    private void stop()
    {
        destination = transform.position;
        attakcing = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.instance.PlaySound(spikeheadSound);
        base.OnTriggerEnter2D(collision);
        stop();
    }

}
