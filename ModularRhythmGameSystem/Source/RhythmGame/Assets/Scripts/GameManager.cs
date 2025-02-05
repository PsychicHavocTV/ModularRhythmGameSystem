using System.Collections;
using UnityEngine;
using EAudioSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Camera sceneCamera;
    public bool songPlaying = false;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null) { instance = new GameManager(); };
    }

    public IEnumerator StartPresongTimer()
    {
        LevelData.presongTimerFinished = false;
        yield return new WaitForSecondsRealtime(EAudio.levelStartDelaySeconds);
        
        LevelData.presongTimerFinished = true;
        LevelData.source.Play();
        songPlaying = true;
        StopCoroutine(StartPresongTimer());

    }

    private void Update()
    {
        if (EAudio.isLevelStarting == true && LevelData.presongTimerStarted == false)
        {
            LevelData.presongTimerStarted = true;
            StartCoroutine(StartPresongTimer());
        }
    }
}
