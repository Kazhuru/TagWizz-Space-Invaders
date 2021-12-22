using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    // Configuration parameters
    [SerializeField] private Vector2Int gridSize;
    [Tooltip("Enemy size in the world")]
    [SerializeField] private float unitWorldSize = 0.5f;

    public float UnitWorldSize { get => unitWorldSize; set => unitWorldSize = value; }
    public Vector2Int GridSize { get => gridSize; set => gridSize = value; }

    public Vector2 GetCoordinatesFromPosition(Vector2 position)
    {
        return new Vector2(Mathf.RoundToInt(position.x / unitWorldSize), Mathf.RoundToInt(position.y / unitWorldSize));
    }

    public Vector2 GetPostionFromCoordinates(Vector2 coordinates)
    {
        return new Vector3(coordinates.x * unitWorldSize, coordinates.y * unitWorldSize);
    }
}
