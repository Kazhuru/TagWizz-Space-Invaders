using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Configuration parameters
    [SerializeField] int lifes = 1;
    [SerializeField] AudioClip playerDeadSFX;
    [SerializeField] [Range(0f, 1f)] float playerDeadVolume = 0.5f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<EnemyHealth>() != null)
        {
            reducePlayerLifes();
            Destroy(collision.gameObject);
        }
    }

    public void reducePlayerLifes()
    {
        lifes--;
        if(lifes <= 0)
        {
            if (playerDeadSFX != null)
                AudioSource.PlayClipAtPoint(playerDeadSFX, Camera.main.transform.position, playerDeadVolume);
            FindObjectOfType<SceneLoader>().LoadGameoverScene(0.75f);
            Destroy(this.gameObject);
        }
    }
}
