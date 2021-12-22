using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Configuration parameters
    [SerializeField] int lifes = 1;
    [SerializeField] int score = 1;
    [SerializeField] AudioClip enemyDeadSFX;
    [SerializeField] [Range(0f, 1f)] float enemyDeadVolume = 0.5f;

    //Cached variables
    private GameScore gameScore;
    private PlayerAttack playerAttack;

    void Start()
    {
        gameScore = FindObjectOfType<GameScore>();
        playerAttack = FindObjectOfType<PlayerAttack>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Laser")
        {
            Destroy(collision.gameObject);
            CalculateHit(playerAttack.GetCurrentLaserDamage);
        }
    }

    private void CalculateHit(int currentPlayerDamage)
    {
        lifes-= currentPlayerDamage;
        if (lifes <= 0)
        {
            if (enemyDeadSFX != null)
                AudioSource.PlayClipAtPoint(enemyDeadSFX, Camera.main.transform.position, enemyDeadVolume);
            gameScore.IncreaseScore(score, this.transform);
            Destroy(this.gameObject);
        }
    }
}
