using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelLayout")]
public class LevelLayout : ScriptableObject
{
    // Configuration parameters
    [SerializeField] string layoutName;
    [SerializeField] bool repeatRows;
    [SerializeField] int numberOfEnemies;
    [SerializeField] List<LevelRow> levelRows;

    public string LayoutName { get => layoutName; set => layoutName = value; }
    public bool RepeatRows { get => repeatRows; set => repeatRows = value; }
    public int NumberOfEnemies { get => numberOfEnemies; set => numberOfEnemies = value; }
    public List<LevelRow> LevelRows { get => levelRows; set => levelRows = value; }
}

[Serializable]
public class LevelRow
{
    [Tooltip("Uses the GridManager's enemy size: 1 = 1 enemy size as offset")]
    public float weaveOffset = 0f;
    public List<EnemyUnit> enemyUnits;
}
