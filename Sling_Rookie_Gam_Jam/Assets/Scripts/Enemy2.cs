using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy2 : MonoBehaviour
{
    public string scene = "CL2";
    static int totalEnemies = 3;
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
                Debug.Log("Level_2 completed");
                SceneSwitch();
            }
        }
    }

    IEnumerator EnemyKillSequence()
    {
        // Trigger kill animation
        animator.SetTrigger("TriKill");

        // Wait for 1 second
        yield return new WaitForSeconds(2f);

        // Destroy enemy game object
        Destroy(gameObject);
    }
}
