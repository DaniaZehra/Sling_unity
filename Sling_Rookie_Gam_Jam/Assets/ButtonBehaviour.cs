using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviour1 : MonoBehaviour
{
    public void SceneSwitch(string scene){
        SceneManager.LoadScene(scene);
    }
}
