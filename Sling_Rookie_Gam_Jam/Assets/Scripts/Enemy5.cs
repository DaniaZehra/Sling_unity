using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy5 : MonoBehaviour
{
    public string scene = "CL5";
    static int totalEnemies = 5;
    private Animator animator;
    private bool isHit = false; // Flag to ensure only one collision is processed per enemy

    void Start()
    {
        totalEnemies = 5;
        Debug.Log("Remaining Enemies: " + totalEnemies);
        animator = GetComponent<Animator>();
    }

    public void SceneSwitch()
    {
        SceneManager.LoadScene(scene);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isHit)
        {
            isHit = true; // Mark the enemy as hit to prevent multiple triggers
            Debug.Log("Enemy hit by player. Destroying enemy!");
            StartCoroutine(EnemyKillSequence());
        }
    }

    IEnumerator EnemyKillSequence()
    {
        // Trigger kill animation
        animator.SetTrigger("TriKill");

        // Wait for 2 second
        yield return new WaitForSeconds(2f);

        // Destroy enemy game object
        Destroy(gameObject);
        totalEnemies--;
        Debug.Log("Remaining Enemies: " + totalEnemies);

        if (totalEnemies <= 0)
        {
            Debug.Log("All enemies killed!");
            //totalEnemies = 5; // Reset for the next level or as needed
            Debug.Log("Level_5 completed");
            SceneSwitch();
        }
    }
}
