using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyUnit
{
    Empty,
    Green,
    Blue,
    Red
}

public class EnemyWeave : MonoBehaviour
{
    // Configuration parameters
    [SerializeField] float movementInterval = 1f;
    [SerializeField] float movementSpeed;
    [SerializeField] Transform weaveGenerationPoint;
    [SerializeField] GameObject prefabGreenEnemy;
    [SerializeField] GameObject prefabBlueEnemy;
    [SerializeField] GameObject prefabRedEnemy;

    // Object variables
    private bool tickInterval = true;
    private bool weaveMoving = true;
    private Dictionary<EnemyUnit, GameObject> enemyPrefabDictionary;
    private int EnemySpawnCount;
    private int spawningCounter = 0;
    private bool stopEnemySpawning = false;
    private LevelLayout currentLayout;

    //Cached variables
    GridManager gridManager;

    void Start()
    {
        enemyPrefabDictionary = new Dictionary<EnemyUnit, GameObject>();
        enemyPrefabDictionary.Add(EnemyUnit.Green, prefabGreenEnemy);
        enemyPrefabDictionary.Add(EnemyUnit.Blue, prefabBlueEnemy);
        enemyPrefabDictionary.Add(EnemyUnit.Red, prefabRedEnemy);

        GameSession session = FindObjectOfType<GameSession>();
        if (session != null)
        {
            currentLayout = session.CurrentGameLayout;
            EnemySpawnCount = session.CurrentGameLayout.NumberOfEnemies;
        }
        gridManager = FindObjectOfType<GridManager>();
    }

    void Update()
    {
        if (tickInterval)
        {
            StartCoroutine(WeaveInterval());
            weaveMoving = !weaveMoving;
            if (weaveMoving && !stopEnemySpawning)
                ExecuteLevelLayout();
        }

        if (weaveMoving)
            this.OnMovementTick?.Invoke();
    }

    private void ExecuteLevelLayout()
    {
        if (currentLayout == null) { return; }

        SpawnRowEnemies();

        spawningCounter++;
        if (spawningCounter >= currentLayout.LevelRows.Count)
        {
            if (currentLayout.RepeatRows)
                spawningCounter = 0;
            else
                stopEnemySpawning = true;
        }
    }

    private void SpawnRowEnemies()
    {
        LevelRow levelRow = currentLayout.LevelRows[spawningCounter];
        int rowUnitCounter = 0;

        foreach (EnemyUnit enemyUnit in levelRow.enemyUnits)
        {
            if (enemyUnit != EnemyUnit.Empty)
            {
                Vector2 instancePosition = new Vector2(
                    weaveGenerationPoint.position.x +
                    (rowUnitCounter * gridManager.UnitWorldSize) +
                    (levelRow.weaveOffset * gridManager.UnitWorldSize),
                    weaveGenerationPoint.position.y);

                Instantiate(enemyPrefabDictionary[enemyUnit], instancePosition, Quaternion.identity);
                EnemySpawnCount--;
            }
            rowUnitCounter++;

            if (EnemySpawnCount <= 0)
            {
                stopEnemySpawning = true;
                break;
            }
        }
    }

    private IEnumerator WeaveInterval()
    {
        tickInterval = false;
        yield return new WaitForSeconds(movementInterval);
        tickInterval = true;
    }

    public event Action OnMovementTick;
    public float GetMovementSpeed() { return movementSpeed; }
    public LevelLayout SetCurrentLayout { set => currentLayout = value; }
}
