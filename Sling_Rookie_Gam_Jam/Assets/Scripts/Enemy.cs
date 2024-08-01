using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    static int totalEnemies = 3;
    private Animator animator;

    void Start()
    {
        Debug.Log("Remaining Enemies: " + totalEnemies);
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy hit by player. Destroying enemy!");
            totalEnemies--;
            StartCoroutine(EnemyKillSequence());

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

    IEnumerator EnemyKillSequence()
    {
        // Trigger kill animation
        animator.SetTrigger("TriKill");

        // Wait for 1 second
        yield return new WaitForSeconds(0.5f);

        // Destroy enemy game object
        Destroy(gameObject);
    }
}
