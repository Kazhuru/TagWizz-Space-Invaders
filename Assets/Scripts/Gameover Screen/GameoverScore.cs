using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameoverScore : MonoBehaviour
{
    // Configuration parameters
    [SerializeField] TextMeshProUGUI textScore;
    //Cached variables
    GameSession session;

    void Start()
    {
        session = FindObjectOfType<GameSession>();
        if (session != null)
            textScore.text = session.CurrentGameScore.ToString();
    }
}
