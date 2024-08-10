using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy2 : MonoBehaviour
{
    public string scene = "CL2";
    static int totalEnemies = 3;
    private Animator animator;
    private bool isHit = false;

    void Start()
    {
        totalEnemies = 3;
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
            Debug.Log("Enemy hit by player. Destroying enemy!");
            StartCoroutine(EnemyKillSequence());

        }
    }

    IEnumerator EnemyKillSequence()
    {
        // Trigger kill animation
        animator.SetTrigger("TriKill");

        // Wait for 1 second
        yield return new WaitForSeconds(3f);

        // Destroy enemy game object
        Destroy(gameObject);
        totalEnemies--;
        Debug.Log("Remaining Enemies: " + totalEnemies);

        if (totalEnemies <= 0)
        {
            Debug.Log("Enemy killed!");
            Debug.Log("Level_2 completed");
            totalEnemies = 3;
            SceneSwitch();
        }
    }
}
