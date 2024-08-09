using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public string scene = "CL1";
    static int totalEnemies = 1;
    private Animator animator;

    void Start()
    {
        Debug.Log("Remaining Enemies: " + totalEnemies);
        animator = GetComponent<Animator>();
    }

    public void SceneSwitch()
    {
        SceneManager.LoadScene(scene);
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
                Debug.Log("Enemy killed!");
                Debug.Log("Level_1 completed");
            }
        }
    }

    IEnumerator EnemyKillSequence()
    {
        // Trigger kill animation
        animator.SetTrigger("TriKill");

        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        // Destroy enemy game object
        Destroy(gameObject);

        // Wait for an additional 2 seconds before switching scene
        //yield return new WaitForSeconds(1f);

        // Switch the scene
        totalEnemies = 1;
        SceneSwitch();
    }
}
