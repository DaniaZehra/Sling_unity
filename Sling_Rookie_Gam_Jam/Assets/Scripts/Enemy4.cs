using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : MonoBehaviour
{

    static int totalEnemies = 5;
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
            StartCoroutine(EnemyKillSequence());

            if (totalEnemies > 0)
            {
                Debug.Log("Remaining Enemies: " + totalEnemies);
            }
            else
            {
                Debug.Log("Enemy killed!");
                Debug.Log("Level_4 completed");
            }
        }
    }

    IEnumerator EnemyKillSequence()
    {
        // Trigger kill animation
        animator.SetTrigger("TriKill");

        // Wait for 1 second
        yield return new WaitForSeconds(1f);

        // Destroy enemy game object
        Destroy(gameObject);
        totalEnemies--;
    }
}
