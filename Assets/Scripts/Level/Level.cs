using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    // Configuration parameters
    [SerializeField] LevelLayout levelDefinition;

    public void LaunchGameWithLvlLayout()
    {
        GameSession session = FindObjectOfType<GameSession>();
        if(session != null && levelDefinition != null)
        {
            session.CurrentGameLayout = levelDefinition;
            FindObjectOfType<SceneLoader>().LoadGameScene();
        }
    }
}
