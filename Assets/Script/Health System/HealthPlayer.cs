using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlayer : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    public bool dead;

    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    //win lose panel
    public GameObject gameOverPanel;

    private SoundManager soundManager;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    public void Takedamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            //kode player terkena damage
            anim.SetTrigger("hurt");
            SoundManager.instance.PlaySound(hurtSound);
        }

        else
        {
            //kode player mati
            if (!dead)
            {
                //player
                if (GetComponent<PlayerController>() != null)
                    GetComponent<PlayerController>().enabled = false;
                if (GetComponent<PlayerAttack>() != null)
                    GetComponent<PlayerAttack>().enabled = false;
                //enemy
                if (GetComponentInParent<EnemyPatrol>() != null)
                    GetComponentInParent<EnemyPatrol>().enabled = false;
                if (GetComponent<MeleeEnemy>() != null)
                    GetComponent<MeleeEnemy>().enabled = false;
                if (GetComponent<CannonEnemy>() != null)
                    GetComponent<CannonEnemy>().enabled = false;
                if (GetComponent<EnemyProjectile>() != null)
                    GetComponent<EnemyProjectile>().enabled = false;
                anim.SetTrigger("die");
                anim.SetBool("Ground", true);

                SoundManager.instance.PlaySound(deathSound);
                GameOver();

                dead = true;
            }
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    public void Respawn()
    {
        AddHealth(startingHealth);
        anim.ResetTrigger("die");
        anim.Play("Idle");

        if (GetComponent<PlayerController>() != null)
            GetComponent<PlayerController>().enabled = true;
        if (GetComponent<PlayerAttack>() != null)
            GetComponent<PlayerAttack>().enabled = true;

        dead = false;
    }

    private void Deactive()
    {
        gameObject.SetActive(false);
    }

    void GameOver()
    {
        /*gameOverPanel.SetActive(true);
        Time.timeScale = 0;
        Destroy(gameObject);
        if (GetComponent<SoundManager>() != null)
            GetComponent<SoundManager>().enabled = false;*/

        Invoke("ShowGameOverPanel", 2f);
        soundManager = FindObjectOfType<SoundManager>();
    }

    void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;

         if (soundManager != null)
             Destroy(soundManager.gameObject);
    }
}
