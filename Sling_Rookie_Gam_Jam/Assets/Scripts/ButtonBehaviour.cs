using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    public void SceneSwitch(string scene){
        SceneManager.LoadScene(scene);
    }
}
