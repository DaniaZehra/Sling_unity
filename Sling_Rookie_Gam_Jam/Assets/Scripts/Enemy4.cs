using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy4 : MonoBehaviour
{
    public string scene = "CL4";
    static int totalEnemies = 5;  // Shared across all enemies of this type
    private Animator animator;
    private bool isHit = false;  // Unique to each instance of Enemy4

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
            isHit = true;  // Prevent further actions for this specific enemy
            Debug.Log("Enemy hit by player. Destroying enemy!");
            StartCoroutine(EnemyKillSequence());
        }
    }

    IEnumerator EnemyKillSequence()
    {
        // Trigger kill animation
        animator.SetTrigger("TriKill");

        // Wait for 3 second
        yield return new WaitForSeconds(2f);

        // Decrement enemy count and destroy this enemy
        Destroy(gameObject);
        totalEnemies--;
        Debug.Log("Remaining Enemies: " + totalEnemies);

        if (totalEnemies <= 0)
        {
            Debug.Log("All enemies killed!");
            //totalEnemies = 5;  // Reset for the next level or as needed
            Debug.Log("Level_4 completed");
            SceneSwitch();
        }
    }
}
