using UnityEngine;
using UnityEngine.SceneManagement;

public class GO1 : MonoBehaviour
{
    public void GoToHomeScreen()
    {
        SceneManager.LoadScene("HomeScreen"); // Replace with your home screen scene name
    }

    public void ReplayLevel()
    {
        SceneManager.LoadScene("Level_1"); // Reload current scene
    }
}
