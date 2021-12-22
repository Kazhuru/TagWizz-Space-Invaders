using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //Cached variables
    EnemyWeave weave;

    void Start()
    {
        weave = FindObjectOfType<EnemyWeave>();
        weave.OnMovementTick += Enemy_OnMovementTick;
    }

    public void Enemy_OnMovementTick()
    {
        this.transform.Translate(Vector3.down * weave.GetMovementSpeed() * Time.deltaTime);
    }

    void OnDestroy()
    {
        weave.OnMovementTick -= Enemy_OnMovementTick;
    }
}
