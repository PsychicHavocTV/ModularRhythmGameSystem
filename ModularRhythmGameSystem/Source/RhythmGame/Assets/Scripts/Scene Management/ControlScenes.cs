using UnityEngine;
using UnityEngine.SceneManagement;
using EAudioSystem;

public class ControlScenes : MonoBehaviour
{
    bool levelLoaded = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EAudio.readyToLoadLevel == true && levelLoaded == false)
        {
            levelLoaded = true;
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
        }
    }
}
