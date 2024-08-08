using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2 : MonoBehaviour
{
    // Ensure the scene name is correct and matches the name in your project
    public string scene = "Level_2";

    void Update()
    {
        // Check for left mouse button click
        if (Input.GetMouseButton(0))
        {
            SceneSwitch();
        }
    }

    public void SceneSwitch()
    {
        Debug.Log("Switching to scene: " + scene);
        SceneManager.LoadScene(scene);
    }

}
