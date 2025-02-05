using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EAudioSystem;
using Unity.VisualScripting;



public class Debugger : MonoBehaviour
{
    public AudioSource[] audioSources;
    ScriptableObjectHandler[] levels;

    // Start is called before the first frame update
    void Start()
    {
    }

    void DebugSongLength(ScriptableObject level)
    {
        if (level == null)
        {
            level = levels[0];
        }

        int seconds = 0;
        seconds = EAudioSystem.EAudio.CalculateSongLengthSeconds(levels[0].levelSong);

        Debug.Log("Level Name: " + levels[0].levelName);
        Debug.Log("Song: " + levels[0].songName + " (by " + levels[0].songArtist);

        Debug.Log("Song Length (In Seconds): " + seconds);
    }

    [ContextMenu("List Sources")]
    void ListSources()
    {
        EAudioSystem.EAudio.ListAudioSources(false);
    }
}
