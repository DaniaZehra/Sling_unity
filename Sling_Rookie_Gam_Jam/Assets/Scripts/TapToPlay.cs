using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TapToPlay: MonoBehaviour
{
        public string scene="Level_1";
        void Update(){
        if(Input.GetMouseButton(0)){
          SceneSwitch();  
        }
    }
    public void SceneSwitch(){
        SceneManager.LoadScene(scene);
    }
}