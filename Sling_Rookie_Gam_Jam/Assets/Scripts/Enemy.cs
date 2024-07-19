using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    static int totalEnemies = 3;

    void Start()
    {
        Debug.Log("Remaining Enemies: " + totalEnemies);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy hit by player. Destroying enemy!");
            totalEnemies--;
            Destroy(gameObject);

            if (totalEnemies > 0)
            {
                Debug.Log("Remaining Enemies: " + totalEnemies);
            }
            else
            {
                Debug.Log("All enemies killed!");
            }
        }
    }

}
