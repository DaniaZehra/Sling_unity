using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public void GoToHomeScreen()
    {
        SceneManager.LoadScene("HomeScreen"); // Replace with your home screen scene name
    }

    public void ReplayLevel()
    {
        // Get the name of the currently active scene
        string currentSceneName = "Level_1";

        // Reload the current scene
        SceneManager.LoadScene(currentSceneName);
    }


    public void GoToNextLevel()
    {
        string nextLevel = "Level_2"; // Ensure this is the correct name of your next level scene
        Debug.Log("Switching to scene: " + nextLevel);
        if (Application.CanStreamedLevelBeLoaded(nextLevel))
        {
            SceneManager.LoadScene(nextLevel);
        }
        else
        {
            Debug.LogError("Scene " + nextLevel + " cannot be loaded. Please check if the scene name is correct and the scene is added to the build settings.");
        }
    }
}
