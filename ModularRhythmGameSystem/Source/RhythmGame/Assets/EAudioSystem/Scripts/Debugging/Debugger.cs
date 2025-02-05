using UnityEngine;
using EAudioSystem;

public class Debugger : MonoBehaviour
{
    public AudioSource[] audioSources;
    ScriptableObjectHandler[] levels;

    // Start is called before the first frame update
    void Start()
    {
        //EAudioSystem.NotemapLoader.SetUpFiles();

        foreach (string val in EAudioSystem.NotemapLoader.filePaths)
        {
            Debug.Log(val);
        }

        foreach (string val in EAudioSystem.NotemapLoader.metaFilePaths)
        {
            Debug.Log(val);
        }

        foreach (Notemap map in EAudioSystem.NotemapLoader.notemapFiles)
        {
            foreach (float val in map.timings)
            {
                Debug.Log("LevelTiming: " + val);
            }
        }
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
