using System;
using UnityEngine;

public class Point : MonoBehaviour
{
    [HideInInspector] public bool isWallContact, isEnemyNear;

    [HideInInspector] public GameObject enemyObject;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Obstacle"))
        {
            isWallContact = true;
            isEnemyNear = false;
        }
        else if (other.CompareTag("Enemy"))
        {
            isEnemyNear = true;
            isWallContact = false;

            enemyObject = other.gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            isWallContact = true;
            isEnemyNear = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isWallContact = false;
        isEnemyNear = false;

        enemyObject = null;
    }
}
