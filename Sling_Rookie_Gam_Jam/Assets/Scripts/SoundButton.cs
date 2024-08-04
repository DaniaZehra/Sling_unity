using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    // Start is called before the first frame update
    public Button MusicButton;
    private bool isMuted = false;
    void Start()
    {
        if (MusicButton == null) 
        {
            MusicButton = GameObject.Find("MusicButton").GetComponent<Button>();

            if (MusicButton == null)
            {
                Debug.LogError("MusicButton could not be found. Make sure the name matches and the button exists.");
                return;
            }
        }
        MusicButton.onClick.AddListener(ToggleMute);
        LoadMuteState();
    }

    void ToggleMute(){
        isMuted = !isMuted;
        GameObject[] musicObjects = GameObject.FindGameObjectsWithTag("Music");
        foreach(GameObject musicObject in musicObjects){
            AudioSource audioSource = musicObject.GetComponent<AudioSource>();
            if(audioSource != null)
            {
                audioSource.mute = isMuted;
            }
        }
        SaveMuteState();
    }

    void SaveMuteState()
    {
        PlayerPrefs.SetInt("BackgroundMusicMuted",isMuted ? 1:0);
    }

    void LoadMuteState()
    {
        isMuted = PlayerPrefs.GetInt("BackgroundMusicMuted",0) == 1;

        GameObject[] musicObjects = GameObject.FindGameObjectsWithTag("Music");
        foreach (GameObject musicObject in musicObjects)
        {
            AudioSource audioSource = musicObject.GetComponent<AudioSource>();
            if(audioSource != null)
            {
                audioSource.mute = isMuted;
            }
        }
    }

}