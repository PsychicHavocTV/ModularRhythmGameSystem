using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleScenes : MonoBehaviour
{
    public void changeToGameScene()
    {
        Debug.Log("Loading new scene.");
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        return;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EAudioSystem.EAudio.readyToLoadLevel == true)
        {
            EAudioSystem.EAudio.readyToLoadLevel = false;
            changeToGameScene();
        }
    }
}
