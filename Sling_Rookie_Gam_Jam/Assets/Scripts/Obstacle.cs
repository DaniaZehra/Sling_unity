using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacle : MonoBehaviour
{
    // The scene name to load
    public string sceneToLoad = "GO5";

    // This method will be called when another collider enters the trigger collider attached to this GameObject
    void OnTriggerEnter(Collider other)
    {
        // Check if the object that triggered the collider has the tag "Sling"
        if (other.CompareTag("Sling"))
        {
            // Log to the console for debugging purposes
            Debug.Log("Sling entered the trigger! Loading scene " + sceneToLoad);

            // Load the specified scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
