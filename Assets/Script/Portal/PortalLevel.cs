using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PortalLevel : MonoBehaviour
{
    public GameObject winPanel;
    private SoundManager soundManager;

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Portal")
        {
            showWinPanel();
        }
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Portal"))
        {
            showWinPanel();
        }
    }

    void win()
    {
        Invoke("showWinPanel", 0f);
        soundManager = FindObjectOfType<SoundManager>();
    }

    void showWinPanel()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0;

        if (soundManager != null)
            Destroy(soundManager.gameObject);
    }
}
