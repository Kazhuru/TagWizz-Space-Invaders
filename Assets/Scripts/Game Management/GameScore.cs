using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameScore : MonoBehaviour
{
    // Configuration parameters
    [SerializeField] TextMeshProUGUI textScore;
    [SerializeField] List<Transform> multiplierByPos;

    // Object variables
    private int currentScore = 0;
    private int currentEnemies;

    // Cached variables
    GameSession session;
    PowerupSpawner powerupSpawner;

    void Start()
    {
        session = FindObjectOfType<GameSession>();
        powerupSpawner = FindObjectOfType<PowerupSpawner>();
        if (session != null)
            currentEnemies = session.CurrentGameLayout.NumberOfEnemies;
    }

    void Update()
    {
        textScore.text = currentScore.ToString();
    }

    public void IncreaseScore(int scoreValue, Transform enemyPostion)
    {
        currentScore += scoreValue * DetermineMultiplier(enemyPostion);
        session.CurrentGameScore = currentScore;
        powerupSpawner.CounterTick(enemyPostion.position);
        CheckIfPlayerWon();
    }

    private void CheckIfPlayerWon()
    {
        currentEnemies--;
        if(currentEnemies <= 0)
        {
            FindObjectOfType<SceneLoader>().LoadGameoverScene();
        }
    }

    private int DetermineMultiplier(Transform enemyPostion)
    {
        int baseMultiplier = 1;

        for (int i = 0; i < multiplierByPos.Count; i++)
            if(enemyPostion.position.y >= multiplierByPos[i].position.y)
                baseMultiplier++;

        return baseMultiplier;
    }
}
