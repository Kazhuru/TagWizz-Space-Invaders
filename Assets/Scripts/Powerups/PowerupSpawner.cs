using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerupSpawner : MonoBehaviour
{
    // Configuration parameters
    [SerializeField] List<Powerup> powerups;
    [SerializeField] int counterForSpawn = 10;

    private int currentSpawnCount = 0;

    public void CounterTick(Vector3 enemyPosition)
    {
        currentSpawnCount++;
        if(currentSpawnCount >= counterForSpawn)
        {
            SpawnPickUp(GenerateRandomPickup(), enemyPosition);
            currentSpawnCount = 0;
        }
    }

    private Powerup GenerateRandomPickup()
    {
        int chosenPowerUp = Random.Range(0, powerups.Count);
        Debug.Log(chosenPowerUp);
        return powerups[chosenPowerUp];
    }

    private void SpawnPickUp(Powerup pickup, Vector3 position)
    {
        Instantiate(pickup, position, Quaternion.identity);
    }
}
