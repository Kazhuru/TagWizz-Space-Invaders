using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoal : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<EnemyHealth>() != null)
        {
            FindObjectOfType<PlayerHealth>().reducePlayerLifes();
        }
        Destroy(collision.gameObject);
    }
}
