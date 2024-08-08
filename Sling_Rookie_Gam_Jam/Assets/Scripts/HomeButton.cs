using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeButton : MonoBehaviour
{
    public string scene = "HomeScreen";
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            SceneSwitch();
        }
    }
    public void SceneSwitch()
    {
        SceneManager.LoadScene(scene);
    }
}
