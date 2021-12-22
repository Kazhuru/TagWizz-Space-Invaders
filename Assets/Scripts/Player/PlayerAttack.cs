using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Configuration parameters
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserShotSpeed = 8f;
    [SerializeField] float laserShotInterval = 1f;
    [SerializeField] int laserShotDamage = 1;
    [SerializeField] AudioClip laserShotSFX;
    [SerializeField] [Range(0f, 1f)] float laserVolume = 0.7f;
    [SerializeField] BoxCollider2D middleTouch;

    // Object variables
    private bool outOfShootInterval = true;
    private GameObject currentLaserPrefab;
    private float currentLaserSpeed;
    private float currentLaserInterval;
    private int currentLaserDamage;
    private AudioClip currentLaserSFX;
    private Coroutine powerupCoroutine;

    void Start()
    {
        StatsValuesToDefault();
    }

    void Update()
    {
        GetVerticalAxisInput();
        GetTouchInput();
    }

    private void GetVerticalAxisInput()
    {
        float verticalKeyboardInput = Input.GetAxis("Vertical");
        if (verticalKeyboardInput > 0)
        {
            Shoot();
        }
    }

    private void GetTouchInput()
    {
        if(Input.touchCount > 0)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (outOfShootInterval)
        {
            FireLaser();
            StartCoroutine(FireInterval());
        }
    }

    private void FireLaser()
    {
        if (currentLaserPrefab != null)
        {
            GameObject laser = Instantiate(currentLaserPrefab, transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, currentLaserSpeed);
            if (currentLaserSFX != null)
            {
                AudioSource.PlayClipAtPoint(currentLaserSFX, Camera.main.transform.position, laserVolume);
            }
        }
    }

    private IEnumerator FireInterval()
    {
        outOfShootInterval = false;
        yield return new WaitForSeconds(currentLaserInterval);
        outOfShootInterval = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Powerup>() != null)
        {
            PowerUp(collision.GetComponent<Powerup>());
            Destroy(collision.gameObject);
        }
    }

    public void PowerUp(Powerup powerup)
    {
        if(powerupCoroutine == null)
            StartCoroutine(StatsPowerupBuff(powerup));
        else
        {
            StopCoroutine(powerupCoroutine);
            powerupCoroutine = null;
        }
    }

    private IEnumerator StatsPowerupBuff(Powerup powerup)
    {
        currentLaserPrefab = powerup.LaserPrefab;
        currentLaserSpeed = powerup.LaserShotSpeed;
        currentLaserInterval = powerup.LaserShotInterval;
        currentLaserDamage = powerup.LaserShotDamage;
        currentLaserSFX = powerup.LaserShotSFX;
        yield return new WaitForSeconds(powerup.PowerupDuration);
        StatsValuesToDefault();
    }

    private void StatsValuesToDefault()
    {
        currentLaserPrefab = laserPrefab;
        currentLaserSpeed = laserShotSpeed;
        currentLaserInterval = laserShotInterval;
        currentLaserDamage = laserShotDamage;
        currentLaserSFX = laserShotSFX;
    }

    public int GetCurrentLaserDamage { get => currentLaserDamage; }
}
