using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    // Configuration parameters
    [SerializeField] float fallingSpeed = 2f;
    [SerializeField] float powerupDuration = 4.5f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserShotSpeed = 8f;
    [SerializeField] float laserShotInterval = 1f;
    [SerializeField] int laserShotDamage = 1;
    [SerializeField] AudioClip laserShotSFX;
    [SerializeField] [Range(0f, 1f)] float laserVolume = 0f;

    public GameObject LaserPrefab { get => laserPrefab; set => laserPrefab = value; }
    public float LaserShotSpeed { get => laserShotSpeed; set => laserShotSpeed = value; }
    public float LaserShotInterval { get => laserShotInterval; set => laserShotInterval = value; }
    public int LaserShotDamage { get => laserShotDamage; set => laserShotDamage = value; }
    public AudioClip LaserShotSFX { get => laserShotSFX; set => laserShotSFX = value; }
    public float LaserVolume { get => laserVolume; set => laserVolume = value; }
    public float PowerupDuration { get => powerupDuration; set => powerupDuration = value; }

    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * fallingSpeed);
    }
}
