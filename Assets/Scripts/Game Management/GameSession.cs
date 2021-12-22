using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    // Configuration parameters
    [SerializeField] AudioClip gameMusic;
    [SerializeField][Range(0f,1f)] float gameMusicVolume = 1f;

    // Object variables
    private int currentGameScore;
    private LevelLayout currentGameLayout;

    public LevelLayout CurrentGameLayout { get => currentGameLayout; set => currentGameLayout = value; }
    public int CurrentGameScore { get => currentGameScore; set => currentGameScore = value; }

    private void Awake()
    {
        int gameManagerCounter = FindObjectsOfType<GameSession>().Length;
        if (gameManagerCounter > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        RestartSessionVariables();
        SetUpGameMusic();
    }

    public void RestartSessionVariables()
    {
        currentGameScore = 0;
    }

    private void SetUpGameMusic()
    {
        AudioSource MyAudioSource = GetComponent<AudioSource>();
        if (gameMusic)
        {
            MyAudioSource.clip = gameMusic;
            MyAudioSource.volume = gameMusicVolume;
            MyAudioSource.Play();
        }
    }
}
