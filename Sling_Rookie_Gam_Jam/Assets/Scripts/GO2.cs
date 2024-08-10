using UnityEngine;
using UnityEngine.SceneManagement;

public class GO2 : MonoBehaviour
{
    public void GoToHomeScreen()
    {
        SceneManager.LoadScene("HomeScreen"); // Replace with your home screen scene name
    }

    public void ReplayLevel()
    {
        SceneManager.LoadScene("Level_2"); // Reload current scene
    }
}
