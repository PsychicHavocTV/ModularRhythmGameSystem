using UnityEngine;
using EAudioSystem;
using UnityEngine.Audio;

public class DataInspector : MonoBehaviour
{
    public int levelObjectCount = 0;
    public int levelStartDelay = 0;
    public float levelSpeed = 1;
    public ScriptableObjectHandler[] levels = new ScriptableObjectHandler[0];
    public ScriptableObjectHandler m_levelData;
    public AudioSource m_audioSource;
    public AudioClip m_musicfile;
    public string m_levelName;
    public string m_songName;
    public string m_songArtist;
    private AudioMixerGroup m_mixer;

    bool dataSet = false;

    private void Awake()
    {
        if (LevelData.source == null && m_audioSource != null)
        {
            LevelData.source = m_audioSource;
        }
    }

    private void Update()
    {
        if (dataSet == false)
        {
            dataSet = true;
            EAudioSystem.EAudio.CopyLevelsList(levels);
            NotemapLoader.SetUpFiles();
            if (m_levelData != null)
            {
                m_levelName = m_levelData.levelName;
                m_songName = m_levelData.songName;
                m_songArtist = m_levelData.songArtist;
                if (EAudioSystem.LevelData.source != null)
                {
                    EAudioSystem.LevelData.source.clip = m_levelData.levelSong;
                }
            }

        }
    }
}
