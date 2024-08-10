using UnityEngine;
using UnityEngine.SceneManagement;

public class GO5 : MonoBehaviour
{
    public void GoToHomeScreen()
    {
        SceneManager.LoadScene("HomeScreen"); // Replace with your home screen scene name
    }

    public void ReplayLevel()
    {
        SceneManager.LoadScene("Level_5"); // Reload current scene
    }
}
