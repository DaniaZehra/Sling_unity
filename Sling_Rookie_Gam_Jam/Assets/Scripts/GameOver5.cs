using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver5 : MonoBehaviour
{
    // The name of the scene to load when the object exits the trigger
    public string sceneToLoad = "GO5";

    // This method is called when the object exits a trigger collider
    void OnTriggerExit(Collider other)
    {
        // Check if the trigger collider has the tag "Game"
        if (other.CompareTag("Game"))
        {
            // Log to the console for debugging purposes
            Debug.Log("Object exited the trigger! Loading scene " + sceneToLoad);

            // Load the specified scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
